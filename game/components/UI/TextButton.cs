using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class TextButton : Button, MenuComponentWithText
    {
        Text labelText = new Text();

        private float focusPercent;
        private float clickPercent;

        public TextButton()
        {
            Position = new Vector2i(0, 0);
            Anchor = new Vector2f(0.0f, 0.0f);
            Height = 10;
        }

        public Font font;

        public Vector2i Position;
        public Vector2f Anchor;
        public int Height;
        public string Label;
        protected float FocusPercent { get => focusPercent; set => focusPercent = Math.Clamp(value, 0.0f, 1.0f); }
        protected float ClickPercent { get => clickPercent; set => clickPercent = Math.Clamp(value, 0.0f, 1.0f); }
        Font MenuComponentWithText.font { get => font; set => font = value; }

        public float GetWidth()
        {
            return hitbox.Width;
        }

        public override void Update(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected = false, bool isEnterPressed = false, bool isLeftPressed = false, bool isRightPressed = false)
        {
            labelText.Font = font;
            labelText.DisplayedString = Label;
            labelText.CharacterSize = (uint)(Height / 1.25f);

            hitbox.Height = Height;
            hitbox.Width = labelText.GetGlobalBounds().Width * 1.1f + 10;
            hitbox.Left = Position.X - Anchor.X * hitbox.Width;
            hitbox.Top = Position.Y - Anchor.Y * hitbox.Height;

            labelText.Position = new Vector2f(hitbox.Left, hitbox.Top);

            CheckHoverAndClick(dt, mousePos, isMousePressed, isSelected, isEnterPressed);

            if (IsFocused) FocusPercent += dt;
            else FocusPercent -= dt * 4;

            if (IsClicked) { ClickPercent = 1; FocusPercent = 1; }
            else ClickPercent -= dt * 2;
        }


        public override void Draw(RenderWindow w)
        {
            float animProgress = (float)Math.Pow(focusPercent, 0.25);
            RectangleShape animRect = new RectangleShape();
            animRect.Position = new Vector2f(hitbox.Left, hitbox.Top);
            animRect.Size = new Vector2f(Math.Clamp(hitbox.Width * animProgress, 0, hitbox.Width), hitbox.Height);
            animRect.FillColor = new Color((byte)Math.Clamp(animProgress * 80, 0, 255),
                                           (byte)Math.Clamp(animProgress * 230, 0, 255),
                                           (byte)Math.Clamp(animProgress * 255, 0, 255),
                                           (byte)Math.Clamp(animProgress * 100 + ClickPercent * 90, 0, 255));

            w.Draw(animRect);
            w.Draw(labelText);
        }

    }
}
