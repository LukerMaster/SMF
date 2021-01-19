using SFML.Graphics;
using System.Collections.Generic;
using SFBF;
using SFML.Audio;

namespace SMF
{
    public class FishAssetManager : AssetManager
    {
        private Dictionary<EType, Texture[]> textures = new Dictionary<EType, Texture[]>();
        private Dictionary<string, Texture> customTextures = new Dictionary<string, Texture>();
        private Dictionary<string, SoundBuffer> customSounds = new Dictionary<string, SoundBuffer>();
        private Dictionary<string, Font> fonts = new Dictionary<string, Font>();

        public FishAssetManager()
        {
            textures.Add(EType.Background, new Texture[256]);
            textures.Add(EType.Fish, new Texture[256]);
            textures.Add(EType.Weapon, new Texture[256]);
        }

        public void LoadByID(EType type, int id)
        {
            string path = "";
            switch (type)
            {
                case EType.Fish:
                    path = "assets/champs/" + id + ".png";
                    break;
                case EType.Weapon:
                    path = "assets/weapons/" + id + ".png";
                    break;
                case EType.Background:
                    path = "assets/maps/" + id + ".png";
                    break;
            }
            textures[type][id] = new Texture(path);
        }
        public Texture GetByID(EType type, int id)
        {
            if (textures[type][id] != null)
                return textures[type][id];
            else
            {
                LoadByID(type, id);
                return textures[type][id];
            }
        }
        public Texture GetCustomTexture(string path)
        {
            if (!customTextures.ContainsKey(path))
                customTextures.Add(path, new Texture(path));

            return customTextures[path];
        }

        public SoundBuffer GetCustomSoundBuffer(string path)
        {
            if (!customSounds.ContainsKey(path))
                customSounds.Add(path, new SoundBuffer(path));

            return customSounds[path];
        }

        public Font GetFont(string name)
        {
            if (!fonts.ContainsKey(name))
                fonts.Add(name, new Font("assets/fonts/" + name + ".mlg"));

            return fonts[name];
        }

        public void UnloadAllAssets()
        {
            textures = new Dictionary<EType, Texture[]>();
            customTextures = new Dictionary<string, Texture>();
            fonts = new Dictionary<string, Font>();
        }

        public enum EType
        {
            Fish,
            Weapon,
            Background
        };

    }
}
