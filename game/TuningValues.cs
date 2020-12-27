using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    static class TuningValues
    {
        public static string GetLabelForTuningLevel(int lvl)
        {
            switch (lvl)
            {
                case 0:
                    return "Stock";
                case 1:
                    return "Improved";
                case 2:
                    return "Pro";
                case 3:
                    return "Extreme";
                case 4:
                    return "ZumTek";
                default:
                    return "Something went wrong in code.";
            }
        }
        public static float GetMaxHealth(FishData baseData, int ChassisLvl, int BodyLvl, int FinsLvl)
        {
            float finalValue = baseData.Health;
            switch (ChassisLvl)
            {
                case 1:
                    finalValue += baseData.Health * 1.1f * baseData.ChassisMult;
                    break;
                case 2:
                    finalValue += baseData.Health * 1.4f * baseData.ChassisMult;
                    break;
                case 3:
                    finalValue += baseData.Health * 2.2f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 3.0f * baseData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (BodyLvl)
            {
                case 1:
                    finalValue += baseData.Health * 0.4f * baseData.ChassisMult;
                    break;
                case 2:
                    finalValue += baseData.Health * 0.7f * baseData.ChassisMult;
                    break;
                case 3:
                    finalValue += baseData.Health * 1.1f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 1.6f * baseData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (FinsLvl)
            {
                case 2:
                    finalValue += baseData.Health * 0.05f * baseData.ChassisMult;
                    break;
                case 3:
                    finalValue += baseData.Health * 0.1f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 0.25f * baseData.ChassisMult;
                    break;
                default:
                    break;
            }
            return finalValue;
        }
        public static float GetHealthRegen(FishData baseData, int BodyLvl)
        {
            float finalValue = baseData.HealthRegen;
            if (BodyLvl == 3) finalValue += baseData.HealthRegen * (1.1f * baseData.BodyMult) + 200 * baseData.BodyMult;
            else if (BodyLvl == 4) finalValue += baseData.HealthRegen * (1.2f * baseData.BodyMult) + 600 * baseData.BodyMult;
            return finalValue;
        }
        public static float GetMaxStamina(FishData baseData, int engineLvl, int nitroLvl)
        {
            float finalValue = baseData.Stamina;
            if (engineLvl > 2) finalValue += baseData.Stamina * 1.1f * baseData.EngineMult;
            switch (nitroLvl)
            {
                case 1:
                    finalValue += baseData.Stamina * 0.75f * baseData.NitroMult;
                    break;
                case 2:
                    finalValue += baseData.Stamina * 1.5f * baseData.NitroMult;
                    break;
                case 3:
                    finalValue += baseData.Stamina * 2.25f * baseData.NitroMult;
                    break;
                case 4:
                    finalValue += baseData.Stamina * 3.0f * baseData.NitroMult;
                    break;
            }
            return finalValue;
        }
        public static float GetStaminaRegen(FishData baseData, int nitroLvl)
        {
            float finalValue = baseData.StaminaRegen;
            switch (nitroLvl)
            {
                case 1:
                    finalValue *= Math.Clamp((0.75f / baseData.NitroMult), 0, 1);
                    break;
                case 2:
                    finalValue *= Math.Clamp((0.65f / baseData.NitroMult), 0, 1);
                    break;
                case 3:
                    finalValue *= Math.Clamp((0.55f / baseData.NitroMult), 0, 1);
                    break;
                case 4:
                    finalValue *= Math.Clamp((0.45f / baseData.NitroMult), 0, 1);
                    break;
            }
            return finalValue;
        }
        public static float GetMaxSpeed(FishData baseData, int engineLvl, int finsLvl)
        {
            float finalValue = baseData.MaxSpeed;
            switch (engineLvl)
            {
                case 1:
                    finalValue += baseData.MaxSpeed * 0.7f * baseData.EngineMult;
                    break;
                case 2:
                    finalValue += baseData.MaxSpeed * 0.9f * baseData.EngineMult;
                    break;
                case 3:
                    finalValue += baseData.MaxSpeed * 1.1f * baseData.EngineMult;
                    break;
                case 4:
                    finalValue += baseData.MaxSpeed * 1.5f * baseData.EngineMult;
                    break;
            }
            switch (finsLvl)
            {
                case 1:
                    finalValue += baseData.MaxSpeed * 0.1f * baseData.FinsMult;
                    break;
                case 2:
                    finalValue += baseData.MaxSpeed * 0.15f * baseData.FinsMult;
                    break;
                case 3:
                    finalValue += baseData.MaxSpeed * 0.25f * baseData.FinsMult;
                    break;
                case 4:
                    finalValue += baseData.MaxSpeed * 0.4f * baseData.FinsMult;
                    break;
            }
            return finalValue / 2;
        }
        public static float GetMaxAcceleration(FishData baseData, int engineLvl, int finsLvl, int bodyLvl)
        {
            float finalForce = baseData.Force;
            float finalMass = baseData.Mass;
            switch (engineLvl)
            {
                case 1:
                    finalForce += baseData.Force * 0.1f * baseData.EngineMult;
                    break;
                case 2:
                    finalForce += baseData.Force * 0.15f * baseData.EngineMult;
                    break;
                case 3:
                    finalForce += baseData.Force * 0.3f * baseData.EngineMult;
                    break;
                case 4:
                    finalForce += baseData.Force * 0.45f * baseData.EngineMult;
                    break;
            }
            switch (finsLvl)
            {
                case 1:
                    finalForce += baseData.Force * 0.1f * baseData.FinsMult;
                    break;
                case 2:
                    finalForce += baseData.Force * 0.15f * baseData.FinsMult;
                    break;
                case 3:
                    finalForce += baseData.Force * 0.2f * baseData.FinsMult;
                    break;
                case 4:
                    finalForce += baseData.Force * 0.3f * baseData.FinsMult;
                    break;
            }
            switch (bodyLvl)
            {
                case 3:
                    finalMass -= baseData.Mass * 0.05f * baseData.BodyMult;
                    break;
                case 4:
                    finalMass -= baseData.Mass * 0.15f * baseData.BodyMult;
                    break;
            }
            return 1000*finalForce/finalMass;
        }
    }
}
