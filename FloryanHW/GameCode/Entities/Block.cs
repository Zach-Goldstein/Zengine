using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;

namespace FloryanHW
{
    public class Block : Entity
    {
        private float time;
        private float timestep = 0.1f;
        public bool spin;

        public Block()
        {
            AnimatedSprite s = new AnimatedSprite(@"spritesheet0.png", @"spritesheet.xml", @"Animations.xml", @"Block");
            Add(s);
            time = 0;
            Transform.Scale = 5;
            spin = false;
        }

        public override void Update()
        {
            base.Update();
            time += timestep;
            Position = (new Vector2((float)Math.Sin(time), (float)Math.Cos(time))) * 5 + GlobalPivotPoint;

            if (spin)
                Transform.Rotation += 0.1f;
        }
    }
}
