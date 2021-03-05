using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace FloryanHW
{
    public class CollisionScene : Scene
    {
        public CollisionScene()
        {
            Player p = new Player();
            Coin c = new Coin(new Vector2(360, 360));

            Add(p);
            Add(c);
        }
    }
}
