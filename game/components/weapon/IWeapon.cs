using SFBF;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMF
{
    public interface IWeapon
    {
        public void Attack(Level l, Vector2f target);
        public void Reload();
        public string Name { get; }
        public uint DPS { get; }
        public int ID { get; }
        public float Rotation { get; set; }
        public Vector2f Position { get; set; }
    }
}
