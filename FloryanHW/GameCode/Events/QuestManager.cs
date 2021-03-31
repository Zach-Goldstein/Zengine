using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;
using Microsoft.Xna.Framework;

namespace FloryanHW
{
    class QuestManager : Entity, IEventListener
    {
        public Text EventText;
        private int framesSinceEvent;

        public QuestManager()
        {
            EventText = new Text("Default", "", Color.Black);
            Transform.Scale = 3;
            Transform.Position = new Vector2(Core.Width / 4, Core.Height / 4);
            Visible = false;
            Active = false;
            Add(EventText);
        }

        public void HandleEvent(Event e)
        {
            if (e.EventType == "COIN_PICKED_UP")
            {
                EventText.TextString = "You picked up a coin!";
                Active = true;
                Visible = true;
            }
        }

        public override void Update()
        {
            base.Update();

            framesSinceEvent++;
            if (framesSinceEvent > 120)
            {
                framesSinceEvent = 0;
                Visible = false;
                Active = false;
            }
        }
    }
}
