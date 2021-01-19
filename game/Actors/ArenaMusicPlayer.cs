using SFBF;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SMF
{
    class ArenaMusicPlayer : Actor
    {
        Music bgm;

        bool isFading = false;
        float FadeOutMult = 1.0f;
        float Volume = 100.0f;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="song"></param>
        /// <param name="offset">In milliseconds</param>
        /// <param name="Volume"></param>
        public ArenaMusicPlayer(int song, int offset, float Volume)
        {
            this.Volume = Volume;
            bgm = new Music("assets/music/" + song + ".ogg");
            bgm.PlayingOffset = Time.FromMilliseconds(Int32.Parse(File.ReadLines("assets/music/timings").Skip(song).Take(1).First()) + offset);
            bgm.Volume = this.Volume * 100;
            bgm.Play();
        }

        public void Stop()
        {
            bgm.Stop();
        }

        public void StartFading()
        {
            isFading = true;
        }

        protected override void Draw(RenderWindow w, Level level, AssetManager assets)
        {
            
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets)
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets)
        {
            if (isFading)
            {
                FadeOutMult -= dt / 3;
            }
            bgm.Volume = Volume * 100 * FadeOutMult;
        }
    }
}
