namespace FSRPG.Input

open Microsoft.Xna.Framework.Input

type InputItem = {
    // Triggers only in the frame where the button was pressed down
    Press: bool;
    // Triggered while the button is pressed
    Down: bool;
}

type InputState = {
    Mouse: MouseState;
    Keys: KeyboardState;
    // Actually useful

    // Mouse shit
    MouseMoved: bool;
    PrimaryMouse: InputItem;
    SecondaryMouse: InputItem;
    TertiaryMouse: InputItem;
    MouseX: float32;
    MouseY: float32;
    MouseXDelta: float32;
    MouseYDelta: float32;

    // Keyboard shit
    ButtonUp: InputItem;
    ButtonDown: InputItem;
    ButtonLeft: InputItem;
    ButtonRight: InputItem;
    ButtonSave: InputItem;
    ButtonLoad: InputItem;
}

module InputUtils =
    
    let getInitialInputItem down = {
        Press = false;
        Down = down;
    }

    let getInputItem (keys: KeyboardState) key prev = 
        let down = keys.IsKeyDown key
        {
            Press = down && (not prev.Down);
            Down = down;
        }

    let getInput (prevInput: Option<InputState>) =
        let mouse = Mouse.GetState ()
        let keys = Keyboard.GetState ()
        let getInput = getInputItem keys
        match prevInput with
            | Some prevInput ->
                // Calculate new input
                {
                    Mouse = mouse;
                    Keys = keys;

                    MouseX = float32 mouse.X;
                    MouseY = float32 mouse.Y;
                    MouseXDelta = (float32 mouse.X) - prevInput.MouseX;
                    MouseYDelta = (float32 mouse.Y) - prevInput.MouseY;
                    MouseMoved = prevInput.MouseX <> (float32 mouse.X) || prevInput.MouseY <> (float32 mouse.Y);
                    PrimaryMouse = {
                        Press = not prevInput.PrimaryMouse.Down && mouse.LeftButton = ButtonState.Pressed;
                        Down = mouse.LeftButton = ButtonState.Pressed;
                    };
                    SecondaryMouse = {
                        Press = not prevInput.SecondaryMouse.Down && mouse.RightButton = ButtonState.Pressed;
                        Down = mouse.RightButton = ButtonState.Pressed;
                    };
                    TertiaryMouse = {
                        Press = not prevInput.SecondaryMouse.Down && mouse.MiddleButton = ButtonState.Pressed;
                        Down = mouse.MiddleButton = ButtonState.Pressed;
                    }
                    ButtonUp = (getInput Keys.Up prevInput.ButtonUp);
                    ButtonDown = (getInput Keys.Down prevInput.ButtonDown);
                    ButtonLeft = (getInput Keys.Left prevInput.ButtonLeft);
                    ButtonRight = (getInput Keys.Right prevInput.ButtonRight);
                    ButtonSave = (getInput Keys.I prevInput.ButtonSave);
                    ButtonLoad = (getInput Keys.L prevInput.ButtonLoad);

                }
            | None ->
                // Get default input
                {
                    Mouse = mouse;
                    Keys = keys;
                
                    MouseX = float32 mouse.X;
                    MouseY = float32 mouse.Y;
                    MouseXDelta = 0.0f;
                    MouseYDelta = 0.0f;
                    MouseMoved = false;
                    PrimaryMouse = getInitialInputItem (mouse.LeftButton = ButtonState.Pressed);
                    SecondaryMouse = getInitialInputItem (mouse.LeftButton = ButtonState.Pressed);
                    TertiaryMouse = getInitialInputItem (mouse.MiddleButton = ButtonState.Pressed)
                    ButtonUp = getInitialInputItem (keys.IsKeyDown (Keys.Up));
                    ButtonDown = getInitialInputItem (keys.IsKeyDown (Keys.Down));
                    ButtonLeft = getInitialInputItem (keys.IsKeyDown (Keys.Left));
                    ButtonRight = getInitialInputItem (keys.IsKeyDown (Keys.Right));
                    ButtonSave = getInitialInputItem (keys.IsKeyDown (Keys.I));
                    ButtonLoad = getInitialInputItem (keys.IsKeyDown (Keys.L));
                }