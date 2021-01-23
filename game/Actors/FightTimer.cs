using SFBF;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    class FightTimer : Actor
    {
        float timeLeft;
        public Action OnCountdownFinish;
        public Action OnCountdownFinishConstant;
        bool ActionPerformed = false;
        public bool Stopped { get; set; } = true;
        public FightTimer(float time)
        {
            timeLeft = time;
        }

        public float DamageMult { get => (float)Math.Round(2 + 0.2 * Math.Abs(timeLeft), 1); }

        Text timer = new Text();
        protected override void Draw(RenderWindow w, Level level = null, AssetManager assets = null)
        {
            timer.Font = (assets.Assets as FishAssetBox).GetFont("cute");
            timer.Origin = new SFML.System.Vector2f(0.5f * timer.GetLocalBounds().Width, 0.0f);
            timer.Position = new SFML.System.Vector2f(level.Settings.ViewSize.X / 2, 20);
            timer.CharacterSize = 70;
            timer.FillColor = new Color(255, 255, 255, 170);
            if (timeLeft > 0)
                timer.DisplayedString = ((int)timeLeft).ToString();
            else
            {
                timer.DisplayedString = "Sudden Death! Damage: " + DamageMult + "x";
                timer.FillColor = new Color(255, Math.Max((byte)0, (byte)(200 - DamageMult * 10)), 0, 220);
                timer.CharacterSize = 70 + (uint)DamageMult;
            }
                
            w.Draw(timer);
        }

        protected override void FixedUpdate(float dt, Level level, AssetManager assets = null)
        {
            
        }

        protected override void Update(float dt, Level level, AssetManager assets = null)
        {
            if (!Stopped)
                timeLeft -= dt;
            if (timeLeft < 0.0f)
            {
                OnCountdownFinishConstant?.Invoke();
                if (!ActionPerformed)
                {
                    OnCountdownFinish?.Invoke();
                    ActionPerformed = true;
                }
                
            }
        }
    }
}
