namespace SyncDispatch.Events 

type Customer = 
  { Name: string
    Email: string }

type Product = 
  { Sku: int
    Name: string
    Price: decimal }



type ShoppingSessionStarted = 
  { Id: int
    Customer: Customer }

type ItemAddedToCart = 
  { Order: int
    Product: Product }

type NewCustomerRegistered = 
  { Customer: Customer }


type Event = 
  | ShoppingSessionStarted of ShoppingSessionStarted
  | NewCustomerRegistered of NewCustomerRegistered
  | ItemAddedToCart of ItemAddedToCart