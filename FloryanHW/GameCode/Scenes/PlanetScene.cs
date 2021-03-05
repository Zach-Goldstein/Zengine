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
    class PlanetScene : Scene
    {
        private float scale;
        private float rotation;
        private Vector2 translate;

        private Planet sun;

        public PlanetScene()
        {
            scale = 1;
            rotation = 0;
            translate = Vector2.Zero;

            Planet p1 = new Planet(@"Lava");
            p1.orbitCenter = new Vector2(Core.Width / 2, Core.Height / 2);
            p1.speed = 0.001f;
            p1.orbitRange = new Vector2(0, 0);
            p1.Transform.Scale = 3f;
            Add(p1);
            sun = p1;
            
            Planet p2 = new Planet(@"Terran");
            p2.speed = 0.3f;
            p2.Transform.Parent = p1.Transform;
            p2.orbitRange = new Vector2(400, 125);
            p2.Transform.Scale = 1f;
            Add(p2);

            Planet p2_Moon = new Planet(@"Moon");
            p2_Moon.speed = 0.7f;
            p2_Moon.Transform.Parent = p2.Transform;
            p2_Moon.orbitRange = new Vector2(100, 100);
            p2_Moon.Transform.Scale = 0.4f;
            Add(p2_Moon);

            Planet p3 = new Planet(@"Lava");
            p3.speed = 0.5f;
            p3.Transform.Parent = p1.Transform;
            p3.orbitRange = new Vector2(200, 500);
            p3.Transform.Scale = 0.9f;
            Add(p3);

            Planet p3_Moon = new Planet(@"Moon");
            p3_Moon.speed = 1.5f;
            p3_Moon.Transform.Parent = p3.Transform;
            p3_Moon.orbitRange = new Vector2(35, 50);
            p3_Moon.Transform.Scale = 0.2f;
            Add(p3_Moon);

            Planet p3_Moon2 = new Planet(@"Moon");
            p3_Moon2.speed = 0.5f;
            p3_Moon2.Transform.Parent = p3.Transform;
            p3_Moon2.orbitRange = new Vector2(50, 50);
            p3_Moon2.Transform.Scale = 0.25f;
            Add(p3_Moon2);

            Planet p5 = new Planet(@"Ice");
            p5.speed = 0.1f;
            p5.Transform.Parent = p1.Transform;
            p5.orbitRange = new Vector2(600, 400);
            p5.Transform.Scale = 0.3f;
            Add(p5);
        }

        public override void Update()
        {
            base.Update();

            sun.orbitCenter += new Vector2(1, 0);

            float scaleChange = 0.01f *
                ((Input.Keyboard.IsDown(Keys.A) ? 1.0f : 0.0f) + (Input.Keyboard.IsDown(Keys.S) ? -1.0f : 0.0f));

            float rotationChange = 0.01f *
                ((Input.Keyboard.IsDown(Keys.Q) ? 1.0f : 0.0f) + (Input.Keyboard.IsDown(Keys.W) ? -1.0f : 0.0f));

            Vector2 changePosition = Vector2.Zero;

            changePosition.X += Input.Keyboard.IsDown(Keys.Right) ? 1 : 0;
            changePosition.X += Input.Keyboard.IsDown(Keys.Left) ? -1 : 0;
            changePosition.Y += Input.Keyboard.IsDown(Keys.Up) ? -1 : 0;
            changePosition.Y += Input.Keyboard.IsDown(Keys.Down) ? 1 : 0;

           changePosition *= 5;

            if (scaleChange != 0 || rotationChange != 0 || changePosition != Vector2.Zero)
            {
                scale += scaleChange;
                rotation += rotationChange;
                translate += changePosition;
                Core.Camera = Matrix.Identity
                    * MatrixTransforms.TransformMatrix(-sun.GlobalPosition.X, -sun.GlobalPosition.Y)
                    * MatrixTransforms.ScaleMatrix(scale)
                    * MatrixTransforms.RotationMatrix(rotation)
                    * MatrixTransforms.TransformMatrix(sun.GlobalPosition.X, sun.GlobalPosition.Y)
                    //* MatrixTransforms.TransformMatrix(-Core.Width / 2, -Core.Height / 2)
                    //* MatrixTransforms.RotationMatrix(rotation)
                    //* MatrixTransforms.TransformMatrix(Core.Width / 2, Core.Height / 2)
                    * MatrixTransforms.TransformMatrix(-translate.X, -translate.Y)
                    ;
            }
        }
    }
}
