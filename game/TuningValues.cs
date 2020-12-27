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
                    finalValue += baseData.Health * 0.6f * baseData.ChassisMult;
                    break;
                case 2:
                    finalValue += baseData.Health * 0.7f * baseData.ChassisMult;
                    break;
                case 3:
                    finalValue += baseData.Health * 1.1f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 1.5f * baseData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (BodyLvl)
            {
                case 1:
                    finalValue += baseData.Health * 0.2f * baseData.ChassisMult;
                    break;
                case 2:
                    finalValue += baseData.Health * 0.35f * baseData.ChassisMult;
                    break;
                case 3:
                    finalValue += baseData.Health * 0.45f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 0.7f * baseData.ChassisMult;
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
                    finalValue += baseData.Health * 0.05f * baseData.ChassisMult;
                    break;
                case 4:
                    finalValue += baseData.Health * 0.2f * baseData.ChassisMult;
                    break;
                default:
                    break;
            }
            return finalValue;
        }
        public static float GetHealthRegen(FishData baseData, int BodyLvl)
        {
            float finalValue = baseData.HealthRegen;
            if (BodyLvl == 2) finalValue += baseData.HealthRegen * 1.1f * baseData.BodyMult;
            else if (BodyLvl == 3) finalValue += baseData.HealthRegen * 1.2f * baseData.BodyMult;
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
    }
}
