module Tests

open Domain
open Xunit

let customerInfo =
    { Email = "foo@bar.com"
      Adress = "street foo bar" }

let memberInfo = { Email = customerInfo.Email }

[<Fact>]
let ``Test physical product`` () =
    // • If the payment is for a physical product
    //  generate a packing slip for shipping.
    // • If the payment is for a physical product or a book,
    // • generate a commission payment to the agent.
    let order = Other "A red dress" |> Phyiscal

    let result = Engine.ProcessOrder(order)

    let packingSlip =
        { Adress = customerInfo.Adress
          Extra = None }

    let expected =
        [ ShipProdcut(packingSlip)
          PaymentCommission ]

    Assert.Equal<Result list>(expected, result)



[<Fact>]
let ``Test Book `` () =
    // • If the payment is for a physical product
    // •  generate a packing slip for shipping.
    // • If the payment is for a book
    // • create a duplicate packing slip for the royalty department.
    // • If the payment is for a physical product or a book,
    // generate a commission payment to the agent.


    let order = Phyiscal Book

    let result = Engine.ProcessOrder(order)

    let packingSlip =
        { Adress = customerInfo.Adress
          Extra = None }

    let expected =
        [ ShipProdcut(packingSlip)
          NotifyRoyality(packingSlip)
          PaymentCommission ]

    Assert.Equal<Result list>(expected, result)

[<Fact>]
let ``Test Video `` () =
    // • If the payment is for a physical product
    // •  generate a packing slip for shipping.
    // • If the payment is for the video “Learning to Ski,
    // •  add a free “First Aid” video to the packing slip
    // • the result of a court decision in 1997
    // • If the payment is for a physical product or a book,
    // generate a commission payment to the agent.

    let order = Phyiscal Video

    let result = Engine.ProcessOrder(order)

    let packingSlip =
        { Adress = customerInfo.Adress
          Extra = None }

    let expected =
        [ ShipProdcut
            ({ packingSlip with
                   Extra = Some(Phyiscal(Other "First Aid kit")) })
          PaymentCommission ]

    Assert.Equal<Result list>(expected, result)


[<Fact>]
let ``Test Membership `` () =
    // • If the payment is for a membership
    // activate that membership.
    // • If the payment is for a membership or upgrade
    //  e-mail the owner and inform them of the activation/upgrade.

    let order = Digital NewMember

    let result = Engine.ProcessOrder(order)

    let expected =
        [ ActivateMemberShip
          NotifyMeber memberInfo ]

    Assert.Equal<Result list>(expected, result)

[<Fact>]
let ``Test Membership  Update`` () =
    // • If the payment is an upgrade to a membership
    // •  apply the upgrade.
    // • If the payment is for a membership or upgrade
    //  e-mail the owner and inform them of the activation/upgrade.

    let order = Digital UpgradeMember

    let result = Engine.ProcessOrder(order)

    let expected =
        [ UpgradeMemberShip
          NotifyMeber memberInfo ]

    Assert.Equal<Result list>(expected, result)
