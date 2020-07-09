module SyncDispatch.Commands

open SyncDispatch.Events

let startSession id customer = 
  [ShoppingSessionStarted { ShoppingSessionStarted.Id = id; Customer = customer }]