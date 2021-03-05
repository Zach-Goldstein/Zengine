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
        private string text;
        private Color color;

        public Text(string fontname, string text, Color color)
        {
            font = Core.Instance.Content.Load<SpriteFont>(fontname);
            this.text = text;
            this.color = color;
        }

        public Text(string text) : this("Default", text, Color.Black)
        {

        }

        public override void Draw()
        {
            Core.SpriteBatch.DrawString(font, text, Entity.Position, color);
        }
    }
}
