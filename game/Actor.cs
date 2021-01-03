using SFML.Graphics;
using SFML.System;
using SMF.engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    public interface Actor
    {
        public void Update(float dt, Input input, List<Actor> actors);
        public void Draw(RenderWindow w);
        public bool ToDestroy { get; }
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; }
    }
}
