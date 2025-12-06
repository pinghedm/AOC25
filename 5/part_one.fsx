open System.IO


// let puzzleInput = """3-5
// 10-14
// 16-20
// 12-18

// 1
// 5
// 8
// 11
// 17
// 32"""

let puzzleInput = File.ReadAllText "input.txt"


let parseRange (range: string) = 
    let split = range.Split "-"
    let l = int64 split[0]
    let r = int64 split[1]
    l,r

let isIdInRange (id:int64) (range:int64*int64) = 
    (id>= (fst range)) && (id <= (snd range))

let splitInput = puzzleInput.Split "\n\n"



let currentIngredientIds = splitInput[1].Split "\n" |> Array.toList |>List.filter (fun x -> x<>"") |> List.map int64 
let freshRanges = splitInput[0].Split "\n" |> Array.toList |> List.map parseRange 

let ingredientIsOk =  List.map (fun ingId -> List.exists (fun r -> isIdInRange ingId r) freshRanges) currentIngredientIds



printfn "Num fresh: %A" (ingredientIsOk |> List.filter id).Length

