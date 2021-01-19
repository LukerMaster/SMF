using SFBF;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class ArenaBackground : Actor
    {
        Sprite background = new Sprite();
        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            DrawOrder = -2;
            background.Texture = (assets as FishAssetManager).GetByID(FishAssetManager.EType.Background, 0);
            background.Scale = new SFML.System.Vector2f((float)w.GetView().Size.X / background.Texture.Size.X, (float)w.GetView().Size.Y / background.Texture.Size.Y);
            w.Draw(background);
            
        }

        public void SetTint(Color Tint)
        {
            background.Color = Tint;
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {

        }
    }
}
