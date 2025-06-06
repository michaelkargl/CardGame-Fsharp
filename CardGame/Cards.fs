// ReSharper disable FSharpInterpolatedString

module CardGame.Cards

open System

type Suit =
    | Hearts
    | Diamonds
    | Clubs
    | Spades

    member public this.ToString: string =
        match this with
        | Hearts -> "♡"
        | Clubs -> "♣"
        | Diamonds -> "♦"
        | _ -> "♤"

type Rank =
    | Two
    | Three
    | Four
    | Five
    | Six
    | Seven
    | Eight
    | Nine
    | Ten
    | Jack
    | Queen
    | King
    | Ace

    member public this.ToString: string =
        match this with
        | Two -> "2"
        | Three -> "3"
        | Four -> "4"
        | Five -> "5"
        | Six -> "6"
        | Seven -> "7"
        | Eight -> "8"
        | Nine -> "9"
        | Ten -> "T"
        | Jack -> "J"
        | Queen -> "Q"
        | King -> "K"
        | Ace -> "A"

// do not reorder the tuple! Only append!
type Card = Card of Suit * Rank

type Deck = Card list

type ShuffledDeck = ShuffledDeck of Deck

type Dealing = ShuffledDeck * Card option

let allSuits = [ Hearts; Diamonds; Clubs; Spades ]

let allRanks =
    [ Two; Three; Four; Five; Six; Seven; Eight; Nine; Ten; Jack; Queen; King; Ace ]

let newDeck =
    allSuits
    |> List.collect (fun suit -> allRanks |> List.map (fun rank -> Card(suit, rank)))

// we pass in the seed here so that we keep this function idempotent
let shuffle (seed: int) (deck: Deck) : ShuffledDeck =
    ShuffledDeck(deck |> List.randomShuffleWith (Random seed))

let dealCard (deck: ShuffledDeck) : Dealing =
    match deck with
    | ShuffledDeck innerDeck when List.isEmpty innerDeck -> (ShuffledDeck [], None)
    | ShuffledDeck innerDeck ->
        let topCard = Some innerDeck.Head
        let restDeck = ShuffledDeck(innerDeck.Tail)
        (restDeck, topCard)

// helper function to unpack a ShuffledDeck into a Deck
// so that we can act on the set of cards more cleanly
let unpackDeck (ShuffledDeck deck) : Deck = deck

let unpackCard (Card(suit, rank)) : Suit * Rank = (suit, rank)

let countCardsInDeck (deck: Deck) : int = deck.Length
let countCardsInShuffledDeck (deck: ShuffledDeck) : int = deck |> unpackDeck |> countCardsInDeck

let getCardShortString (card: Card option) : string =
    match card with
    | None -> "XXX"
    | Some c ->
        c
        |> unpackCard
        |> fun (suit, rank) -> sprintf "%s%s" suit.ToString rank.ToString

let getCardString (card: Card option) : string =
    match card with
    | None -> "XXX"
    | Some c ->
        c
        |> unpackCard
        |> fun (suit, rank) -> sprintf "┌───┐\n│%s  │\n│ %s │\n│  %s│\n└───┘" rank.ToString suit.ToString rank.ToString

let printCard (card: Card option) : unit = card |> getCardString |> printfn "%s\n"


let printDeckOverview (count: int) (deck: Deck) : unit =
    deck
    |> List.take count
    |> List.iter (fun card ->
            let cardShort = getCardShortString (Some card)
            printf "%s " cardShort)

        
