using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    abstract class Button : MenuComponent
    {
        public abstract void Update(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected, bool isEnter, bool isLeft, bool isRight);

        public abstract void Draw(RenderWindow w);

        protected void CheckHoverAndClick(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected, bool isKeyboardBindPressed)
        {
            if ((mousePos.X > hitbox.Left && mousePos.X < hitbox.Left + hitbox.Width
            && mousePos.Y > hitbox.Top && mousePos.Y < hitbox.Top + hitbox.Height))
                IsHovered = true;
            else
                IsHovered = false;

            if (IsHovered || isSelected)
            {
                IsFocused = true;
                OnFocusConstant?.Invoke();
                if (!wasFocused)
                    OnEnterFocus?.Invoke();
            }
            else
            {
                IsFocused = false;
            }
            if ((!wasPressed && IsFocused && isKeyboardBindPressed) || (!wasPressed && IsHovered && isMousePressed))
            {
                IsClicked = true;
                OnClick?.Invoke();
            }
            else
            {
                IsClicked = false;
            }


            wasPressed = (isMousePressed || isKeyboardBindPressed);
            wasFocused = IsFocused;
        }

        private bool wasPressed = true; // "True" to prevent autoclicking if button is constructed underneath currently clicked one.

        private bool wasFocused;
        private bool isFocused;

        private bool isClicked;

        private bool isHovered;

        public Action OnClick;
        public Action OnEnterFocus;
        public Action OnFocusConstant;

        protected FloatRect hitbox;

        public bool IsClicked { get => isClicked; private set => isClicked = value; }
        public bool IsFocused { get => isFocused; private set => isFocused = value; }
        public bool IsHovered { get => isHovered; private set => isHovered = value; }
        public string FontName { get; set; }
    }
}
