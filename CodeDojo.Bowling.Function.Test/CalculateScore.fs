module CodeDojo.Bowling.Function.Test

open NUnit.Framework
open BowlingCalculator
open BowlingCalculator

[<SetUp>]
let Setup () =
    ()

[<Test>]
let AllStrikes () =
    let expected = 300
    let actual = calculateScore [10;10;10;10;10;10;10;10;10;10;10;10] 0 1
    Assert.AreEqual(expected, actual)

[<Test>]
let AllOpenFrames () =
    let expected = 60
    let actual = calculateScore [3;3;3;3;3;3;3;3;3;3;3;3;3;3;3;3;3;3;3;3] 0 1
    Assert.AreEqual(expected, actual)

[<Test>]
let AllSpares () =
    let expected = 120
    let actual = calculateScore [2;8;2;8;2;8;2;8;2;8;2;8;2;8;2;8;2;8;2;8;2] 0 1
    Assert.AreEqual(expected, actual)

[<Test>]
let BottledTheLastRoll () =
    let expected = 291
    let actual = calculateScore [10;10;10;10;10;10;10;10;10;10;10;1] 0 1
    Assert.AreEqual(expected, actual)
