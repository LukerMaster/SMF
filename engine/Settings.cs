using System;
using SFML.System;
using SMF.game;
using SMF.game.fish;
using SMF.game.weapon;

public enum EResolution
{
    E640X480,
    E800X600,
    E1280X720,
    E1366X768,
    E1440X900,
    E1600X900,
    E1920X1080,
    E2160X1440,
    E3840X2160
}

namespace SMF.engine
{
    public class Settings
    {
        private EResolution resolution = EResolution.E1280X720;

        private float masterSoundVolume = 1.0f;

        private float musicSoundVolume = 1.0f;

        private float sfxSoundVolume = 1.0f;

        public EResolution Resolution { get => resolution; set
            {
                resolution = (EResolution)Math.Clamp((byte)value, (byte)EResolution.E640X480, (byte)EResolution.E3840X2160);
                shouldWindowBeRecreated = true;
            }
        }

        public bool Fullscreen { get => fullscreen; set
            {
                fullscreen = value;
                shouldWindowBeRecreated = true;
            }
        }

        public float MasterSoundVolume { get => masterSoundVolume; set => masterSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }
        public float MusicSoundVolume { get => musicSoundVolume; set => musicSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }
        public float SfxSoundVolume { get => sfxSoundVolume; set => sfxSoundVolume = Math.Clamp(value, 0.0f, 1.0f); }

        public bool isGameOn = true;

        public string windowName = "SMF";

        public bool shouldWindowBeRecreated = false;

        public Vector2u playfieldSize = new Vector2u(1920, 1080);

        public bool stretched = false;

        private bool fullscreen = false;

        public Vector2u GetScreenSize()
        {
            switch (Resolution)
            {
                case EResolution.E640X480:
                    return new Vector2u(640, 480);
                case EResolution.E800X600:
                    return new Vector2u(800, 600);
                case EResolution.E1280X720:
                    return new Vector2u(1280, 720);
                case EResolution.E1366X768:
                    return new Vector2u(1366, 768);
                case EResolution.E1440X900:
                    return new Vector2u(1440, 900);
                case EResolution.E1600X900:
                    return new Vector2u(1600, 900);
                case EResolution.E1920X1080:
                    return new Vector2u(1920, 1080);
                case EResolution.E2160X1440:
                    return new Vector2u(2160, 1440);
                case EResolution.E3840X2160:
                    return new Vector2u(3840, 2160);
                default:
                    return new Vector2u(640, 480);
            }
        }

        public FishBase selectedFishBase;
        public WeaponBase selectedWeaponBase;
    }
}
