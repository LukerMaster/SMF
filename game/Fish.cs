using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class Fish
    {
        public Sprite sprite = new Sprite();
        /// <summary>
        /// Data of the fish read from file. Does NOT include tuning values.
        /// </summary>
        public FishData baseData = new FishData();

        public Color tint = new Color(255, 255, 255, 255);

        public Vector2f position = new Vector2f(0, 0);
        public Vector2f speed = new Vector2f(0, 0);
        public Vector2f acceleration = new Vector2f(0, 0);

        public int EngineLvl = 0;
        public int ChassisLvl = 0;
        public int BodyLvl = 0;
        public int FinsLvl = 0;
        public int NitroLvl = 0;

        public int MaxHealth { get => (int)TuningValues.GetMaxHealth(baseData, ChassisLvl, BodyLvl, FinsLvl); }

        public void Update(float dt)
        {
            speed += acceleration;
            position += speed;
        }

        public void Draw(RenderWindow w)
        {
            sprite.Origin = new Vector2f(sprite.Texture.Size.X / 2, sprite.Texture.Size.Y / 2);
            sprite.Position = position;
            sprite.Color = tint;
            w.Draw(sprite);
        }
    }
}
