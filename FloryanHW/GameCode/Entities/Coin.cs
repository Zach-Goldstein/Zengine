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
            Transform.Scale = 8;
            Add(new Hitbox(Vector2.Zero, coinSprite.Image.Width * Transform.Scale, coinSprite.Image.Height * Transform.Scale));
        }

        public Coin()
            : this(Vector2.Zero)
        {
        }

        public override void Update()
        {
            base.Update();

            Transform.Rotation += 0.01f;
        }

        public void HandlePlayerCollision()
        {
            Visible = false;
            Scene.Remove(this);
            if (Scene is CollisionScene)
            {
                CollisionScene cs = Scene as CollisionScene;
                cs.EventDispatcher.DispatchEvent(new Event("COIN_PICKED_UP", this));
            }
        }
    }
}
