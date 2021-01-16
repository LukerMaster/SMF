using System;
using System.Collections.Generic;
using System.Text;
using SFBE;
using SFML.Graphics;
using SFML.Window;

namespace SMF
{
    class ArenaLevel : Level
    {
        Fish player;
        int selectedWeapon = 0;
        Fish opponent;

        bool gameStarted = false;
        float timeToStart = 4.0f;
        float prevTimeToStart;

        bool gameOver = false;
        float gameOverTime = 0.0f;

        float musicVolume = 1.0f;

        FishInput input = new FishInput();

        // Arguments in here are going to be changed. Nothing to worry about.
        public ArenaLevel(FishBase fish, Instance data, WindowSettings s, int selectedWeapon, float MusicVolume)
        {
            musicVolume = MusicVolume;

            this.selectedWeapon = selectedWeapon;
            Settings = s;
            player = new Fish(fish);
            player.weapon = new WeaponBuilder().CreateWeapon(selectedWeapon, player);

            opponent = new Fish(new FishBase(new Random().Next(0, 4)));
            opponent.weapon = new WeaponBuilder().CreateWeapon(new Random().Next(0, 4), opponent);
            InstantiateActor(player);
            InstantiateActor(player.weapon as Actor);
            InstantiateActor(opponent);
            InstantiateActor(opponent.weapon as Actor);
            InstantiateActor(new HealthBar(player));
            InstantiateActor(new HealthBar(opponent));
            InstantiateActor(new ArenaMusicPlayer(new Random().Next(0, 8), -4000, musicVolume));

            player.Position = new SFML.System.Vector2f(this.Settings.ViewSize.X * 0.25f, this.Settings.ViewSize.Y * 0.25f);
            opponent.Position = new SFML.System.Vector2f(this.Settings.ViewSize.X * 0.75f, this.Settings.ViewSize.Y * 0.25f);
            opponent.FacingLeft = true;

            InstantiateActor(new ArenaBackground());
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
                data.InstantiateLevel(new MenuLevel(data, Settings, player.fishBase));
                data.DestroyLevel(this);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.F))
                player.TakeDamage(20);

            // Game start

            if (!gameStarted)
            {
                timeToStart -= dt;
                if (timeToStart <= 3 && prevTimeToStart > 3)
                    InstantiateActor(new TextNotification("3", 1));
                if (timeToStart <= 2 && prevTimeToStart > 2)
                    InstantiateActor(new TextNotification("2", 1));
                if (timeToStart <= 1 && prevTimeToStart > 1)
                    InstantiateActor(new TextNotification("1", 1));
                if (timeToStart <= 0 && prevTimeToStart > 0)
                {
                    InstantiateActor(new TextNotification("FIGHT!", 1));
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


                player.ReceiveInput(input);

                


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
                    List<ArenaMusicPlayer> musicPlayer = GetActorsOfClass<ArenaMusicPlayer>();
                    musicPlayer[0].StartFading();
                    gameOverTime += dt;
                }
                if (gameOverTime > 6)
                {
                    List<ArenaMusicPlayer> musicPlayer = GetActorsOfClass<ArenaMusicPlayer>();
                    musicPlayer[0].Stop();
                    data.InstantiateLevel(new ArenaLevel(player.fishBase, data, Settings, selectedWeapon, musicVolume));
                    data.DestroyLevel(this);
                }
            }
        }
    }
}
