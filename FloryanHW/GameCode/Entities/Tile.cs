using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;

namespace FloryanHW
{
    public class Tile : Entity
    {
        public Tile(string tilename)
        {
            Sprite s = new Sprite("spritesheet.xml", tilename);
            Add(s);
            Add(new Hitbox(Vector2.Zero, s.Image.Width, s.Image.Height));
        }

        public Tile(string tilename, Vector2 location) : this(tilename)
        {
            Position = location;
        }
    }
}
