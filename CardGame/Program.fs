module CardGame.Program

open System
open CardGame.Cards

type Rules = { TargetScore: int }

let evaluateWinCondition (player: Player) (rules: Rules) : bool =
    getPlayerScore player <= rules.TargetScore

let nextCardRequested (key: ConsoleKeyInfo) : bool = key.Key = ConsoleKey.Enter

let printScore (rules: Rules) (player: Player) : unit =
    let playerScore = getPlayerScore player
    printfn $"Score: %i{playerScore}/%i{rules.TargetScore}"

let printNextUpCards (deck: Deck) : unit =
    printf "Next up: "
    printDeckOverview 5 deck
    printfn ""
    

let printWinLossMessage (playerWinState: bool) : unit =
    if playerWinState then
        printfn "YOU WIN!!!!"
    else
        printfn "You loose"


type ErrorCodes =
    static member public Success = 0
    static member public UnhandledError = 1

[<EntryPoint>]
let main args : int =
    try
        let seed = Random.Shared.Next()
        let mutable deck: ShuffledDeck = Cards.newDeck |> shuffle 123
        let mutable player: Player = { Hand = List.empty<Card> }
        let blackJackRules: Rules = { TargetScore = 21 }

        let mutable continueGame = true
        while (continueGame) do
            let (_newDeck, _newPlayer) = dealCard deck player
            player <- _newPlayer
            deck <- _newDeck

            let unpackedNewDeck = deck |> unpackDeck
            printCard (Some player.Hand.Head)
            printScore blackJackRules player
            
            printNextUpCards unpackedNewDeck
            printfn ""
            
            printfn "---------------------"
            printfn "Press keys to play:"
            printfn "Draw a card: Enter"
            printfn "Draw no card: N/n"
            printfn "Leave table: Ctrl+C"
            continueGame <- evaluateWinCondition player blackJackRules && nextCardRequested (Console.ReadKey())
            printfn "---------------------"
                
        
        evaluateWinCondition player blackJackRules |> printWinLossMessage
        ErrorCodes.Success
    with ex ->
        printfn $"An unknown exception has been thrown: {ex.Message}"
        ErrorCodes.UnhandledError
