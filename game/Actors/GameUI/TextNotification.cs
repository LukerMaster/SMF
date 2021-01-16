using SFBE;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class TextNotification : Actor
    {
        Text mainText = new Text();
        float lifeTime;
        float currentTime;

        public override bool ToDestroy => lifeTime <= 0;
        public TextNotification(string str, float duration = 5.0f)
        {
            lifeTime = duration / 2;
            currentTime = duration;

            mainText.DisplayedString = str;
        }
        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            float offset = (float)(Math.Pow(currentTime / lifeTime - 1, 10));
            mainText.CharacterSize = 120;
            mainText.Origin = new SFML.System.Vector2f(0.0f, 1.0f);
            mainText.Position = new SFML.System.Vector2f(w.GetView().Size.X / 2 - mainText.GetGlobalBounds().Width / 2, (w.GetView().Size.Y / 2) + (offset * w.GetView().Size.Y));
            mainText.Font = (assets as FishAssetManager).GetFont("cute");
            w.Draw(mainText);
        }

        protected override void FixedUpdate(float dt, Level level)
        {
            
        }

        protected override void Update(float dt, Level level)
        {
            currentTime -= dt;
        }
    }
}
