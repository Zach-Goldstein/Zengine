using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Component
    {
        public Entity Entity { get; private set; }

        public virtual void Update()
        {

        }

        public virtual void Draw()
        {

        }

        public void Added(Entity e)
        {
            Entity = e;
        }

        public void Removed()
        {
            Entity = null;
        }
    }
}
