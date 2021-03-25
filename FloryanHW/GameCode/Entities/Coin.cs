using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;

namespace FloryanHW
{
    public class Coin : Entity
    {
        public Coin(Vector2 position) : base(position.X, position.Y)
        {
            AnimatedSprite coinSprite = new AnimatedSprite(@"spritesheet0.png", @"spritesheet.xml", @"Animations.xml", @"Coin");
            Add(coinSprite);
            coinSprite.Play("idle");
            Transform.Scale = 5;
        }

        public Coin()
            : this(Vector2.Zero)
        {
        }
    }
}
