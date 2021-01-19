using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class GameSettings
    {
        public FishBase playerFishBase = new FishBase(0);
        public int selectedWeaponID = 0;
        private float masterSoundVolume = 0.1f;
        private float musicSoundVolume = 1.0f;
        private float sfxSoundVolume = 1.0f;

        public float MasterSoundVolume { get => masterSoundVolume; set => masterSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }
        public float MusicSoundVolume { get => musicSoundVolume; set => musicSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }
        public float SfxSoundVolume { get => sfxSoundVolume; set => sfxSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }
    }
}
