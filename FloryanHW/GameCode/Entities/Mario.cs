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
    public class Mario : Entity
    {

        public Mario()
        {
            Add(new Sprite(@"spritesheet.xml", @"Mario-1"));
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

            //if (changePosition != Vector2.Zero)
            //    Get<AnimatedSprite>().Play("idle");

            Vector2 changePivot = Vector2.Zero;

            changePivot.X += Input.Keyboard.IsDown(Keys.L) ? 1 : 0;
            changePivot.X += Input.Keyboard.IsDown(Keys.J) ? -1 : 0;
            changePivot.Y += Input.Keyboard.IsDown(Keys.I) ? -1 : 0;
            changePivot.Y += Input.Keyboard.IsDown(Keys.K) ? 1 : 0;

            Transform.PivotPoint += changePivot;

            if (Input.Keyboard.IsPressed(Keys.V))
                Visible = !Visible;

            float alphaAdjust = 0.01f * 
                ((Input.Keyboard.IsDown(Keys.Z) ? 1.0f : 0.0f) + (Input.Keyboard.IsDown(Keys.X) ? -1.0f : 0.0f));

            if (alphaAdjust != 0)
            {
                Sprite s = Get<Sprite>();
                if (s != null)
                    s.Alpha += alphaAdjust;
            }

            Transform.Rotation += 0.01f *
                ((Input.Keyboard.IsDown(Keys.Q) ? 1.0f : 0.0f) + (Input.Keyboard.IsDown(Keys.W) ? -1.0f : 0.0f));

            Transform.Scale += 0.1f *
                ((Input.Keyboard.IsDown(Keys.A) ? 1.0f : 0.0f) + (Input.Keyboard.IsDown(Keys.S) ? -1.0f : 0.0f));
        }
    }
}
