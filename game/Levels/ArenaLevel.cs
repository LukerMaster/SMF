using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using SFBF;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace SMF
{
    class ArenaLevel : Level
    {
        Fish player;
        Fish opponent;

        FishAI fishAI;

        bool gameStarted = false;
        float timeToStart = 4.0f;
        float prevTimeToStart;

        bool gameOver = false;
        float gameOverTime = 0.0f;

        GameSettings gameSettings;

        FishInput input = new FishInput();

        FightTimer timer;

        // Arguments in here are going to be changed. Nothing to worry about.
        public ArenaLevel(Instance data, WindowSettings s, GameSettings gameSettings)
        {
            
            this.gameSettings = gameSettings;
            Settings = s;
            player = new Fish(this.gameSettings.playerFishBase);
            player.weapon = new WeaponBuilder().CreateWeapon(this.gameSettings.selectedWeaponID, player);
            FishBase opponentFishBase;
            if (gameSettings.SameOpponentFish)
                opponentFishBase = new FishBase(player.fishBase.ID);
            else
                opponentFishBase = new FishBase(new Random().Next(0, Int32.Parse(XDocument.Load("assets/champs/FishData.xml").Root.Element("FishAmount").Value)));


            if (gameSettings.SameOpponentUpgrades)
            {
                opponentFishBase.BodyLvl = player.fishBase.BodyLvl;
                opponentFishBase.EngineLvl = player.fishBase.EngineLvl;
                opponentFishBase.ChassisLvl = player.fishBase.ChassisLvl;
                opponentFishBase.FinsLvl = player.fishBase.FinsLvl;
                opponentFishBase.NitrousLvl = player.fishBase.NitrousLvl;
            }
            else
            {
                opponentFishBase.BodyLvl = player.fishBase.BodyLvl + new Random().Next(-1, 2);
                opponentFishBase.EngineLvl = player.fishBase.EngineLvl + new Random().Next(-1, 2);
                opponentFishBase.ChassisLvl = player.fishBase.ChassisLvl + new Random().Next(-1, 2);
                opponentFishBase.FinsLvl = player.fishBase.FinsLvl + new Random().Next(-1, 2);
                opponentFishBase.NitrousLvl = player.fishBase.NitrousLvl + new Random().Next(-1, 2);
            }
            opponentFishBase.tint = new Color((byte)new Random().Next(0, 256), (byte)new Random().Next(0, 256), (byte)new Random().Next(0, 256), 255);
            opponent = new Fish(opponentFishBase);
            
            if (player.weapon is MeleeWeapon)
                opponent.weapon = new WeaponBuilder().CreateWeapon(0, opponent);
            else
                opponent.weapon = new WeaponBuilder().CreateWeapon(new Random().Next(1, 4), opponent);
            fishAI = new FishAI(player.Position);
            InstantiateActor(player);
            InstantiateActor(player.weapon as Actor);
            InstantiateActor(opponent);
            InstantiateActor(opponent.weapon as Actor);
            InstantiateActor(new HealthBar(player));
            InstantiateActor(new HealthBar(opponent));
            int songAmount = Int32.Parse(XDocument.Load("assets/music/songs.xml").Root.Element("Amount").Value);
            if (songAmount != 0)
                InstantiateActor(new ArenaMusicPlayer(new Random().Next(0, songAmount), -4000, this.gameSettings.MasterSoundVolume * this.gameSettings.MusicSoundVolume));

            player.Position = new SFML.System.Vector2f(this.Settings.ViewSize.X * 0.25f, this.Settings.ViewSize.Y * 0.25f);
            opponent.Position = new SFML.System.Vector2f(this.Settings.ViewSize.X * 0.75f, this.Settings.ViewSize.Y * 0.25f);
            opponent.FacingLeft = true;

            ArenaBackground arenaBG = (ArenaBackground)InstantiateActor(new ArenaBackground());

            timer = (FightTimer)InstantiateActor(new FightTimer(gameSettings.FightTimer));
            timer.OnCountdownFinishConstant += () => { player.DamageMultiplier = timer.DamageMult; opponent.DamageMultiplier = timer.DamageMult; };
            timer.OnCountdownFinish += () => { arenaBG.SetTint(new Color(255, 80, 80, 255)); };
        }
        protected override void FixedUpdateScript(float dt, Instance data)
        {
            
        }

        protected override void UpdateScript(float dt, Instance data)
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                List<ArenaMusicPlayer> musicPlayer = GetActorsOfClass<ArenaMusicPlayer>();
                musicPlayer[0].Stop();
                data.InstantiateLevel(new MenuLevel(data, Settings, gameSettings));
                data.DestroyLevel(this);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
                player.TakeDamage(20);


            // Constraint the screen
            float stiffness = 0.6f;
            if (player.Position.X > Settings.ViewSize.X)    player.speed = new SFML.System.Vector2f(-Math.Abs(player.speed.X) * stiffness, player.speed.Y);
            if (player.Position.X < 0)                      player.speed = new SFML.System.Vector2f(Math.Abs(player.speed.X) * stiffness, player.speed.Y);
            if (player.Position.Y > Settings.ViewSize.Y)    player.speed = new SFML.System.Vector2f(player.speed.X, -Math.Abs(player.speed.Y) * stiffness);
            if (player.Position.Y < 0)                      player.speed = new SFML.System.Vector2f(player.speed.X, Math.Abs(player.speed.Y) * stiffness);

            if (opponent.Position.X > Settings.ViewSize.X)  opponent.speed = new SFML.System.Vector2f(-Math.Abs(opponent.speed.X), opponent.speed.Y);
            if (opponent.Position.X < 0)                    opponent.speed = new SFML.System.Vector2f(Math.Abs(opponent.speed.X), opponent.speed.Y);
            if (opponent.Position.Y > Settings.ViewSize.Y)  opponent.speed = new SFML.System.Vector2f(opponent.speed.X, -Math.Abs(opponent.speed.Y) * stiffness);
            if (opponent.Position.Y < 0)                    opponent.speed = new SFML.System.Vector2f(opponent.speed.X, Math.Abs(opponent.speed.Y) * stiffness);

            player.Position = new Vector2f( Math.Clamp(player.Position.X, 0, Settings.ViewSize.X), Math.Clamp(player.Position.Y, 0, Settings.ViewSize.Y));
            opponent.Position = new Vector2f(Math.Clamp(opponent.Position.X, 0, Settings.ViewSize.X), Math.Clamp(opponent.Position.Y, 0, Settings.ViewSize.Y));
            // Game start

            if (!gameStarted)
            {
                timeToStart -= dt;
                if (timeToStart <= 3.25 && prevTimeToStart > 3.25)
                    InstantiateActor(new TextNotification("3", 1));
                if (timeToStart <= 2.25 && prevTimeToStart > 2.25)
                    InstantiateActor(new TextNotification("2", 1));
                if (timeToStart <= 1.25 && prevTimeToStart > 1.25)
                    InstantiateActor(new TextNotification("1", 1));
                if (timeToStart <= 0.25 && prevTimeToStart > 0.25)
                {
                    InstantiateActor(new TextNotification("FIGHT!", 1.0f));
                    timer.Stopped = false;
                    gameStarted = true;
                }
                prevTimeToStart = timeToStart;
            }




            // Input

            if (gameStarted)
            {
                input.LeftPressed = Keyboard.IsKeyPressed(Keyboard.Key.A) || Keyboard.IsKeyPressed(Keyboard.Key.Left);
                input.RightPressed = Keyboard.IsKeyPressed(Keyboard.Key.D) || Keyboard.IsKeyPressed(Keyboard.Key.Right);
                input.UpPressed = Keyboard.IsKeyPressed(Keyboard.Key.W) || Keyboard.IsKeyPressed(Keyboard.Key.Up);
                input.DownPressed = Keyboard.IsKeyPressed(Keyboard.Key.S) || Keyboard.IsKeyPressed(Keyboard.Key.Down);
                input.ReloadPressed = Keyboard.IsKeyPressed(Keyboard.Key.R);
                input.BoostPressed = Keyboard.IsKeyPressed(Keyboard.Key.LShift);
                input.AttackPressed = Mouse.IsButtonPressed(Mouse.Button.Left);
                input.MousePos = MousePos;

                List<Bullet> bullets = GetActorsOfClass<Bullet>();
                List<Vector2f> bulletPositions = new List<Vector2f>();
                foreach (Bullet b in bullets)
                    if (b.owner == player)
                        bulletPositions.Add(b.Position);

                if (opponent.CurrentHealth > 0)
                {
                    double range = 80;
                    if (opponent.weapon is RangedWeapon)
                    {
                        range = (opponent.weapon as RangedWeapon).weaponData.BulletSpeed / 3;
                    }
                    opponent.ReceiveInput(fishAI.GetInput(dt, opponent.Position, player.Position, player.speed, bulletPositions, (float)range, opponent.weapon is MeleeWeapon));
                }
                else
                    opponent.ReceiveInput(new FishInput());

                if (player.CurrentHealth > 0)
                    player.ReceiveInput(input);
                else
                    player.ReceiveInput(new FishInput());
                


                // Game over checking and handling

                if (player.CurrentHealth <= 0 && !gameOver)
                {
                    gameOver = true;
                    InstantiateActor(new TextNotification("You lost!"));
                }
                else if (opponent.CurrentHealth <= 0 && !gameOver)
                {
                    gameOver = true;
                    InstantiateActor(new TextNotification("You won!"));
                }

                if (gameOver)
                {
                    timer.Stopped = true;
                    List<ArenaMusicPlayer> musicPlayer = GetActorsOfClass<ArenaMusicPlayer>();
                    musicPlayer[0].StartFading();
                    gameOverTime += dt;
                }
                if (gameOverTime > 6)
                {
                    List<ArenaMusicPlayer> musicPlayer = GetActorsOfClass<ArenaMusicPlayer>();
                    musicPlayer[0].Stop();
                    data.InstantiateLevel(new ArenaLevel(data, Settings, gameSettings));
                    data.DestroyLevel(this);
                }
            }
        }
    }
}
