using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class AdjustButtonSet : MenuComponent
    {
        TextButton labelButton;

        TextButton prevButton;
        TextButton nextButton;

        Text currentSetting = new Text();

        private Func<String> UpdateSetting;

        public String Label;

        public int Height;

        public Vector2f Anchor;

        public Vector2i Position;

        public bool IsHovered { get { return labelButton.IsHovered || prevButton.IsHovered || nextButton.IsHovered; } }

        public void SetStringFunction(Func<String> f)
        {
            UpdateSetting = f;
        }

        public void SetOnClickPrev(Action f)
        {
            prevButton.OnClick += f;
        }
        public void SetOnClickNext(Action f)
        {
            nextButton.OnClick += f;
        }

        public AdjustButtonSet(Font font)
        {
            currentSetting.Font = font;
            labelButton = new TextButton(font);
            prevButton = new TextButton(font);
            nextButton = new TextButton(font);
        }

        public void Update(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected, bool isEnterPressed, bool isLeft, bool isRight)
        {
            labelButton.Update(dt, mousePos, false, isSelected, false);
            nextButton.Update(dt, mousePos, isMousePressed, isSelected, isRight);
            prevButton.Update(dt, mousePos, isMousePressed, isSelected, isLeft);
        }

        public void Draw(RenderWindow w)
        {
            labelButton.Label = Label;
            currentSetting.DisplayedString = UpdateSetting();
            prevButton.Label = "<";
            nextButton.Label = ">";
            labelButton.Height = Height;
            prevButton.Height = Height;
            nextButton.Height = Height;
            float width = currentSetting.GetGlobalBounds().Width + prevButton.GetWidth() + nextButton.GetWidth() + labelButton.GetWidth();

            currentSetting.CharacterSize = (uint)(Height / 1.25f);

            labelButton.Position = Position;
            prevButton.Position = new Vector2i((int)(Position.X + labelButton.GetWidth()), Position.Y);
            currentSetting.Position = new Vector2f(prevButton.Position.X + prevButton.GetWidth(), Position.Y);
            nextButton.Position = new Vector2i((int)(currentSetting.Position.X + currentSetting.GetGlobalBounds().Width + 10), Position.Y);

            labelButton.Position -=     new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            prevButton.Position -=      new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            currentSetting.Position -=  new Vector2f((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            nextButton.Position -=      new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));

            labelButton.Draw(w);
            prevButton.Draw(w);
            w.Draw(currentSetting);
            nextButton.Draw(w);

        }

    }
}
