open SyncDispatch.Effects
open SyncDispatch.Events
open SyncDispatch.Handlers
open SyncDispatch.Commands

// Command (i.e. Event Constructor) -> Handle Events (rec) ->  .... -> Aggregate Effects -> Apply at Boundary

// Iterate over a list of Events and Effects and unpack Events
let rec evaluate handlerResults output =
    match handlerResults with
    | [] -> output
    | head :: xs -> 
        match head with
        | Effect _ -> evaluate xs (output @ [head])
        | Event ev -> evaluate xs (output @ handleEvent ev)

// Recursively passes over list until all Events have been evaluated to Effects
let rec exhaust effects handlerResults =
    if handlerResults |> List.exists (function | Event _ -> true | _ -> false)
    then exhaust effects (evaluate handlerResults [])
    else handlerResults

// Take the output of a Command and exhaustively unpack it to Effects
let dispatch events =
    events
    |> List.fold (fun acc n -> acc @ handleEvent n) []
    |> exhaust []
    |> List.map (
        function
        | Event _ -> NoOp // Can't happen
        | Effect fx -> fx )


let customer = {
    Customer.Name = "Ben"
    Email = "bf@connectdevelop.com"
}

[<EntryPoint>]
let main argv =
    startSession 123 customer
    |> dispatch
    |> printfn "Effects: %A"

    0 // return an integer exit code