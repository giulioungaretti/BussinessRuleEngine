namespace Domain

type DigitalProduct =
    | NewMember
    | UpgradeMember

type PhyiscalProduct =
    | Book
    | Video
    | Other of string

type Product =
    | Phyiscal of PhyiscalProduct
    | Digital of DigitalProduct

type PackingSlip =
    { Adress: string
      Extra: Option<Product> }

type MemberShipInfo = { Email: string }

type Result =
    | ShipProdcut of PackingSlip
    | NotifyRoyality of PackingSlip
    | NotifyMeber of MemberShipInfo
    | ActivateMemberShip
    | UpgradeMemberShip
    | PaymentCommission
    | AddFirstAidKit
    | NoOp of string


type CustomerInfo = { Email: string; Adress: string }
