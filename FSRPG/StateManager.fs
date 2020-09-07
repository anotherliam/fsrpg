module StateManager

open FSRPG

type Reducer = State.WorldState -> Actions.GameAction -> State.WorldState

type StateManager =
    {
        Reducers: List<Reducer>;
        Current: State.WorldState;
        ActionHistory: List<Actions.GameAction>
    }

    member this.next action =
        {
            this with
                Current =
                    List.fold
                        (fun state reducer -> reducer state action)
                        this.Current
                        this.Reducers
                ActionHistory = action :: this.ActionHistory
        }

    static member create initialState reducers =
        {
            Reducers = reducers;
            Current = initialState;
            ActionHistory = [];
        }