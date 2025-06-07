module CardGame.Tests.ScoringTest

open CardGame.Cards
open Xunit

[<Fact>]
let ``returns 0 given an empty hand`` () =
    let player: Player = { Hand = List.Empty }
    let score = getPlayerScore player
    Assert.Equal(0, score)

[<Fact>]
let ``returns score of first card given a hand of 1`` () =
    let expectedRank = Rank.Jack
    let card: Card = Card(Suit.Clubs, expectedRank)
    let player: Player = { Hand = [ card ] }
    let score = getPlayerScore player
    Assert.Equal(int expectedRank, score)

[<Fact>]
let ``returns sum of all player held cards`` () =
    let player: Player =
        { Hand =
            [ Card(Suit.Clubs, Rank.Ace)
              Card(Suit.Hearts, Rank.Seven)
              Card(Suit.Spades, Rank.Queen) ] }
    let score = getPlayerScore player
    Assert.Equal(20, score)
