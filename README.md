# Card Game

A terminal black-jack like card game written in F#

![terminal recording](./rec.svg)

## Quality Goals

- In short: This application is intended to be a playground for getting to know F#
- It should build
- It should be written in the functional programming style
- It should contain some tests

## Spec


| Name          | Description                                          |
|---------------|------------------------------------------------------|
| Suit          | Hearts, Diamonds, ...                                |
| Rank          | Ace, King, 1, 2, ...                                 |
| Card          | A card with a suit and rank                          |
| Deck          | A list of cards                                      |
| Shuffled Deck | A type that enforces the use of the shuffle function |
| Hand          | A list of player held cards                          |
| Player        | A player with a hand                                 |
| Rules         | A set of rules for the game                          |

## Rules

We adapt the blackjack ruleset but leave out the second player / dealer.
The player can request cards until the game ends which is when the player
reaches 21 or more or when they decide to leave the table.