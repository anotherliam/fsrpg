module testing

type State = {
    values: int
}

type EventCommand =
    | NoOp
    | PrintMessage of string

type EventStep =
    | Complete
    | Update of (State -> State)
    | Do of (State -> EventCommand)

type EventStatus =
    | Finished of State
    | Ongoing of State * (State -> EventStatus)

type Event = State -> List<EventStep>

let someEvent (state: State) =
    let mutable count = 1
    [
        Do(fun _ -> NoOp);
        Do(fun _ -> NoOp);
        Update(fun state ->
            { state with
                values = state.values + 2
            }
        )
        Do(fun _ -> PrintMessage "Hello Friends and countrymen")
        Complete
    ]

let handleEventCommand (state: State) (command: EventCommand) =
    match command with
    | NoOp -> state
    | PrintMessage str ->
        do System.Diagnostics.Debug.WriteLine str
        state

let rec eventStep steps state =
    match steps with
    | head::tail ->
        match head with
        | Complete ->
            do System.Diagnostics.Debug.WriteLine "[EVENT FINISHED]"
            do System.Diagnostics.Debug.WriteLine (sprintf "Final state is %A" state)
            Finished(state)
        | Update updater ->
            Ongoing(
                (updater state),
                eventStep tail
            )
        | Do cmd ->
            Ongoing(
                (cmd state |> handleEventCommand state),
                eventStep tail
            )
    | [] -> Finished(state)

// Returns a next function that can be called with state
let runEvent (event: Event) (initialState: State) =
    let rec handleNext eventStatus =
        match eventStatus with
        | Ongoing(state, next) ->
            handleNext(next state)
        | Finished(_) ->
            ()
    handleNext (Ongoing(initialState, eventStep (event initialState)))

type FakeState = {
    Vars: Map<string, string>
}

type DialogEvent =
    | Text of string
    | Name of string

let words = """
>Robert
|So basically, am monkey
|Well, pogger logger, am monkmkge
!v.name v.awesomeness


"""

let rec generateText (str: string) (vars: List<string>) =
    match vars with
    | head::tail ->
        Printf.StringFormat<string->string> str
        |> fun x -> sprintf x head
        |> fun x -> generateText x tail
    | _ -> str

let parseScene (scene: string) (state: FakeState): List<DialogEvent> =
    []

let runWordsTest () =
    let state = {
        Vars = Map([("name", "Jimmy Bobert"); ("awesomeness", "not very :("); ("test", "testicle")])
    }

    do System.Diagnostics.Debug.WriteLine (generateText "so basically, am monkey %s" [state.Vars.["name"]])
    ()

let something =
    seq {
        yield 5
        let x = 3
        yield 2
    }
    