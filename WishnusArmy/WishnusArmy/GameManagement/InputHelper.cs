using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static Constant;

public class InputHelper
{
    protected MouseState currentMouseState, previousMouseState;
    protected KeyboardState currentKeyboardState, previousKeyboardState;
    protected Vector2 scale, offset;
    protected int currentScrollState, previousScrollState;

    public InputHelper()
    {
        scale = Vector2.One;
        offset = Vector2.Zero;
    }

    public void Update()
    {
        previousMouseState = currentMouseState;
        previousKeyboardState = currentKeyboardState;
        currentMouseState = Mouse.GetState();
        currentKeyboardState = Keyboard.GetState();
        previousScrollState = currentScrollState;
        currentScrollState = currentMouseState.ScrollWheelValue;
    }

    public bool ScrollUp()
    {
        return currentScrollState > previousScrollState;
    }
    public bool ScrollDown()
    {
        return currentScrollState < previousScrollState;
    }

    public Vector2 Scale
    {
        get { return scale; }
        set { scale = value; }
    }

    public Vector2 Offset
    {
        get { return offset; }
        set { offset = value; }
    }

    public Vector2 MousePosition
    {
        get { return (new Vector2(currentMouseState.X, currentMouseState.Y) - offset ) / scale; }
    }

    public bool MouseInGameWindow
    {
        get { return MouseInRectangle(new Rectangle(Point.Zero, GAME_WINDOW_SIZE)); }
    }

    public bool MouseInRectangle(Rectangle r)
    {
        if (MousePosition.X >= r.X && MousePosition.X < r.X + r.Width &&
            MousePosition.Y >= r.Y && MousePosition.Y < r.Y + r.Height)
        {
            return true;
        }
        return false;
    }

    public bool MouseLeftButtonPressed()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released;
    }

    public bool MouseRightButtonPressed()
    {
        return currentMouseState.RightButton == ButtonState.Pressed && previousMouseState.RightButton == ButtonState.Released;
    }

    public Keys[] CurrentPressedKeys()
    {
        return currentKeyboardState.GetPressedKeys();
    }

    public bool MouseLeftButtonDown()
    {
        return currentMouseState.LeftButton == ButtonState.Pressed;
    }

    public bool KeyPressed(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k) && previousKeyboardState.IsKeyUp(k);
    }

    public bool IsKeyDown(Keys k)
    {
        return currentKeyboardState.IsKeyDown(k);
    }

    public bool AnyKeyPressed
    {
        get { return currentKeyboardState.GetPressedKeys().Length > 0 && previousKeyboardState.GetPressedKeys().Length == 0; }
    }
}