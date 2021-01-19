using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class FishAI
    {
        float timeUsingNitrous = 0.0f;
        float maxTimeUsingNitrous = 1.0f;
        float currentTimeNotUsingNitrous = 0.0f;
        float maxTimeNotUsingNitrous = 5.0f;
        float currentRotation = 0.0f;

        float currentTargetTime = 0.0f;

        Vector2f currentTarget;
        Vector2f previousOpponentPos;
        public FishAI(Vector2f opponentPos)
        {
        }
        public FishInput GetInput(float dt, Vector2f currentPos, Vector2f opponentPos, Vector2f opponentSpeed, List<Vector2f> bulletPosList, float weaponRange, bool melee = false)
        {
            FishInput input = new FishInput();
            if (!melee)
            {
                input.MousePos = opponentPos + opponentSpeed * 0.1f;
                if (Math.Pow(currentPos.X - opponentPos.X, 2) + Math.Pow(currentPos.Y - opponentPos.Y, 2) < weaponRange * weaponRange)
                {
                    input.AttackPressed = true;
                }

                if (currentTargetTime < 0.0f)
                    GetNewTarget(currentPos, opponentPos);
            }
            else
            {
                if (Math.Pow(currentPos.X - opponentPos.X, 2) + Math.Pow(currentPos.Y - opponentPos.Y, 2) < 250000)
                {
                    input.MousePos = currentPos + new Vector2f((float)(Math.Cos(currentRotation) - Math.Sin(currentRotation)) * 100, (float)(Math.Sin(currentRotation) + Math.Cos(currentRotation)) * 100);
                    currentRotation += dt * 30;
                }
                else
                    input.MousePos = opponentPos;

                if (currentTargetTime < 0.0f)
                SetTarget(opponentPos);
            }

            // Nitrous
            if (Math.Pow(currentPos.X - currentTarget.X, 2) + Math.Pow(currentPos.Y - currentTarget.Y, 2) > 640000 && timeUsingNitrous < maxTimeUsingNitrous)
            {
                currentTimeNotUsingNitrous = 0;
                input.BoostPressed = true;
                timeUsingNitrous += dt;
            }
            currentTimeNotUsingNitrous += dt;
            if (currentTimeNotUsingNitrous > maxTimeNotUsingNitrous)
            {
                timeUsingNitrous = 0;
            }

            // Dodging bullets
            if (bulletPosList != null)
            {
                double distanceToTarget = Math.Pow(currentPos.X - currentTarget.X, 2) + Math.Pow(currentPos.Y - currentTarget.Y, 2);
                double rotation = Math.Atan2(currentPos.Y - currentTarget.Y, currentPos.X - currentTarget.X);
                foreach (Vector2f position in bulletPosList)
                {
                    double distanceToBullet = Math.Pow(currentPos.X - position.X, 2) + Math.Pow(currentPos.Y - position.Y, 2);
                    double bulletRotation = Math.Atan2(currentPos.Y - position.Y, currentPos.X - position.X);
                    if (Math.Abs(rotation - bulletRotation) < Math.PI / 12 && distanceToBullet < 0.8f * distanceToTarget)
                    {
                        GetEscapeTarget(currentPos, position);
                        timeUsingNitrous = 0.0f;
                    }
                        
                }
            }

            // Moving towards target
            if (currentPos.X < currentTarget.X) input.RightPressed = true;
            if (currentPos.X > currentTarget.X) input.LeftPressed = true;
            if (currentPos.Y < currentTarget.Y) input.DownPressed = true;
            if (currentPos.Y > currentTarget.Y) input.UpPressed = true;

            

            currentTargetTime -= dt;

            return input;
        }
        private void GetNewTarget(Vector2f currentPos, Vector2f opponentPos)
        {
            currentTarget = new Vector2f(opponentPos.X + new Random().Next(-700, 700), opponentPos.Y + new Random().Next(-700, 700));
            currentTargetTime = 2.0f;
        }
        private void GetEscapeTarget(Vector2f currentPos, Vector2f bulletPos)
        {
            currentTarget = new Vector2f(2 * currentPos.X - bulletPos.X * 0.33f + new Random().Next(-100, 100), 2 * currentPos.Y - bulletPos.Y * 0.33f + new Random().Next(-100, 100));
            currentTargetTime = 0.75f;
        }
        private void SetTarget(Vector2f target)
        {
            currentTarget = target;
            currentTargetTime = 0.1f;
        }
    }
}
