using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;

namespace FloryanHW
{
    public class TestScene : Scene
    {
        public TestScene()
        {
            Mario firstMario = new Mario();
            Add(firstMario);

            Block block = new Block();
            Add(block);
            firstMario.AddChild(block);
            block.Position = new Vector2(0, 0);
            block.Transform.PivotPoint = new Vector2(64, 64);

            Block block2 = new Block();
            Add(block2);
            block.AddChild(block2);
            block.Position = new Vector2(0, 0);
            block2.spin = true;
            block.Transform.PivotPoint = new Vector2(4, 4);
        }
    }
}
