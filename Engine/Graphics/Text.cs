using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Text : Component
    {
        private SpriteFont font;
        public string TextString;
        private Color color;

        public Text(string fontname, string text, Color color)
        {
            font = Core.Instance.Content.Load<SpriteFont>(fontname);
            this.TextString = text;
            this.color = color;
        }

        public Text(string text) : this("Default", text, Color.Black)
        {

        }

        public override void Draw()
        {
            Core.SpriteBatch.DrawString(font, TextString, Entity.Position, color, 0, Vector2.Zero, Entity.Transform.Scale, SpriteEffects.None, 1);
        }
    }
}
