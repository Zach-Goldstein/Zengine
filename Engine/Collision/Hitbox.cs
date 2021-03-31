using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Hitbox : Component
    {
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
            get => Entity.Transform.Rotation;
        }

        public Vector2[] Points
        {
            get
            {
                Matrix transform = Matrix.Identity 
                    * MatrixTransforms.TransformMatrix(-Entity.Transform.GlobalPivotPoint.X, -Entity.Transform.GlobalPivotPoint.Y, 0)
                    * MatrixTransforms.RotationMatrix(-GlobalRotation)
                    * MatrixTransforms.TransformMatrix(Entity.Transform.GlobalPosition.X, Entity.Transform.GlobalPosition.Y, 0);

                Vector2 topLeft = new Vector2(0, 0);
                Vector2 topRight = new Vector2(Width, 0);
                Vector2 botLeft = new Vector2(0, Height);
                Vector2 botRight = new Vector2(Width, Height);

                Vector2 topLeftTransform = Vector2.Transform(topLeft, transform);
                Vector2 topRightTransform = Vector2.Transform(topRight, transform);
                Vector2 botLeftTransform = Vector2.Transform(botLeft, transform);
                Vector2 botRightTransform = Vector2.Transform(botRight, transform);

                return new Vector2[] { topLeftTransform, topRightTransform, botRightTransform, botLeftTransform };
            }
        }

        public Hitbox(Vector2 offset, float width, float height, float rotation = 0)
        {
            Offset = offset;
            Width = width;
            Height = height;
        }

        public bool CollidesWith(Hitbox h)
        {
            if (this.GlobalRotation == 0 && h.GlobalRotation == 0)
            {
                return
                    GlobalLeft <= h.GlobalRight &&
                    GlobalRight >= h.GlobalLeft &&
                    GlobalBottom >= h.GlobalTop &&
                    GlobalTop <= h.GlobalBottom;
            }

            foreach (Hitbox hb in new[] { this, h})
            {
                for (int i = 0; i < 2; i++)
                {
                    int j = (i + 1) % 4;
                    // Create a separating axis with two consecutive points
                    Vector2 p1 = hb.Points[i];
                    Vector2 p2 = hb.Points[j];

                    // Get the perpendicular axis, since the other hitbox will cross the separating axis
                    Vector2 axis = p1 - p2;
                    Vector2 axisPerp = new Vector2(axis.Y, axis.X);

                    // Get the limits along the perpendicular axis for this hitbox
                    double minA = double.MaxValue, maxA = double.MinValue;
                    foreach (Vector2 p in this.Points)
                    {
                        double projection = axisPerp.X * p.X + axisPerp.Y * p.Y;
                        if (projection < minA)
                            minA = projection;
                        if (projection > maxA)
                            maxA = projection;
                    }

                    // Get the limits along the perpendicular axis for the other hitbox
                    double minB = double.MaxValue, maxB = double.MinValue;
                    foreach (Vector2 p in h.Points)
                    {
                        var projection = axisPerp.X * p.X + axisPerp.Y * p.Y;
                        if (projection < minB)
                            minB = projection;
                        if (projection > maxB)
                            maxB = projection;
                    }
                    
                    // If the objects don't overlap along the projection, they must be separate
                    if (maxA < minB || maxB < minA)
                        return false;
                }
            }  

            return true;
        }

        public override void Draw()
        {
            
#if DEBUG
            Texture2D square = new Texture2D(Core.Graphics.GraphicsDevice, 1, 1);
            square.SetData(new Color[] { Color.Red });
#pragma warning disable CS0618 // Type or member is obsolete
            foreach (Vector2 point in Points)
            {
                Core.SpriteBatch.Draw(
                square,
                point,
                null,
                new Rectangle(0, 0, 3, 3),
                //Entity.ParentTransform is null ? Entity.Transform.PivotPoint : Entity.ParentTransform.PivotPoint/* - Image.Offset */, 
                //Entity.Transform.PivotPoint,
                Vector2.Zero,
                0,
                Vector2.One,
                Color.White
                );
            }
#pragma warning restore CS0618 // Type or member is obsolete
#endif

            
        }
    }
}
