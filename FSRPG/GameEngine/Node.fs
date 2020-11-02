namespace GameEngine

open MonoGame.Extended.Sprites

type Context = int32

// A component is a pure function that takes Props and 'Context' (Global Game State)
// It returns 

type 'TProps GameObject = GameObject of ('TProps -> Renderable List * ((GameObject<'TProps> * 'TProps) List))

// A component should output both a list of rendarables to draw and a list of new GameObjects and their props ready to be put into a new node

type 'TProps Node =
    | Fresh of 'TProps GameObject
    | Ready of 'TProps GameObject * 'TProps * Renderable List * 'TProps Node List


// On update we traverse the tree of nodes and update them one by one.
// A node 'Reupdates' if its "Fresh" or if its props have changed

// Example GameObject

module GameEngineTests =
    open Microsoft.Xna.Framework

    type TileProps =
        {
            GridPosition: Point;
            Type: string;
        }
    let Tile (props: TileProps) =
        (
            [Renderable.DebugText (sprintf "(Tile@%i,%i:[%s])" props.GridPosition.X props.GridPosition.Y props.Type)],
            []
        )

    type TileMapProps =
        {
            Width: int;
            Height: int;
        }
    let TileMap ({Width = width; Height = height}: TileMapProps) =
        (
            [Renderable.DebugText "TileMap"],
            seq {
                for row in 0 .. width do
                    for col in 0 .. height ->
                        (Tile, { GridPosition = (Point (row, col)); Type = "Plains" })
            }
            |> Seq.toList
        )
        

    let rec updateTree initialNode props =

        let updateRest obj props renderables children =
            let updatedChildren =
                children |>
                List.map (fun (child, childProps) -> updateTree (Fresh child) childProps )
            Ready (obj, props, renderables, updatedChildren)

        match initialNode with
        | Fresh (GameObject obj) ->
            // An unupdated game object
            // Update it
            let (renderables, children) = obj props
            // Call updateTree on each of its children
            updateRest (GameObject obj) props renderables children
        | Ready ((GameObject obj), prevProps, _, _) ->
            // Only update if props is different
            if props = prevProps
                then initialNode
                else // Update
                    let (renderables, children) = obj props
                    updateRest (GameObject obj) props renderables children

    let rec drawTree initialNode spriteBatch time =
        let rec prepare node renderables =
            let rec prepareChildren children renderables =
                match children with
                | head::tail -> 
                    prepareChildren tail (prepare head renderables)
                | [] -> renderables
            match node with
            | Fresh _ -> renderables // Ignore if fresh (Shouldnt be? Should I design types so this isnt possible?)
            | Ready (_, _, newRenderables, children) ->
                // Depth first probably fine??
                prepareChildren children (List.append renderables newRenderables)
        prepare initialNode []
        |> (List.map (fun renderable ->
            do renderable spriteBatch time
        ))
