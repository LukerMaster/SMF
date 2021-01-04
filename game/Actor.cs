using SFML.Graphics;
using SFML.System;
using SMF.engine;
using System;

namespace SMF.game
{
    public interface Actor
    {
        public void Update(float dt, Input input, Scene scene, AssetManager assetMgr);
        public void Draw(RenderWindow w);
        public bool ToDestroy { get; }
        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; }
        /// <summary>
        /// Defines on which layer current actor is drawn. Higher means drawn later. Can range from 0 to 8.
        /// </summary>
        public byte DrawLayer { get; }
    }
}
