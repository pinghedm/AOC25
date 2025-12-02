open System.IO

// let puzzleInput = ["L68";"L30";"R48";"L5";"R60";"L55";"L1";"L99";"R14";"L82";]
// let puzzleInput = ["R65";"R85";"L50";"L11";"L40";"R51";"R49";"R772";"L532"]
let puzzleInput = File.ReadAllLines "input_one.txt" |> Array.toList 

type Instruction =
    | Left of int
    | Right of int

let parseLine (line: string): Option<Instruction> = 
    match line.[0] with
        | 'L' -> Some (Left (int line.[1..]))
        | 'R' -> Some (Right (int line.[1..]))
        | _ -> None

let adjustedModulo x m =
// I want the result of a remainder to always be positive.  eg -11 % 100 would be -11, but I want it to be 89
    let r = x % m
    if r < 0 then r + m else r


let getNextNum (cur, priorZeros) instruction = 
    match instruction with
        | Some(Left distance) -> 
            let fullCircles = distance / 100
            let remainder = distance - (fullCircles*100)
            let newPosRaw = cur - remainder
            let zeroCrossings = fullCircles + (if newPosRaw < 0 && cur <> 0 then 1 else 0)
            let newPos = adjustedModulo newPosRaw 100
            printfn $"from {cur} turn LEFT {distance} to {newPos}"
            newPos, zeroCrossings+priorZeros
        | Some(Right distance) -> 
            let fullCircles = distance / 100
            let remainder = distance - (fullCircles*100)
            let newPos = (cur + remainder ) % 100
            let zeroCrossings = fullCircles + (if (cur + remainder)> 100 then 1 else 0)
            printfn $"from {cur} turn RIGHT {distance} to {newPos}"
            newPos, priorZeros + zeroCrossings
        | None -> cur, priorZeros


let parsed = List.map parseLine puzzleInput
let applied = List.scan getNextNum (50, 0) parsed
let zeroLandings = List.filter (fun (num, zeroCrossings) -> num=0) applied
let zeroCrossings = List.last applied
printfn $"{zeroLandings.Length + snd zeroCrossings}"
