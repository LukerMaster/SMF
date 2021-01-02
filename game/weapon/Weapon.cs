using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game.weapon
{
    interface Weapon
    {
        /// <summary>
        /// Function to update weapon's statistics.
        /// </summary>
        /// <param name="dt">Delta time</param>
        /// <param name="pos">Position of the weapon</param>
        /// <param name="aimPos">Position of the aimed target. Used to determine rotation.</param>
        public void Update(float dt, Vector2f pos, Vector2f aimPos, List<GameObject> others);
        /// <summary>
        /// Call specific attack function.
        /// </summary>
        /// <param name="type">Used to determine which attack is being used. Default attack type is 0, and you can
        /// see how many attacks are there with "AttacksCount" parameter.</param>
        public void Attack(int type = 0);
        public void Reload();
        public void Draw(RenderWindow w);
        public uint AttacksCount { get; }
        public uint CurrentAmmoCount { get; set; }
        public uint MaxAmmoCount { get; }
    }
}
