using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    /// <summary>
    /// Separate class for position/rotation/scale/pivot so it doesn't clutter the Entity
    /// </summary>
    public class Transform
    {
        public List<Entity> ChildrenList { get; private set; }

        public Transform Parent;

        public Vector2 Position;
        public float Scale;
        public Vector2 PivotPoint;
        public float Rotation;

        public Vector2 GlobalPosition
        {
            get
            {
                if (Parent is null)
                    return Position;
                return Position + Parent.GlobalPosition;
            }
        }

        public Vector2 GlobalPivotPoint
        {
            get
            {
                if (Parent is null)
                    return PivotPoint;
                return PivotPoint + Parent.GlobalPivotPoint;
            }
        }

        public float GlobalRotation
        {
            get
            {
                if (Parent is null)
                    return Rotation;
                return Rotation + Parent.GlobalRotation;
            }
        }

        public Transform()
        {
            Position = Vector2.Zero;
            Scale = 1;
            PivotPoint = Vector2.Zero;
            Rotation = 0;

            ChildrenList = new List<Entity>();
        }

        public void AddedChild(Entity e)
        {
            ChildrenList.Add(e);
        }

        public void RemovedChild(Entity e)
        {
            ChildrenList.Remove(e);
        }

        public void RemovedAllChild()
        {
            ChildrenList.Clear();
        }
    }
}
