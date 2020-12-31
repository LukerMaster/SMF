using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game.fish
{
    partial class FishBase
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
        public float GetMaxHealth()
        {
            float finalValue = fileData.Health;
            switch (ChassisLvl)
            {
                case 1:
                    finalValue += fileData.Health * 1.1f * fileData.ChassisMult;
                    break;
                case 2:
                    finalValue += fileData.Health * 1.4f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 2.2f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 3.0f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (BodyLvl)
            {
                case 1:
                    finalValue += fileData.Health * 0.4f * fileData.ChassisMult;
                    break;
                case 2:
                    finalValue += fileData.Health * 0.7f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 1.1f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 1.6f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (FinsLvl)
            {
                case 2:
                    finalValue += fileData.Health * 0.05f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 0.1f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 0.25f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            return finalValue;
        }
        public float GetHealthRegen()
        {
            float finalValue = fileData.HealthRegen;
            if (BodyLvl == 3) finalValue += fileData.HealthRegen * (1.1f * fileData.BodyMult) + 200 * fileData.BodyMult;
            else if (BodyLvl == 4) finalValue += fileData.HealthRegen * (1.2f * fileData.BodyMult) + 600 * fileData.BodyMult;
            return finalValue;
        }
        public float GetMaxStamina()
        {
            float finalValue = fileData.Stamina;
            if (engineLvl > 2) finalValue += fileData.Stamina * 1.1f * fileData.EngineMult;
            switch (nitroLvl)
            {
                case 1:
                    finalValue += fileData.Stamina * 0.75f * fileData.NitroMult;
                    break;
                case 2:
                    finalValue += fileData.Stamina * 1.5f * fileData.NitroMult;
                    break;
                case 3:
                    finalValue += fileData.Stamina * 2.25f * fileData.NitroMult;
                    break;
                case 4:
                    finalValue += fileData.Stamina * 3.0f * fileData.NitroMult;
                    break;
            }
            return finalValue;
        }
        public float GetStaminaRegen()
        {
            float finalValue = fileData.StaminaRegen;
            switch (nitroLvl)
            {
                case 1:
                    finalValue *= Math.Clamp((0.75f / fileData.NitroMult), 0, 1);
                    break;
                case 2:
                    finalValue *= Math.Clamp((0.65f / fileData.NitroMult), 0, 1);
                    break;
                case 3:
                    finalValue *= Math.Clamp((0.55f / fileData.NitroMult), 0, 1);
                    break;
                case 4:
                    finalValue *= Math.Clamp((0.45f / fileData.NitroMult), 0, 1);
                    break;
            }
            return finalValue;
        }
        public float GetMaxSpeed()
        {
            float finalValue = fileData.MaxSpeed;
            switch (engineLvl)
            {
                case 1:
                    finalValue += fileData.MaxSpeed * 0.7f * fileData.EngineMult;
                    break;
                case 2:
                    finalValue += fileData.MaxSpeed * 0.9f * fileData.EngineMult;
                    break;
                case 3:
                    finalValue += fileData.MaxSpeed * 1.1f * fileData.EngineMult;
                    break;
                case 4:
                    finalValue += fileData.MaxSpeed * 1.5f * fileData.EngineMult;
                    break;
            }
            switch (finsLvl)
            {
                case 1:
                    finalValue += fileData.MaxSpeed * 0.1f * fileData.FinsMult;
                    break;
                case 2:
                    finalValue += fileData.MaxSpeed * 0.15f * fileData.FinsMult;
                    break;
                case 3:
                    finalValue += fileData.MaxSpeed * 0.25f * fileData.FinsMult;
                    break;
                case 4:
                    finalValue += fileData.MaxSpeed * 0.4f * fileData.FinsMult;
                    break;
            }
            return finalValue;
        }
        public float GetMaxAcceleration()
        {
            float finalForce = fileData.Force;
            float finalMass = fileData.Mass;
            switch (engineLvl)
            {
                case 1:
                    finalForce += fileData.Force * 1.8f * fileData.EngineMult;
                    break;
                case 2:
                    finalForce += fileData.Force * 2.2f * fileData.EngineMult;
                    break;
                case 3:
                    finalForce += fileData.Force * 3.0f * fileData.EngineMult;
                    break;
                case 4:
                    finalForce += fileData.Force * 4.0f * fileData.EngineMult;
                    break;
            }
            switch (finsLvl)
            {
                case 1:
                    finalForce += fileData.Force * 1.3f * fileData.FinsMult;
                    break;
                case 2:
                    finalForce += fileData.Force * 1.5f * fileData.FinsMult;
                    break;
                case 3:
                    finalForce += fileData.Force * 2.0f * fileData.FinsMult;
                    break;
                case 4:
                    finalForce += fileData.Force * 2.5f * fileData.FinsMult;
                    break;
            }
            switch (bodyLvl)
            {
                case 3:
                    finalMass -= fileData.Mass * 0.05f * fileData.BodyMult;
                    break;
                case 4:
                    finalMass -= fileData.Mass * 0.15f * fileData.BodyMult;
                    break;
            }
            return 1000*finalForce/finalMass;
        }
        public float GetFriction()
        {
            float finalValue = fileData.Friction;
            switch (finsLvl)
            {
                case 1:
                    finalValue = Math.Min(finalValue * 1.1f, 0.98f);
                    break;
                case 2:
                    finalValue = Math.Min(finalValue * 1.2f, 0.98f);
                    break;
                case 3:
                    finalValue = Math.Min(finalValue * 1.5f, 0.98f);
                    break;
                case 4:
                    finalValue = Math.Min(finalValue * 2.0f, 0.98f);
                    break;
            }
            return finalValue;
        }
    }
}
