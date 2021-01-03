using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SMF.engine;

namespace SMF.game.weapon
{
    public class Bullet
    {
        public Vector2f Velocity;
        public bool ToDestroy => new Vector2(Velocity.X, Velocity.Y).LengthSquared() > 1.0f;

        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; }

        public void Draw(RenderWindow w)
        {
        }

        public void Update(float dt, Input input, List<Actor> others)
        {
            
        }
    }
}