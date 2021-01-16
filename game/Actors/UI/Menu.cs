using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;
using SFBE;

namespace SMF
{
    class Menu : Actor
    {
        private List<MenuComponent> componentList = new List<MenuComponent>();

        public void AddButton(string label, Action ac)
        {
            TextButton temp = new TextButton();
            temp.Position = new Vector2i((int)BottomLeftPos.X, (int)(BottomLeftPos.Y - ButtonHeight * componentList.Count));
            temp.Anchor = new Vector2f(0.0f, 0.5f);
            temp.Height = (int)ButtonHeight;
            temp.Label = label;
            temp.OnClick += ac;
            componentList.Add(temp);
        }
        public void AddButtonSet(string label, Func<String> labelFunction, Action OnClickPrev, Action OnClickNext, int SpaceForTextWidth)
        {
            AdjustButtonSet temp = new AdjustButtonSet();
            temp.Position = new Vector2i((int)BottomLeftPos.X, (int)(BottomLeftPos.Y - ButtonHeight * componentList.Count));
            temp.Anchor = new Vector2f(0.0f, 0.5f);
            temp.Height = (int)ButtonHeight;
            temp.Label = label;
            temp.SetStringFunction(labelFunction);
            temp.SetOnClickPrev(OnClickPrev);
            temp.SetOnClickNext(OnClickNext);
            temp.SpaceForSettingWidth = SpaceForTextWidth;
            componentList.Add(temp);
        }
        public void ClearMenu()
        {
            componentList.Clear();
        }

        string fontName;

        public Menu(string font)
        {
            this.fontName = font;
        }

        int currentlySelected = 0;

        float keyboardCooldown = 0.0f;

        public bool FlipUpDown = false;
        public Vector2f BottomLeftPos = new Vector2f(20, 20);
        public uint ButtonHeight = 100;

        public int CurrentlySelected { get => currentlySelected;
            set 
                {
                    if (value >= componentList.Count) currentlySelected = 0;
                    else if (value < 0) currentlySelected = componentList.Count - 1;
                    else currentlySelected = value;
                }
        }
        float KeyboardCooldown { get => keyboardCooldown; set => keyboardCooldown = Math.Max(0, value); }

        protected override void Update(float dt, Level level)
        {
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Up) && KeyboardCooldown == 0) { if (FlipUpDown) CurrentlySelected--; else CurrentlySelected++; keyboardCooldown = 0.2f; }
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Down) && KeyboardCooldown == 0) { if (FlipUpDown) CurrentlySelected++; else CurrentlySelected--; keyboardCooldown = 0.2f; }

            if (!SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Up) && !SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Down)) KeyboardCooldown = 0;

            for (int i = 0; i < componentList.Count; i++)
            {
                
                if (componentList[i].IsHovered)
                    CurrentlySelected = i;

                componentList[i].Update(dt, new Vector2i((int)level.MousePos.X, (int)level.MousePos.Y),
                    SFML.Window.Mouse.IsButtonPressed(SFML.Window.Mouse.Button.Left),
                    i == CurrentlySelected,
                    SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Enter),
                    SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Left),
                    SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.Right));
            }

            KeyboardCooldown -= dt;
        }

        protected override void FixedUpdate(float dt, Level level)
        {
        }

        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i] is MenuComponentWithText)
                    ((MenuComponentWithText)componentList[i]).font = ((FishAssetManager)assets).GetFont(fontName);
                componentList[i].Draw(w);
            }
        }
    }
}
