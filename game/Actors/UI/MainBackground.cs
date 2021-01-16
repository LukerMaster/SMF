using SFBF;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace SMF
{
    class MainBackground : Actor
    {

        Sprite bg = new Sprite();
        Sprite title = new Sprite();
        Vector2u Size;

        override public bool ToDestroy { get => false; }

        public MainBackground(Vector2u size)
        {
            DrawOrder = -1;
            Size = size;
        }
        override protected void Draw(RenderWindow w, AssetManager assets)
        {
            FishAssetManager asset = (FishAssetManager)assets;
            bg.Texture = asset.GetCustomTexture("assets/maps/0.png");
            title.Texture = asset.GetCustomTexture("assets/menu/title.png");
            bg.Scale = new Vector2f((float)Size.X / bg.Texture.Size.X, (float)Size.Y / bg.Texture.Size.Y);
            title.Scale = new Vector2f((float)Size.X * 0.8f / title.Texture.Size.X, (float)Size.X * 0.8f / title.Texture.Size.X);
            title.Position = new Vector2f(20, 20);

            w.Draw(bg);
            w.Draw(title);
        }

        override protected void FixedUpdate(float dt, Level level) 
        {
            
        }

        override protected void Update(float dt, Level level)
        {
            
        }
    }
}
