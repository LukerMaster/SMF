using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SMF.game
{
    class AssetManager
    {
        private Texture[] fishTextures = new Texture[256];
        private Texture[] weaponTextures = new Texture[256];
        private Dictionary<EType, Texture[]> textures = new Dictionary<EType, Texture[]>();
        private Texture background;

        public AssetManager()
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
        

        public enum EType
        {
            Fish,
            Weapon,
            Background
        };

    }
}
