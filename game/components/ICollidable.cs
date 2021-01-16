using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    interface ICollidable
    {
        bool isPointColliding(Vector2f point);
        bool isBoxColliding(FloatRect rect);
    }
}
