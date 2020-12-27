using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    public class FishData
    {
        public int ID = 0;
        public string Name = "";
        public int SizeX = 192;
        public int SizeY = 108;

        public int Health = 1000;
        public int HealthRegen = 0;
        public int HealthRegenCooldown = 0;
        public int Stamina = 2000;
        public int StaminaRegen = 500;
        public int StaminaRegenCooldown = 1000;

        public int MaxSpeed = 1000;
        public int Acceleration = 1000;
        public int Jerk = 1000;
        public float Friction = 0.5f;

        public float EngineMult = 1;
        public float ChassisMult = 1;
        public float BodyMult = 1;
        public float FinsMult = 1;
        public float NitroMult = 1;
    }
}
