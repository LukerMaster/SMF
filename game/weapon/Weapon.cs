using SFML.Graphics;
using SFML.System;
using SMF.engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game.weapon
{
    interface Weapon : Actor
    {
        /// <summary>
        /// Call specific attack function.
        /// </summary>
        /// <param name="type">Used to determine which attack is being used. Default attack type is 0, and you can
        /// see how many attacks are there with "AttacksCount" parameter.</param>
        public void Attack(int type = 0);
        public void Reload();
        public uint AttacksCount { get; }
        public uint CurrentAmmoCount { get; set; }
        public uint MaxAmmoCount { get; }
        
    }
}
