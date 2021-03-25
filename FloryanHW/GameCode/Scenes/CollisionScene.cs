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

            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(0, 500)));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(70, 500)));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(140, 500)));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(210, 500)));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(280, 500)));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(350, 500)));
        }
    }
}
