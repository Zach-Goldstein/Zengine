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
    public class Planet : Entity
    {
        private float time;
        private float timestep = 0.01f;
        public float speed = 1;
        public Vector2 orbitRange = Vector2.Zero;
        public bool spin;
        private Image baseImage;
        public Vector2 orbitCenter = Vector2.Zero;

        public Planet(string planetName)
        {
            AnimatedSprite planetSprite = new AnimatedSprite(@"spritesheet0.png", @"spritesheet.xml", @"Animations.xml", planetName);
            Add(planetSprite);
            planetSprite.Play("idle");

            baseImage = planetSprite.Image;
            Transform.PivotPoint = new Vector2(baseImage.Width / 2, baseImage.Height);

            spin = true;
        }

        public override void Update()
        {
            base.Update();

            if (!(Transform.Parent is null))
                orbitCenter = Vector2.Zero;

            time += timestep * speed;
            Vector2 offset = new Vector2((float)Math.Sin(time) * orbitRange.X, (float)Math.Cos(time) * orbitRange.Y);
            Transform.Position = orbitCenter + offset;
            Transform.PivotPoint = new Vector2(baseImage.Width / 2, baseImage.Height / 2);

            if (spin)
                Transform.Rotation += 0.01f;
        }
    }
}
