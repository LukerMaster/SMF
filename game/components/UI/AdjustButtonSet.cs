using SFBF;
using SFML.Graphics;
using SFML.System;
using System;

namespace SMF
{
    class AdjustButtonSet : MenuComponentWithText
    {
        TextButton labelButton;

        TextButton prevButton;
        TextButton nextButton;

        Text currentSetting = new Text();

        private Func<String> UpdateSetting;

        public String Label;

        public int Height;
        public int SpaceForSettingWidth;

        public Font font;

        public Vector2f Anchor;

        public Vector2i Position;

        public bool IsHovered { get { return labelButton.IsHovered || prevButton.IsHovered || nextButton.IsHovered; } }

        Font MenuComponentWithText.font { get => font; set => font = value; }

        public void SetStringFunction(Func<String> f)
        {
            UpdateSetting = f;
            UpdateSetting();
        }

        public void SetOnClickPrev(Action f)
        {
            prevButton.OnClick += f;
        }
        public void SetOnClickNext(Action f)
        {
            nextButton.OnClick += f;
        }
        public void SetOnEnterFocus(Action f)
        {
            nextButton.OnEnterFocus += f;
            prevButton.OnEnterFocus += f;
            labelButton.OnEnterFocus += f;
        }
        public void SetOnFocusConstant(Action f)
        {
            nextButton.OnFocusConstant += f;
            prevButton.OnFocusConstant += f;
            labelButton.OnFocusConstant += f;
        }
        public AdjustButtonSet()
        {

            labelButton = new TextButton();
            prevButton = new TextButton();
            nextButton = new TextButton();
        }

        public void Draw(RenderWindow w)
        {
            currentSetting.Font = font;
            prevButton.font = font;
            nextButton.font = font;
            labelButton.font = font;

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
            nextButton.Position = new Vector2i((int)(prevButton.Position.X + prevButton.GetWidth() + SpaceForSettingWidth), Position.Y);
            currentSetting.Position = new Vector2f(prevButton.Position.X + prevButton.GetWidth() + (SpaceForSettingWidth - currentSetting.GetGlobalBounds().Width) / 2, Position.Y);

            labelButton.Position -=     new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            prevButton.Position -=      new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            currentSetting.Position -=  new Vector2f((int)(width * Anchor.X), (int)(Height * Anchor.Y));
            nextButton.Position -=      new Vector2i((int)(width * Anchor.X), (int)(Height * Anchor.Y));

            labelButton.Draw(w);
            prevButton.Draw(w);
            w.Draw(currentSetting);
            nextButton.Draw(w);

        }

        void MenuComponent.Update(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected, bool isEnterPressed, bool isLeftPressed, bool isRightPressed)
        {
            labelButton.Update(dt, mousePos, false, isSelected, false);
            nextButton.Update(dt, mousePos, isMousePressed, isSelected, isRightPressed);
            prevButton.Update(dt, mousePos, isMousePressed, isSelected, isLeftPressed);
        }
    }
}
