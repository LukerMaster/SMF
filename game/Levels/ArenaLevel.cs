using System;
using System.Collections.Generic;
using System.Text;
using SFBE;
using SFML.Window;

namespace SMF
{
    class ArenaLevel : Level
    {
        Fish player;

        FishInput input = new FishInput();
        public ArenaLevel(FishBase fish, Instance data, WindowSettings s)
        {
            Settings = s;
            player = new Fish(fish, ((FishAssetManager)data.assets).GetByID(FishAssetManager.EType.Fish, fish.ID));
            InstantiateActor(player);
        }
        protected override void FixedUpdateScript(float dt, Instance data)
        {
            
        }

        protected override void UpdateScript(float dt, Instance data)
        {
            input.LeftPressed =     Keyboard.IsKeyPressed(Keyboard.Key.A);
            input.RightPressed =    Keyboard.IsKeyPressed(Keyboard.Key.D);
            input.UpPressed =       Keyboard.IsKeyPressed(Keyboard.Key.W);
            input.DownPressed =     Keyboard.IsKeyPressed(Keyboard.Key.S);
            input.ReloadPressed =   Keyboard.IsKeyPressed(Keyboard.Key.R);
            input.BoostPressed =    Keyboard.IsKeyPressed(Keyboard.Key.LShift);
            input.AttackPressed =   Mouse.IsButtonPressed(Mouse.Button.Left);

            player.ReceiveInput(input);

            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape))
            {
                data.InstantiateLevel(new MenuLevel(data, Settings));
                data.DestroyLevel(this);
            }
        }
    }
}
