using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Engine;

namespace FloryanHW
{
    public class Mario : Entity
    {
        public Mario()
        {
            Sprite s = new Sprite("Mario.png");
            Add(s);
        }
    }
}
