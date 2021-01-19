using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
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
                    finalValue += fileData.Health * 0.4f * fileData.ChassisMult;
                    break;
                case 2:
                    finalValue += fileData.Health * 0.7f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 1.0f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 1.25f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (BodyLvl)
            {
                case 1:
                    finalValue += fileData.Health * 0.1f * fileData.ChassisMult;
                    break;
                case 2:
                    finalValue += fileData.Health * 0.2f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 0.25f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 0.4f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            switch (FinsLvl)
            {
                case 2:
                    finalValue += fileData.Health * 0.02f * fileData.ChassisMult;
                    break;
                case 3:
                    finalValue += fileData.Health * 0.04f * fileData.ChassisMult;
                    break;
                case 4:
                    finalValue += fileData.Health * 0.1f * fileData.ChassisMult;
                    break;
                default:
                    break;
            }
            return finalValue;
        }
        public float GetHealthRegen()
        {
            float finalValue = fileData.HealthRegen;
            if (BodyLvl == 3) finalValue += fileData.HealthRegen * (1.1f * fileData.BodyMult) + 20 * fileData.BodyMult;
            else if (BodyLvl == 4) finalValue += fileData.HealthRegen * (1.2f * fileData.BodyMult) + 60 * fileData.BodyMult;
            return finalValue;
        }
        public float GetMaxNitrousForce()
        {
            float finalValue = fileData.NitrousForce;
            if (engineLvl > 2) finalValue += fileData.NitrousForce * 1.1f * fileData.EngineMult;
            switch (nitroLvl)
            {
                case 1:
                    finalValue += fileData.NitrousForce * 2.0f * fileData.NitrousMult;
                    break;
                case 2:
                    finalValue += fileData.NitrousForce * 4.0f * fileData.NitrousMult;
                    break;
                case 3:
                    finalValue += fileData.NitrousForce * 6.0f * fileData.NitrousMult;
                    break;
                case 4:
                    finalValue += fileData.NitrousForce * 12.0f * fileData.NitrousMult;
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
        public float GetMaxForce()
        {
            float finalForce = fileData.Force;
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
            return finalForce;
        }
        public float GetMass()
        {
            float finalMass = fileData.Mass;
            switch (bodyLvl)
            {
                case 3:
                    finalMass -= fileData.Mass * 0.05f * fileData.BodyMult;
                    break;
                case 4:
                    finalMass -= fileData.Mass * 0.15f * fileData.BodyMult;
                    break;
            }
            return finalMass;
        }
        public float GetMaxNitrous()
        {
            float finalValue = fileData.Nitrous;
            switch (nitroLvl)
            {
                case 1:
                    finalValue += fileData.Nitrous * 1.0f;
                    break;
                case 2:
                    finalValue += fileData.Nitrous * 1.25f;
                    break;
                case 3:
                    finalValue += fileData.Nitrous * 1.5f;
                    break;
                case 4:
                    finalValue += fileData.Nitrous * 2.0f;
                    break;
            }
            return finalValue;
        }
        public float GetFriction()
        {
            float finalValue = fileData.Friction;
            switch (finsLvl)
            {
                case 1:
                    finalValue = Math.Min(finalValue * 1.1f, 0.99f);
                    break;
                case 2:
                    finalValue = Math.Min(finalValue * 1.2f, 0.99f);
                    break;
                case 3:
                    finalValue = Math.Min(finalValue * 1.5f, 0.99f);
                    break;
                case 4:
                    finalValue = Math.Min(finalValue * 2.0f, 0.99f);
                    break;
            }
            return finalValue;
        }
    }
}
