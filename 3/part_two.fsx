open System.IO


let puzzleInput_ = """
987654321111111
811111111111119
234234234234278
818181911112111
"""

let puzzleInput = (puzzleInput_).Split "\n" |> Array.toList |>List.filter (fun line -> line<>"")

// let puzzleInput = File.ReadAllLines "puzzle_input_one.txt" |> Array.toList

let removeAt idx xs =
    xs
    |> List.mapi (fun i x -> (i, x))
    |> List.filter (fun (i, _) -> i <> idx)
    |> List.map snd


let rec findNextBiggestDigit (acc: list<int>) (list: list<int>) = 
    if acc.Length = 12 then
        acc 
    else
        let remainingList = List.mapi (fun idx listEl -> if List.contains idx acc then None else (Some listEl)) list |> List.choose id
        let biggestDigit = List.max remainingList
        let predicate (idx, listEl) = if listEl <> biggestDigit then false else if List.contains idx acc then false else true
        let idxOfBiggestDigit = list|>List.mapi (fun idx listEl -> (idx, listEl)) |> List.tryFindBack predicate |> Option.get |> fst
        findNextBiggestDigit (idxOfBiggestDigit::acc) list




let findJoltageForLine (line:string) = 
    let digits = line |> Seq.map string |> Seq.map int |> Seq.toList
    let joltageIndexList = findNextBiggestDigit [] digits |> List.sort
    let selectedJoltages = List.map (fun idx -> List.item idx digits) joltageIndexList
    printfn "For line %A: %A" line selectedJoltages
    17L

    // let totalJoltage = joltageList|> List.mapi (fun i x -> i,x) |> List.fold (fun acc (i, x) -> acc + x * int64 (pown 10 (12 - i ))) 0L
    // printfn $"For line {line}: ${totalJoltage}"
    // totalJoltage


let joltages =puzzleInput  |> List.map findJoltageForLine
let totalJoltage =List.sum joltages
printfn "Total Joltage: %A" totalJoltage
