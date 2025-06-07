module DealingTest

open CardGame.Cards
open Xunit

let dummyPlayer: Player = { Hand = List.Empty }

[<Fact>]
let ``Should return 51 cards when dealing a card from a new deck`` () =
    // Arrange
    let expected = 52 - 1
    let deck: Deck = newDeck
    
    // Act
    let shuffledDeck: ShuffledDeck = ShuffledDeck deck
    let (newDeck, _) = dealCard shuffledDeck dummyPlayer
    let actual = newDeck |> countCardsInShuffledDeck
    // Assert
    Assert.Equal(expected, actual)

[<Fact>]
let ``Should return 0 cards when dealing a card from a depleted deck`` () =
    // Arrange
    let expected = 0
    // Act
    let deck = ShuffledDeck []
    let newDeck,_ = dealCard deck dummyPlayer
    let actual = newDeck |> countCardsInShuffledDeck
    // Assert
    Assert.Equal(expected, actual)

