﻿using SFML.Graphics;
using SFML.System;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SMF
{
    /// <summary>
    /// Base fish settings class for creating fishes. This class keeps everything needed to instantiate fish
    /// but does not contain any concrete fish values like position, current health etc.
    /// Each fish uses a reference to an object of this class to know what their max values are like health, stamina etc.
    /// If you need to reset fish to it's starting state, just instantiate new fish with same FishBase object.
    /// </summary>
    public partial class FishBase
    {
        private static readonly int MAX_UPGRADE_LVL = 4;
        private FishFileData fileData = new FishFileData();
        

        private int engineLvl = 0;
        private int chassisLvl = 0;
        private int bodyLvl = 0;
        private int finsLvl = 0;
        private int nitroLvl = 0;

        public Color tint = new Color(255, 255, 255, 255);
        public int EngineLvl { get => engineLvl; set => engineLvl = Math.Clamp(value, 0, MAX_UPGRADE_LVL); }
        public int ChassisLvl { get => chassisLvl; set => chassisLvl = Math.Clamp(value, 0, MAX_UPGRADE_LVL); }
        public int BodyLvl { get => bodyLvl; set => bodyLvl = Math.Clamp(value, 0, MAX_UPGRADE_LVL); }
        public int FinsLvl { get => finsLvl; set => finsLvl = Math.Clamp(value, 0, MAX_UPGRADE_LVL); }
        public int NitrousLvl { get => nitroLvl; set => nitroLvl = Math.Clamp(value, 0, MAX_UPGRADE_LVL); }

        public int MaxHealth { get => (int)GetMaxHealth(); }
        public int MaxHealthRegen { get => (int)GetHealthRegen(); }
        public float MaxHealthRegenCooldown { get => fileData.HealthRegenCooldown; }
        public int MaxNitrous { get => (int)GetMaxNitrous(); }
        public int MaxNitrousForce { get => (int)GetMaxNitrousForce(); }
        public float MaxNitrousRegenCooldown { get => fileData.NitrousRegenCooldown; }
        public int MaxSpeed { get => (int)GetMaxSpeed(); }
        public int MaxForce { get => (int)GetMaxForce(); }
        public int Mass { get => (int)GetMass(); }
        public float Friction { get => GetFriction(); }
        public Vector2f Size { get => new Vector2f(fileData.SizeX, fileData.SizeY); }
        public bool EngineUpgradable { get => fileData.EngineMult != 0; }
        public bool ChassisUpgradable { get => fileData.ChassisMult != 0; }
        public bool BodyUpgradable { get => fileData.BodyMult != 0; }
        public bool FinsUpgradable { get => fileData.FinsMult != 0; }
        public bool NitrousUpgradable { get => fileData.NitrousMult != 0; }
        public string Name { get => fileData.Name; }
        public int ID { get => fileData.ID; }

        public FishBase(int id)
        {
            ChangeFishData(id);
        }

        public void ChangeFishData(int id)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(FishFileData));
            using (Stream reader = new FileStream("assets/champs/" + id + ".xml", FileMode.Open))
            {
                fileData = (FishFileData)serializer.Deserialize(reader);
            }
        }

        public FishBase Copy()
        {
            FishBase other = (FishBase)this.MemberwiseClone();
            other.fileData = fileData.Copy();
            return other;
        }
        public class FishFileData
        {
            public FishFileData Copy()
            {
                FishFileData other = (FishFileData)this.MemberwiseClone();
                return other;
            }

            public int ID = 0;
            public string Name = "";
            public int SizeX = 128;
            public int SizeY = 72;

            public int Health = 1000;
            public int HealthRegen = 0;
            public float HealthRegenCooldown = 0;
            public int Nitrous = 1000;
            public int NitrousRegen = 500;
            public float NitrousRegenCooldown = 1000;

            public int MaxSpeed = 500;
            public int Force = 50;
            public int NitrousForce = 50;
            public int Mass = 1000;
            public float Friction = 0.5f;

            public float EngineMult = 1;
            public float ChassisMult = 1;
            public float BodyMult = 1;
            public float FinsMult = 1;
            public float NitrousMult = 1;
        }
    }
}
