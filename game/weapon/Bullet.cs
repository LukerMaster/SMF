using System;
using System.Collections.Generic;
using System.Numerics;
using SFML.Graphics;
using SFML.System;
using SMF.engine;
using SMF.game.fish;

namespace SMF.game.weapon
{
    public class Bullet : Actor
    {
        public Vector2f Velocity;
        public Sprite sprite = new Sprite();
        public bool ToDestroy => new Vector2(Velocity.X, Velocity.Y).LengthSquared() < 1.0f;

        public byte DrawLayer { get => 0; }

        public Vector2f Position { get; set; }
        public Vector2f Scale { get; set; }
        public float Rotation { get; set; }



        public Bullet(Vector2f startingPos, Vector2f velocity, AssetManager assets)
        {
            Position = startingPos;
            Velocity = velocity;
            Rotation = 360 * (float)Math.Atan2(velocity.Y, Velocity.X) / (float)(2 * Math.PI);
            sprite = new Sprite(assets.GetCustomTexture("assets/misc/bullet.png"));
        }

        public void Update(float dt, Scene scene, AssetManager assets)
        {
            List<Actor> Targets = scene.GetActorsOfClass(typeof(Fish));
            
            Position += Velocity * dt;
            Velocity *= (float)Math.Pow(0.01, dt);
        }

        public void Draw(RenderWindow w)
        {
            sprite.Position = Position;
            sprite.Scale = new Vector2f(15 / sprite.Texture.Size.X, 10 / sprite.Texture.Size.Y);
            sprite.Rotation = Rotation;
            w.Draw(sprite);
        }

        public void ReceiveInput(Input input)
        {
            
        }
    }
}