using System;
using System.Collections.Generic;
using System.Text;
using SFML.Graphics;
using SMF.engine;

namespace SMF.engine
{
    interface IGameState
    {
        void Update(float dt);
        void Draw();
        bool IsDisposable { get; set; }
        bool IsVisible { get; set; }
    }
}
