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
        public EventDispatcher EventDispatcher;

        public CollisionScene()
        {
            EventDispatcher = new EventDispatcher();
            QuestManager questManager = new QuestManager();

            Entity background = new Entity();
            background.Add(new Sprite("spritesheet.xml", @"Graphics/bg"));
            background.Transform.Scale = 8;
            Add(background);

            int tileScale = 70;

            Add(new Tile(@"Graphics/Tiles/grassLeft", new Vector2(0, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassMid", new Vector2(1, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassMid", new Vector2(2, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassMid", new Vector2(3, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassMid", new Vector2(4, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassRight", new Vector2(5, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(7, 8) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(8, 7) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(9, 6) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grass", new Vector2(11, 6) * tileScale));

            Add(new Tile(@"Graphics/Tiles/grassLeft", new Vector2(15, 6) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassMid", new Vector2(16, 6) * tileScale));
            Add(new Tile(@"Graphics/Tiles/grassRight", new Vector2(17, 6) * tileScale));

            Add(new Tile(@"Graphics/Tiles/box", new Vector2(2, 7) * tileScale, false));
            Add(new Tile(@"Graphics/Tiles/boxAlt", new Vector2(3, 7) * tileScale, false));

            Player p = new Player();
            Coin c = new Coin(new Vector2(360, 360));

            EventDispatcher = new EventDispatcher();
            EventDispatcher.AddEventListener(questManager, "COIN_PICKED_UP");

            Add(p);
            Add(c);
            Add(questManager);
        }
    }
}
