module Domain.Engine

let handlePhyiscalProduct product shipingaAdress =


    let packingSlip =
        { Adress = shipingaAdress
          Extra = None }

    match product with
    | Book ->
        [ ShipProdcut(packingSlip)
          NotifyRoyality(packingSlip)
          PaymentCommission ]
    | Video ->
        [ ShipProdcut
            ({ packingSlip with
                   Extra = Some(Phyiscal(Other "First Aid kit")) })
          PaymentCommission ]
    | Other _ ->
        // Unspecifed in spec here, assume basic, ship and payment?
        [ ShipProdcut(packingSlip)
          PaymentCommission ]

let handleDigitalProduct product email =
    let memberInfo = { Email = email }
    let defaultAction = [ NotifyMeber memberInfo ]
    match product with
    | NewMember -> ActivateMemberShip :: defaultAction
    | UpgradeMember -> UpgradeMemberShip :: defaultAction

let ProcessOrder (product: Product) =
    // this is a silly customer info but works for this
    let customerInfo =
        { Email = "foo@bar.com"
          Adress = "street foo bar" }

    match product with
    | Phyiscal product -> handlePhyiscalProduct product customerInfo.Adress
    | Digital product -> handleDigitalProduct product customerInfo.Email
