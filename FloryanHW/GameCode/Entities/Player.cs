using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace FloryanHW
{
    public class Player : Entity
    {
        private AnimatedSprite alien;

        public Player() : base(0, 0)
        {
            alien = new AnimatedSprite("spritesheet0.png", "spritesheet.xml", "Animations.xml", "Player1");
            Add(alien);
        }

        public override void Update()
        {
            base.Update();

            Vector2 changePosition = Vector2.Zero;

            changePosition.X += Input.Keyboard.IsDown(Keys.Right) ? 1 : 0;
            changePosition.X += Input.Keyboard.IsDown(Keys.Left) ? -1 : 0;
            changePosition.Y += Input.Keyboard.IsDown(Keys.Up) ? -1 : 0;
            changePosition.Y += Input.Keyboard.IsDown(Keys.Down) ? 1 : 0;

            Transform.Position += changePosition;

            if (changePosition.X < 0)
                alien.Flipped = true;
            else if (changePosition.X > 0)
                alien.Flipped = false;

            if (changePosition.X != 0)
                alien.Play("walk");
        }
    }
}
