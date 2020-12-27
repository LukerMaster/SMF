using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.engine.UI
{
    interface MenuComponent
    {
        bool IsHovered { get; }
        void Update(float dt, Vector2i mousePos, bool isMousePressed, bool isSelected, bool isEnterPressed, bool isLeftPressed, bool isRightPressed);
        void Draw(RenderWindow w);
    }
}
