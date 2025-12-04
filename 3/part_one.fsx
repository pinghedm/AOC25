open System.IO


// let puzzleInput_ = """
// 987654321111111
// 811111111111119
// 234234234234278
// 818181911112111
// """

// let puzzleInput = puzzleInput_.Split "\n" |> Array.toList

let puzzleInput = File.ReadAllLines "puzzle_input_one.txt" |> Array.toList


let findJoltageForLine (line:string) = 
    let digits = line |> Seq.map string |> Seq.map int |> Seq.toList
    let biggestDigit = List.max digits[0..digits.Length-2]
    let idxOfBiggestDigit = List.findIndex (fun x -> x=biggestDigit) digits
    let remainder = digits[idxOfBiggestDigit+1..]
    let nextBiggest = List.max remainder
    printfn $"For line {line}, joltage is {biggestDigit}{nextBiggest}"
    biggestDigit*10 + nextBiggest



let joltages =puzzleInput |>List.filter (fun line -> line<>"") |> List.map findJoltageForLine
let totalJoltage =List.sum joltages
printfn "Total Joltage: %A" totalJoltage
