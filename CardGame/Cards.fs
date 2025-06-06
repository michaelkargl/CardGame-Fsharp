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
    | Ace = 1
    | Two = 2
    | Three = 3
    | Four = 4
    | Five = 5
    | Six = 6
    | Seven = 7
    | Eight = 8
    | Nine = 9
    | Ten = 10
    | Jack = 11
    | Queen = 12
    | King = 13

// do not reorder the tuple! Only append!
type Card = Card of Suit * Rank

type Deck = Card list

type ShuffledDeck = ShuffledDeck of Deck

type Dealing = ShuffledDeck * Card option

let allSuits = [ Hearts; Diamonds; Clubs; Spades ]

let allRanks =
    [ Rank.Two
      Rank.Three
      Rank.Four
      Rank.Five
      Rank.Six
      Rank.Seven
      Rank.Eight
      Rank.Nine
      Rank.Ten
      Rank.Jack
      Rank.Queen
      Rank.King
      Rank.Ace ]

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

let rankToString (rank: Rank) : string =
    match rank with
    | Rank.Two
    | Rank.Three
    | Rank.Four
    | Rank.Five
    | Rank.Six
    | Rank.Seven
    | Rank.Eight
    | Rank.Nine -> (int rank).ToString()
    | Rank.Ten -> "T"
    | Rank.Jack -> "J"
    | Rank.Queen -> "Q"
    | Rank.King -> "K"
    | Rank.Ace -> "A"
    | _ -> "X"


let getCardShortString (card: Card option) : string =
    match card with
    | None -> "XXX"
    | Some c ->
        c
        |> unpackCard
        |> fun (suit, rank) -> sprintf "%s%s" suit.ToString (rankToString rank)

let getCardString (card: Card option) : string =
    match card with
    | None -> "XXX"
    | Some c ->
        c
        |> unpackCard
        |> fun (suit, rank) ->
            sprintf "┌───┐\n│%s  │\n│ %s │\n│  %s│\n└───┘" (rankToString rank) suit.ToString (rankToString rank)

let printCard (card: Card option) : unit = card |> getCardString |> printfn "%s\n"


let printDeckOverview (count: int) (deck: Deck) : unit =
    deck
    |> List.take count
    |> List.iter (fun card ->
        let cardShort = getCardShortString (Some card)
        printf "%s " cardShort)
