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
        public Tile(string tilename) :
            this(tilename, Vector2.Zero, true)
        {
            
        }

        public Tile(string tilename, Vector2 location, bool hasCollision = true)
        {
            Sprite s = new Sprite("spritesheet.xml", tilename);
            Add(s);
            if (hasCollision)
                Add(new Hitbox(Vector2.Zero, s.Image.Width, s.Image.Height));
            Position = location;
        }
    }
}
