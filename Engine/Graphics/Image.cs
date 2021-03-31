using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Engine
{
    public class Image
    {
        // Prevent textures from being loaded more than once
        public static Dictionary<string, Texture2D> loadedTextures = new Dictionary<string, Texture2D>();
        public static Dictionary<string, Dictionary<string, Image>> loadedSpriteSheet = new Dictionary<string, Dictionary<string, Image>>();

        public Texture2D Texture { get; private set; }

        // Sizing and location
        public Rectangle SrcRect { get; private set; }
        public Vector2 Offset { get; private set; }

        public int Height
        {
            get => SrcRect.Height;
        }

        public int Width
        {
            get => SrcRect.Width;
        }

        public Image(string filename)
        {
            Texture2D texture;
            if (loadedTextures.TryGetValue(filename, out texture))
                Texture = texture;
            else
            {
                FileStream fileStream = new FileStream(Core.ContentDirectory + "\\" + filename, FileMode.Open, FileAccess.Read);
                Texture = Texture2D.FromStream(Core.Instance.GraphicsDevice, fileStream);
                fileStream.Close();
            }
            SrcRect = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Offset = Vector2.Zero;
        }

        public Image(Image srcImage, int x, int y, int width, int height)
        {
            Texture = srcImage.Texture;

            SrcRect = new Rectangle(x, y, width, height);
            Offset = new Vector2(-Math.Min(x - srcImage.Offset.X, 0), -Math.Min(y - srcImage.Offset.Y, 0));
        }

        public static Dictionary<string, Image> LoadSpriteSheet(string spriteSheetData)
        {
            Dictionary<string, Image> spriteList;
            if (!loadedSpriteSheet.TryGetValue(spriteSheetData, out spriteList))
            {
                spriteList = new Dictionary<string, Image>();
                loadedSpriteSheet.Add(spriteSheetData, spriteList);
            }
            else
                return spriteList;

            XmlDocument xml = new XmlDocument();
            xml.Load(TitleContainer.OpenStream(Core.Instance.Content.RootDirectory + "\\" + spriteSheetData));
            XmlElement spriteSheetSet = xml["atlas"];

            foreach (XmlElement spriteSheet in spriteSheetSet)
            {
                string path = spriteSheet.Attributes["n"].InnerText;
                path = (path[0] == '\\' ? path.Substring(1) : path) + ".png";
                Image srcImage = new Image(path);

                foreach (XmlElement sprite in spriteSheet)
                {
                    string spriteName = sprite.Attributes["n"].InnerText;
                    spriteList[spriteName] = new Image(srcImage,
                        Convert.ToInt32(sprite.Attributes["x"].InnerText),
                        Convert.ToInt32(sprite.Attributes["y"].InnerText),
                        Convert.ToInt32(sprite.Attributes["w"].InnerText),
                        Convert.ToInt32(sprite.Attributes["h"].InnerText)
                        );
                }
            }

            return spriteList;
        }
    }
}
