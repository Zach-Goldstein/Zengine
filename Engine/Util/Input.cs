using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class Input
    {
        public static KeyboardInput Keyboard { get; private set; }
        public static MouseInput Mouse { get; private set; }

        public static void Initialize()
        {
            Keyboard = new KeyboardInput();
            Mouse = new MouseInput();
        }

        public static void Update()
        {
            Keyboard.Update();
            Mouse.Update();
        }

        public class KeyboardInput
        {
            private KeyboardState currentKeyboard;
            private KeyboardState previousKeyboard;

            public KeyboardInput()
            {

            }

            public void Update()
            {
                previousKeyboard = currentKeyboard;
                currentKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            }

            public bool IsDown(Keys k) => currentKeyboard.IsKeyDown(k);

            public bool IsPressed(Keys k) => currentKeyboard.IsKeyDown(k) && !previousKeyboard.IsKeyDown(k);

            public bool IsReleased(Keys k) => !currentKeyboard.IsKeyDown(k) && previousKeyboard.IsKeyDown(k);
        }

        public class MouseInput
        {
            private MouseState currentMouse;
            private MouseState previousMouse;

            public void Update()
            {
                previousMouse = currentMouse;
                currentMouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
            }

            public bool IsDown() => currentMouse.LeftButton == ButtonState.Pressed;

            public bool IsPressed() => currentMouse.LeftButton == ButtonState.Pressed && previousMouse.LeftButton != ButtonState.Pressed;

            public bool IsReleased() => currentMouse.LeftButton != ButtonState.Pressed && previousMouse.LeftButton == ButtonState.Pressed;
        }
    }
}
