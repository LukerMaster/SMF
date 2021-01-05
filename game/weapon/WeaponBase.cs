using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SMF.game.weapon
{
    /// <summary>
    /// Base weapon settings class for creating weapons. This class keeps everything needed to instantiate a weapon
    /// but does not contain any concrete fish values like current ammo etc.
    /// Each weapon uses a reference to an object of this class to know what their max values are, what is its texture etc.
    /// If you need to reset weapon to it's starting state, just instantiate new weapon with same WeaponBase object.
    /// </summary>
    public class WeaponBase
    {
        private WeaponFileData fileData;

        public int MaxRange { get => fileData.Range; }
        public uint MaxAmmo { get => fileData.AmmoCount; }
        public int ID { get => fileData.ID; }
        public string Name { get => fileData.Name; }
        public Vector2f Size { get => new Vector2f(fileData.SizeX, fileData.SizeY); }
        public float TimeBetweenFire { get => fileData.TimeBetweenFire; }
        public float ReloadTime { get => fileData.ReloadTime; }
        public float BulletSpeed { get => fileData.BulletSpeed; }

        public WeaponBase(int id, Texture tex)
        {
            ChangeWeaponData(0);
        }

        public void ChangeWeaponData(int id)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(WeaponFileData));
            using (Stream reader = new FileStream("assets/weapons/" + id + ".xml", FileMode.Open))
            {
                fileData = (WeaponFileData)serializer.Deserialize(reader);
            }
        }

        public WeaponBase Copy()
        {
            WeaponBase other = (WeaponBase)this.MemberwiseClone();
            other.fileData = fileData.Copy();
            return other;
        }

        public class WeaponFileData
        {
            public WeaponFileData Copy()
            {
                WeaponFileData other = (WeaponFileData)this.MemberwiseClone();
                return other;
            }
            public int ID = 0;
            public string Name = "";
            public int SizeX = 128;
            public int SizeY = 72;

            public string Type = "None";
            public int Range = 10;
            public int Damage = 10;
            public int BulletSpeed = 0;
            public uint AmmoCount = 1;
            public float TimeBetweenFire = 1;
            public float ReloadTime = 0.5f;
        }
    }
}
