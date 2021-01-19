using SFBF;
using SFML.Audio;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SMF
{
    class MenuMusicPlayer : Actor
    {
        Music bgm;
        GameSettings settingsRef;
        public MenuMusicPlayer(GameSettings sets)
        {
            this.settingsRef = sets;
            bgm = new Music("assets/music/menu.ogg");
            bgm.Volume = settingsRef.MasterSoundVolume * settingsRef.MusicSoundVolume * 100;
            bgm.Loop = true;
            bgm.Play();
        }

        public void Stop()
        {
            bgm.Stop();
        }

        protected override void Update(float dt, Level level, AssetManager assets = null)
        {
            bgm.Volume = settingsRef.MasterSoundVolume * settingsRef.MusicSoundVolume * 100;
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets = null)
        {
            
        }

        protected override void Draw(RenderWindow w, Level level = null, AssetManager assets = null)
        {
            
        }
    }
}
