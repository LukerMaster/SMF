using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SMF.game.fish
{
    class AssetManager
    {
        private Texture[] fishTextures = new Texture[256];
        private Texture[] weaponTextures = new Texture[256];
        private Texture background;

        public Texture GetBackground(int id)
        {
            if (background != null)
                return background;
            else
            {
                background = new Texture("assets/maps/" + id + ".png");
                return background;
            }
        }
        public void LoadAllFishTextures()
        {
            for (int i = 0; i < 256; i++)
            {
                if (File.Exists("assets/champs/" + i + ".png"))
                {
                    fishTextures[i] = new Texture("assets/champs/" + i + ".png");
                }
            }
        }
        public void LoadFishTextureByID(int id)
        {
            fishTextures[id] = new Texture("assets/champs/" + id + ".png");
        }
        public Texture GetFishTexture(int id)
        {
            if (fishTextures[id] != null)
                return fishTextures[id];
            else
            {
                LoadFishTextureByID(id);
                return fishTextures[id];
            }
        }
    }
}
