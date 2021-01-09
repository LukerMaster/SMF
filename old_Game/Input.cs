using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace SMF
{
    public class Input
    {
        public Input()
        {

        }

        Vector2i mousePos = new Vector2i();

        private bool lmbPressed;
        private bool rmbPressed;

        private bool upPressed;
        private bool leftPressed;
        private bool rightPressed;
        private bool downPressed;
        private bool enterPressed;
        private bool escapePressed;
        private bool boostPressed;
        private bool reloadPressed;

        public bool AttackPressed { get => lmbPressed; private set => lmbPressed = value; }
        public bool RmbPressed { get => rmbPressed; private set => rmbPressed = value; }
        public bool UpPressed { get => upPressed; private set => upPressed = value; }
        public bool LeftPressed { get => leftPressed; private set => leftPressed = value; }
        public bool RightPressed { get => rightPressed; private set => rightPressed = value; }
        public bool DownPressed { get => downPressed; private set => downPressed = value; }
        public Vector2i MousePos { get => mousePos; private set => mousePos = value; }
        public bool EnterPressed { get => enterPressed; private set => enterPressed = value; }
        public bool EscapePressed { get => escapePressed; private set => escapePressed = value; }
        public bool BoostPressed { get => boostPressed; private set => boostPressed = value; }
        public bool ReloadPressed { get => reloadPressed; set => reloadPressed = value; }
    }
}
