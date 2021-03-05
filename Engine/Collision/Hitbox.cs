using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Hitbox : Component
    {
        public float Rotation;
        public Vector2 Offset;
        public float Width;
        public float Height;

        public float GlobalLeft
        {
            get => Entity.GlobalPosition.X + Offset.X;
        }

        public float GlobalRight
        {
            get => Entity.GlobalPosition.X + Offset.X + Width;
        }

        public float GlobalTop
        {
            get => Entity.GlobalPosition.Y + Offset.Y;
        }

        public float GlobalBottom
        {
            get => Entity.GlobalPosition.Y + Offset.Y + Height;
        }

        public float GlobalRotation
        {
            get => Rotation + Entity.Transform.Rotation;
        }

        public Hitbox(Vector2 offset, float width, float height, float rotation = 0)
        {
            Offset = offset;
            Width = width;
            Height = height;
            Rotation = rotation;
        }

        public bool CollidesWith(Hitbox h)
        {
            if (this.Rotation == 0 && h.Rotation == 0)
            {
                return
                    GlobalLeft <= h.GlobalRight &&
                    GlobalRight >= h.GlobalLeft &&
                    GlobalBottom >= h.GlobalTop &&
                    GlobalTop <= h.GlobalBottom;
            }

            return false;
        }
    }
}
