namespace SyncDispatch.Effects

open SyncDispatch.Events

type Effect =
    | NoOp
    | CreateCart of int
    | RegisterUser of Customer
    | SendWelcomeEmail of Customer
    | InsertItemIntoCart of int * Product