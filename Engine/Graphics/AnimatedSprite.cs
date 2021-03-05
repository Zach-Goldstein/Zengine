using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Engine
{
    public class AnimatedSprite : Sprite
    {
        public int Speed;
        private int remainder;
        private Dictionary<string, List<Image>> animations;
        public List<Image> currentAnimation { get; private set; }
        private Dictionary<string, string> animationSwitch;
        private string currentAnimationName;
        private int frameIndex;
        private bool isPlaying;

        // Ill be back
        public AnimatedSprite(string spriteSheet, string spriteSheetData, string animationData, string spriteName) : base(spriteSheet)
        {
            animations = new Dictionary<string, List<Image>>();
            animationSwitch = new Dictionary<string, string>();
            Dictionary<string, Image> sprites = Image.LoadSpriteSheet(spriteSheetData);
            string defaultAnim = LoadAnimations(animationData, sprites, spriteName);
            isPlaying = false;
            currentAnimation = animations[defaultAnim];
            currentAnimationName = defaultAnim;
            Image = currentAnimation[0];
            remainder = 0;
            Speed = 5;
        }

        public override void Update()
        {
            base.Update();
            if (!isPlaying)
                return;

            Image = currentAnimation[frameIndex];
            remainder += 1;
            if (remainder >= Speed)
            {
                remainder = 0;
                frameIndex += 1;
            }

            if (frameIndex >= currentAnimation.Count)
            {
                string next;
                animationSwitch.TryGetValue(currentAnimationName, out next);
                if (next == null)
                    frameIndex = 0;
                else
                {
                    currentAnimation = animations[next];
                    currentAnimationName = next;
                    frameIndex = 0;
                }    
            }
        }

        public void Play(string animationName, bool restart = false)
        {
            isPlaying = true;
            if (animationName == currentAnimationName && !restart)
                return;
            else if (animationName == currentAnimationName && restart)
            {
                frameIndex = 0;
                return;
            }

            currentAnimationName = animationName;
            currentAnimation = animations[animationName];
            frameIndex = 0;
        }

        public void Stop()
        {
            isPlaying = false;
        }

        public void Resume()
        {
            isPlaying = true;
        }

        private string LoadAnimations(string spriteAnimationData, Dictionary<string, Image> sprites, string spriteName)
        {
            XmlDocument xml = new XmlDocument();
            xml.Load(TitleContainer.OpenStream(Core.Instance.Content.RootDirectory + "\\" + spriteAnimationData));
            XmlElement spriteAnimationSet = xml["Sprites"];

            string defaultAnim = "";

            foreach (XmlElement spriteAnimation in spriteAnimationSet)
            {
                if (spriteName != spriteAnimation.Name)
                    continue;

                defaultAnim = spriteAnimation.Attributes["default"].InnerText;

                foreach (XmlElement animation in spriteAnimation)
                {
                    string animationName = animation.Attributes["name"].InnerText;
                    string set = animation.Attributes["set"].InnerText;

                    int start = Convert.ToInt32(animation.Attr("start", "-1"));
                    int end = Convert.ToInt32(animation.Attr("end", "-1"));

                    string padding = animation.Attr("format", "0");
                    string splitter = animation.Attr("splitter", "-");

                    string nextAnim = animation.Attributes["next"]?.InnerText;

                    List<Image> animationImages = new List<Image>();
                    if (start >= 0)
                        for (int i = start; i <= end; i += 1)
                        {
                            string withPadding = i.ToString(padding);
                            animationImages.Add(sprites[set + splitter + withPadding]);
                        }
                    else
                        animationImages.Add(sprites[set]);

                    animations[animationName] = animationImages;
                    if (nextAnim != null)
                        animationSwitch[animationName] = nextAnim;
                }

                break;
            }

            return defaultAnim;
        }
    }
}
