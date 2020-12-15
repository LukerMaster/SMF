using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class Menu
    {
        public List<MenuComponent> componentList = new List<MenuComponent>();
        public List<Text> textList = new List<Text>();

        int currentlySelected = 0;

        float keyboardCooldown = 0.0f;

        public bool FlipUpDown = false;

        public int CurrentlySelected { get => currentlySelected;
            set 
                {
                    if (value >= componentList.Count) currentlySelected = 0;
                    else if (value < 0) currentlySelected = componentList.Count - 1;
                    else currentlySelected = value;
                }
        }

        float KeyboardCooldown { get => keyboardCooldown; set => keyboardCooldown = Math.Max(0, value); }

        public void Update(float dt, Vector2i mousePos, bool isMousePressed, bool isEnterPressed, bool isUpPressed, bool isDownPressed, bool isLeftPressed, bool isRightPressed)
        {
            if (isUpPressed && KeyboardCooldown == 0) { if (FlipUpDown) CurrentlySelected++; else CurrentlySelected--; keyboardCooldown = 0.2f; }
            if (isDownPressed && KeyboardCooldown == 0) { if (FlipUpDown) CurrentlySelected--; else CurrentlySelected++; keyboardCooldown = 0.2f; }

            if (!isUpPressed && !isDownPressed) KeyboardCooldown = 0;

            for (int i = 0; i < componentList.Count; i++)
            {
                if (componentList[i].IsHovered)
                    CurrentlySelected = i;

                componentList[i].Update(dt, mousePos, isMousePressed, i == CurrentlySelected, isEnterPressed, isLeftPressed, isRightPressed);
            }

            KeyboardCooldown -= dt;
        }

        public void Draw(RenderWindow w)
        {
            for (int i = 0; i < componentList.Count; i++)
            {
                componentList[i].Draw(w);
            }
        }
    }
}
