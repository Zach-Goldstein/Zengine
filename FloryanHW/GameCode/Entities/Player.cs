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
        private Hitbox playerHitbox;

        private float gravity = -0.5f;
        private float velocity = 0;
        private int jumps = 0;

        public Player() : base(0, 0)
        {
            alien = new AnimatedSprite("spritesheet0.png", "spritesheet.xml", "Animations.xml", "Player1");
            Add(alien);
            Add(playerHitbox = new Hitbox(Vector2.Zero, alien.Image.Width, alien.Image.Height));
        }

        public override void Update()
        {
            base.Update();

            Vector2 changePosition = Vector2.Zero;
            changePosition.Y -= velocity;
            velocity += gravity;

            changePosition.X += Input.Keyboard.IsDown(Keys.Right) ? 1.5f : 0;
            changePosition.X += Input.Keyboard.IsDown(Keys.Left) ? -1.5f : 0;
            //changePosition.Y += Input.Keyboard.IsDown(Keys.Up) ? -1 : 0;
            //changePosition.Y += Input.Keyboard.IsDown(Keys.Down) ? 1 : 0;

            if (Input.Keyboard.IsPressed(Keys.Space) && jumps > 0)
            {
                jumps--;
                velocity = 10;
                changePosition.Y -= 15;
            }

            bool collided = false;
            Transform.Position += changePosition;

            foreach (Entity e in this.Scene)
            {
                if (e == this)
                    continue;
                Hitbox h;
                if ((h = e.Get<Hitbox>()) != null)
                {
                    if (playerHitbox.CollidesWith(h))
                    {
                        if (h.Rotation == 0)
                        {
                            if (playerHitbox.GlobalBottom > h.GlobalTop)
                            {
                                Transform.Position.Y = h.GlobalTop - playerHitbox.Height;
                                jumps = 2;
                                velocity = 0;
                            }
                        }
                        else
                            changePosition = Vector2.Zero;
                        break;
                    }
                }
            }

            if (!collided)
                

            if (changePosition.X < 0)
                alien.Flipped = true;
            else if (changePosition.X > 0)
                alien.Flipped = false;

            if (changePosition.X != 0)
                alien.Play("walk");
        }
    }
}
