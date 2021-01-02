using SFML.Graphics;
using SMF.engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    interface GameObject
    {
        public void Update(float dt, Input input, List<GameObject> others);
        public void Draw(RenderWindow w);
        public bool ToDestroy { get; }
    }
}
