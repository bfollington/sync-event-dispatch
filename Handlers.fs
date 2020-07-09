module SyncDispatch.Handlers 

open SyncDispatch.Events
open SyncDispatch.Effects

type HandlerResult =
    | Effect of Effect 
    | Event of Event 

let hat = {
    Product.Sku = 123
    Name = "Hat"
    Price = 999.0M
}

let handleNewCustomer ev =
    [
        Effect (RegisterUser ev.Customer)
        Effect (SendWelcomeEmail ev.Customer)
        Event (ItemAddedToCart { ItemAddedToCart.Product = hat; Order = 1 })
    ]

let handleShoppingSessionStarted (ev: ShoppingSessionStarted) = 
    let newCustomer = NewCustomerRegistered { Customer = ev.Customer }

    [
        Effect (CreateCart ev.Id)
        Event newCustomer
    ]
    
let handleAddItem (ev: ItemAddedToCart) = 
    [Effect (InsertItemIntoCart (ev.Order, hat))]

let handleEvent = function
    | ShoppingSessionStarted ev -> handleShoppingSessionStarted ev
    | ItemAddedToCart ev -> handleAddItem ev
    | NewCustomerRegistered ev -> handleNewCustomer ev