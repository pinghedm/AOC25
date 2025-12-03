#r "nuget: PCRE.NET, 1.3.0"
#r "nuget: FSharp.Collections.ParallelSeq, 1.2.0"

open PCRE // the default regex lib doesnt handle backtracks that well
open FSharp.Collections.ParallelSeq // the real range with the regex benefits from being run in parallel


// let input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
let input = "67562556-67743658,62064792-62301480,4394592-4512674,3308-4582,69552998-69828126,9123-12332,1095-1358,23-48,294-400,3511416-3689352,1007333-1150296,2929221721-2929361280,309711-443410,2131524-2335082,81867-97148,9574291560-9574498524,648635477-648670391,1-18,5735-8423,58-72,538-812,698652479-698760276,727833-843820,15609927-15646018,1491-1766,53435-76187,196475-300384,852101-903928,73-97,1894-2622,58406664-58466933,6767640219-6767697605,523453-569572,7979723815-7979848548,149-216"

let checkIfIllegalIdSubPartOne (fullString:string) (slice:string)  = 
    let pattern = $"{slice}{slice}"
    pattern=fullString

let checkIfIllegalIdSubPartTwo (fullString:string) (slice:string) = 

    let pattern = new PcreRegex $"^({slice})({slice})+$"
    pattern.IsMatch fullString

let checkIfIllegalId num = 
    let stringOfNum = string num
    let idxs = [0..stringOfNum.Length/2]
    idxs |> List.exists (fun endIdx -> checkIfIllegalIdSubPartTwo stringOfNum stringOfNum[0..endIdx-1])


let ranges = 
    input.Split "," 
    |> Seq.map (fun range -> 
        let parts = range.Split "-"
        int64 parts[0], int64 parts[1])
        
let allNums = seq {
    for a,b in ranges do
        for n in [a..b] do
            yield n
}
let illegalNums = PSeq.filter checkIfIllegalId allNums
let sum =  illegalNums |> PSeq.sum

printfn "%A" (illegalNums |> Seq.toList)
printfn "%A" sum





