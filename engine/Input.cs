using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SMF.engine
{
    class Input
    {
        public Input(RenderWindow w, Settings settings)
        {
            w.KeyPressed += (object sender, KeyEventArgs e) =>
            {
                LeftPressed = e.Code == Keyboard.Key.A  || e.Code == Keyboard.Key.Left  ? true : LeftPressed;
                RightPressed = e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right ? true : RightPressed;
                UpPressed = e.Code == Keyboard.Key.W    || e.Code == Keyboard.Key.Up    ? true : UpPressed;
                DownPressed = e.Code == Keyboard.Key.S  || e.Code == Keyboard.Key.Down  ? true : DownPressed;
                EnterPressed = e.Code == Keyboard.Key.Enter ? true : EnterPressed;
            };

            w.KeyReleased += (object sender, KeyEventArgs e) =>
            {
                LeftPressed = e.Code == Keyboard.Key.A  || e.Code == Keyboard.Key.Left  ? false : LeftPressed;
                RightPressed = e.Code == Keyboard.Key.D || e.Code == Keyboard.Key.Right ? false : RightPressed;
                UpPressed = e.Code == Keyboard.Key.W    || e.Code == Keyboard.Key.Up    ? false : UpPressed;
                DownPressed = e.Code == Keyboard.Key.S  || e.Code == Keyboard.Key.Down  ? false : DownPressed;
                EnterPressed = e.Code == Keyboard.Key.Enter ? false : EnterPressed;
            };

            w.MouseButtonPressed += (object sender, MouseButtonEventArgs e) =>
            {
                LmbPressed = e.Button == Mouse.Button.Left ? true : LmbPressed;
                RmbPressed = e.Button == Mouse.Button.Right ? true : RmbPressed;
            };

            w.MouseButtonReleased += (object sender, MouseButtonEventArgs e) =>
            {
                LmbPressed = e.Button == Mouse.Button.Left ? false : LmbPressed;
                RmbPressed = e.Button == Mouse.Button.Right ? false : RmbPressed;
            };

            w.MouseMoved += (object sender, MouseMoveEventArgs e) =>
            {
                if (settings.stretched)
                {
                    mousePos.X = (int)(((float)(e.X) / w.Size.X) * settings.playfieldSize.X);
                    mousePos.Y = (int)(((float)(e.Y) / w.Size.Y) * settings.playfieldSize.Y);
                }
                else
                {
                    float screenAspectRatio = (float)w.Size.X / w.Size.Y;
                    float fieldAspectRatio = (float)settings.playfieldSize.X / settings.playfieldSize.Y;

                    float fieldToScreenRatio = fieldAspectRatio / screenAspectRatio;

                    float left = Math.Max(0, (1 - fieldToScreenRatio) / 2) * w.Size.X;
                    float width = Math.Min(1, fieldToScreenRatio) * w.Size.X;

                    float top = Math.Max(0, (1 - (1 / fieldToScreenRatio)) / 2) * w.Size.Y;
                    float height = Math.Min(1 / fieldToScreenRatio, 1) * w.Size.Y;


                    mousePos.X = (int)(((e.X - left) / (float)width) * settings.playfieldSize.X);
                    mousePos.Y = (int)(((e.Y - top) / (float)height) * settings.playfieldSize.Y);
                }
            };
        }

        Vector2i mousePos = new Vector2i();

        private bool lmbPressed;
        private bool rmbPressed;

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;
        private bool downPressed;
        private bool enterPressed;

        public bool LmbPressed { get => lmbPressed; private set => lmbPressed = value; }
        public bool RmbPressed { get => rmbPressed; private set => rmbPressed = value; }
        public bool UpPressed { get => upPressed; private set => upPressed = value; }
        public bool LeftPressed { get => leftPressed; private set => leftPressed = value; }
        public bool RightPressed { get => rightPressed; private set => rightPressed = value; }
        public bool DownPressed { get => downPressed; private set => downPressed = value; }
        public Vector2i MousePos { get => mousePos; set => mousePos = value; }
        public bool EnterPressed { get => enterPressed; set => enterPressed = value; }
    }
}
