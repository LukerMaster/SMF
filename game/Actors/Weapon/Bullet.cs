using System;
using System.Collections.Generic;
using System.Numerics;
using SFBF;
using SFML.Graphics;
using SFML.System;

namespace SMF
{
    public class Bullet : Actor
    {
        /// <summary>
        /// Used to avoid shooting yourself
        /// </summary>
        public Fish owner;
        public Vector2f Velocity;
        public Sprite sprite = new Sprite();
        override public bool ToDestroy => new Vector2(Velocity.X, Velocity.Y).LengthSquared() < 1.0f;

        public byte DrawLayer { get => 0; }

        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; }

        public float Damage { get; set; }

        public Bullet(Vector2f startingPos, Vector2f velocity, float Damage, Fish owner = null)
        {
            this.owner = owner;
            this.Damage = Damage;
            Position = startingPos;
            Velocity = velocity;
            Rotation = 360 * (float)Math.Atan2(velocity.Y, Velocity.X) / (float)(2 * Math.PI);
            sprite = new Sprite();
        }

        protected override void Update(float dt, Level level)
        {
        }

        protected override void FixedUpdate(float dt, Level level)
        {
            Position += Velocity * dt;
            Velocity *= (float)Math.Pow(0.01, dt);
            List<ICollidable> fishes = level.GetActorsOfClass<ICollidable>();
            foreach (ICollidable f in fishes)
            {
                if (f != owner && f.isPointColliding(new Vector2f(sprite.GetGlobalBounds().Left - sprite.GetGlobalBounds().Width / 2, sprite.GetGlobalBounds().Top - sprite.GetGlobalBounds().Height / 2)))
                {
                    (f as Fish).TakeDamage(Damage);
                    level.DestroyActor(this);
                }
                    
                    
            }
        }

        protected override void Draw(RenderWindow w, AssetManager assets)
        {
            sprite.Texture = ((FishAssetManager)assets).GetCustomTexture("assets/misc/bullet.png");
            sprite.Position = Position;
            sprite.Scale = new Vector2f(15 / sprite.Texture.Size.X, 10 / sprite.Texture.Size.Y);
            sprite.Rotation = Rotation;
            w.Draw(sprite);
        }
    }
}