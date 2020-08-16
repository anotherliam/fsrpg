module Input

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
    MouseX: int;
    MouseY: int;
    MouseXDelta: int;
    MouseYDelta: int;

    // Keyboard shit
    ButtonUp: InputItem;
    ButtonDown: InputItem;
    ButtonLeft: InputItem;
    ButtonRight: InputItem;
}

let getInitialInputItem down = {
    Press = false;
    Down = down;
}

let getInputItem down prev = {
    Press = down && (not prev.Down);
    Down = down;
}

let getInput (prevInput: Option<InputState>) =
    let mouse = Mouse.GetState ()
    let keys = Keyboard.GetState ()
    match prevInput with
        | Some prevInput ->
            // Calculate new input
            {
                Mouse = mouse;
                Keys = keys;

                MouseX = mouse.X;
                MouseY = mouse.Y;
                MouseXDelta = mouse.X - prevInput.MouseX;
                MouseYDelta = mouse.Y - prevInput.MouseY;
                MouseMoved = prevInput.MouseX <> mouse.X || prevInput.MouseY <> mouse.Y;
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
                ButtonUp = (getInputItem (keys.IsKeyDown (Keys.Up)) prevInput.ButtonUp);
                ButtonDown = (getInputItem (keys.IsKeyDown (Keys.Down)) prevInput.ButtonDown);
                ButtonLeft = (getInputItem (keys.IsKeyDown (Keys.Left)) prevInput.ButtonLeft);
                ButtonRight = (getInputItem (keys.IsKeyDown (Keys.Right)) prevInput.ButtonRight);

            }
        | None ->
            // Get default input
            {
                Mouse = mouse;
                Keys = keys;
                
                MouseX = mouse.X;
                MouseY = mouse.Y;
                MouseXDelta = 0;
                MouseYDelta = 0;
                MouseMoved = false;
                PrimaryMouse = getInitialInputItem (mouse.LeftButton = ButtonState.Pressed);
                SecondaryMouse = getInitialInputItem (mouse.LeftButton = ButtonState.Pressed);
                TertiaryMouse = getInitialInputItem (mouse.MiddleButton = ButtonState.Pressed)
                ButtonUp = getInitialInputItem (keys.IsKeyDown (Keys.Up));
                ButtonDown = getInitialInputItem (keys.IsKeyDown (Keys.Down));
                ButtonLeft = getInitialInputItem (keys.IsKeyDown (Keys.Left));
                ButtonRight = getInitialInputItem (keys.IsKeyDown (Keys.Right));
            }