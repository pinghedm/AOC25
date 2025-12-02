open System.IO

let puzzleInput = File.ReadAllLines "input_one.txt" |> Array.toList 
//let puzzleInput = ["L68";"L30";"R348";"L5";"R60";"L655";"L1";"L99";"R714";"L82";]


type Instruction =
    | Left of int
    | Right of int

let parseLine (line: string): Option<Instruction> = 
    match line.[0] with
        | 'L' -> Some (Left (int line.[1..]))
        | 'R' -> Some (Right (int line.[1..]))
        | _ -> None

let getNextNum cur instruction = 
    match instruction with
        | Some(Left(num)) -> 
            let intermediate = (cur-num) 
            let safe= 
                match intermediate with 
                | x when x < 0 -> (abs 100 + x) % 100
                | x -> x % 100
            safe
        | Some(Right(num)) -> 
            (cur + num ) % 100
        | None -> cur


let parsed = List.map parseLine puzzleInput
printfn "-------------------------------"
let applied = List.scan getNextNum 50 parsed
let zeros = List.filter (fun x -> x=0) applied
printfn $"there are {List.length zeros} 0's"


