module CardGame.Program

open System
open CardGame.Cards

type ErrorCodes =
    static member public Success = 0
    static member public UnhandledError = 1

[<EntryPoint>]
let main args : int =
    try
        let seed = Random.Shared.Next()
        let mutable deck: ShuffledDeck = Cards.newDeck |> shuffle 123

        while (true) do
            Console.WriteLine("Draw a card (Enter/Ctrl+C)?")
            Console.ReadKey() |> ignore
            let (newDeck, card) = dealCard deck

            let unpackedNewDeck = newDeck |> unpackDeck
            printCard card

            printfn "Next up: "
            printDeckOverview 5 unpackedNewDeck
            printfn "\n"

            deck <- newDeck

        ErrorCodes.Success
    with ex ->
        printfn $"An unknown exception has been thrown: {ex.Message}"
        ErrorCodes.UnhandledError
