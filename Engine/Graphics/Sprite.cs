using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Sprite : Component
    {
        public Image Image;
        private float alpha;
        public Vector2 Offset;

        public float Alpha
        {
            get => alpha;
            set
            {
                alpha = value;
                if (alpha > 1)
                    alpha = 1;
                if (alpha < 0)
                    alpha = 0;
            }
        }

        public bool Flipped;

        public Sprite(string filename, Vector2 offset)
        {
            Image = new Image(filename);
            Alpha = 1;
            Flipped = false;

            Offset = offset;
        }

        public Sprite(string filename)
            : this(filename, Vector2.Zero)
        {

        }

        public Sprite(string spriteSheetData, string spriteName, Vector2 offset)
        {
            Image = Image.LoadSpriteSheet(spriteSheetData)[spriteName];
            Alpha = 1;
            Flipped = false;

            Offset = offset;
        }

        public Sprite(string spriteSheetData, string spriteName)
            : this(spriteSheetData, spriteName, Vector2.Zero)
        {

        }

        public override void Update()
        {
            base.Update();
        }

        public override void Draw()
        {
#pragma warning disable CS0618 // Type or member is obsolete
            Core.SpriteBatch.Draw(
                Image.Texture,
                Entity.GlobalPosition + Offset/* + (Entity.Transform.PivotPoint)/* * Entity.Transform.Scale*/,
                null,
                Image.SrcRect,
                //Entity.ParentTransform is null ? Entity.Transform.PivotPoint : Entity.ParentTransform.PivotPoint/* - Image.Offset */, 
                //Entity.Transform.PivotPoint,
                Entity.Transform.PivotPoint /*+ (Entity.Transform.Parent is null ? Vector2.Zero : Entity.Transform.Parent.GlobalPosition) */,
                Entity.Transform.Rotation,
                new Vector2(Entity.Transform.Scale, Entity.Transform.Scale),
                Color.White * Alpha,
                Flipped ? Microsoft.Xna.Framework.Graphics.SpriteEffects.FlipHorizontally : Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
                0
                );
#pragma warning restore CS0618 // Type or member is obsolete
            /*
            Core.SpriteBatch.Draw(
                Image.Texture,
                Entity.Position + Entity.Transform.PivotPoint,
                Image.SrcRect,
                Color.White * Alpha,
                Entity.Transform.Rotation,
                Entity.Transform.PivotPoint,
                new Vector2(Entity.Transform.Scale, Entity.Transform.Scale),
                Microsoft.Xna.Framework.Graphics.SpriteEffects.None,
                0);
            */
        }
    }
}
