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
        private int dashReady = 0;

        public Player() : base(0, 0)
        {
            alien = new AnimatedSprite("spritesheet0.png", "spritesheet.xml", "Animations.xml", "Player1");
            alien.Speed = 2;
            Add(alien);
            Add(playerHitbox = new Hitbox(Vector2.Zero, alien.Image.Width, alien.Image.Height));
        }

        public override void Update()
        {
            base.Update();

            Vector2 changePosition = Vector2.Zero;
            changePosition.Y -= velocity;
            velocity += gravity;
            if (dashReady > 0)
                dashReady--;

            changePosition.X += Input.Keyboard.IsDown(Keys.Right) ? 3f : 0;
            changePosition.X += Input.Keyboard.IsDown(Keys.Left) ? -3f : 0;
            //changePosition.Y += Input.Keyboard.IsDown(Keys.Up) ? -1 : 0;
            //changePosition.Y += Input.Keyboard.IsDown(Keys.Down) ? 1 : 0;

            if (Input.Keyboard.IsPressed(Keys.Space) && jumps > 0)
            {
                jumps--;
                velocity = 10;
                changePosition.Y -= 15;
            }

            if (Input.Keyboard.IsPressed(Keys.LeftShift) && dashReady == 0)
            {
                if (changePosition.X != 0)
                {
                    changePosition.X *= 70;
                    dashReady = 180;
                }
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
                        if (h.GlobalRotation == 0)
                        {
                            if (e is Tile)
                            {
                                if (playerHitbox.GlobalBottom > h.GlobalTop)
                                {
                                    Transform.Position.Y = h.GlobalTop - playerHitbox.Height;
                                    jumps = 2;
                                    velocity = 0;
                                }

                                if (playerHitbox.GlobalRight > h.GlobalLeft && playerHitbox.GlobalBottom > h.GlobalTop)
                                {
                                    Transform.Position.X = h.GlobalLeft;
                                }
                                if (playerHitbox.GlobalLeft < h.GlobalRight && playerHitbox.GlobalBottom > h.GlobalTop)
                                {
                                    Transform.Position.X = h.GlobalRight;
                                }
                            }
                        }
                        else
                            changePosition = Vector2.Zero;
                        
                        if (e is Coin)
                        {
                            Coin c = e as Coin;
                            c.HandlePlayerCollision();
                        }
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
            //else if (velocity == 0 && Input.Keyboard.IsDown(Keys.Down))
            //    alien.Play("duck");
        }
    }
}
