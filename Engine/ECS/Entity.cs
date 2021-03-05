using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Entity
    {
        public Scene Scene { get; private set; }

        private List<Component> components;
        private HashSet<Component> componentsToRemove;
        private HashSet<Component> componentsToAdd;

        public Vector2 Position
        {
            get => Transform.Position;
            set => Transform.Position = value;
        }

        public Vector2 GlobalPosition
        {
            get => Transform.GlobalPosition;
        }

        public Vector2 GlobalPivotPoint
        {
            get => Transform.GlobalPivotPoint;
        }

        public float GlobalRotation
        {
            get => Transform.GlobalRotation;
        }

        public Transform Transform;


        public bool Visible;
        public bool Active;

        public Entity(float x, float y)
        {
            Transform = new Transform();
            Transform.Position = new Vector2(x, y);

            components = new List<Component>();
            componentsToRemove = new HashSet<Component>();
            componentsToAdd = new HashSet<Component>();

            Visible = true;
            Active = true;
        }

        public Entity() : this(0, 0)
        {

        }

        public virtual void Update()
        {
            if (!Active)
                return;

            foreach (Component c in components)
                c.Update();
        }

        public virtual void Draw()
        {
            if (!Visible)
                return;

            foreach (Component c in components)
                c.Draw();
        }

        public void Add(Component c)
        {
            if (c.Entity == null)
                componentsToAdd.Add(c);
        }

        public void Added(Scene s)
        {
            Scene = s;
        }

        public void Remove(Component c)
        {
            if (c.Entity == this)
                componentsToRemove.Add(c);
        }

        public void Removed()
        {
            foreach (Component c in components)
                c.Removed();

            Scene = null;
        }

        public void UpdateComponents()
        {
            if (componentsToRemove.Count > 0)
            {
                foreach (Component c in componentsToRemove)
                {
                    components.Remove(c);
                    c.Removed();
                }

                componentsToRemove.Clear();
            }

            if (componentsToAdd.Count > 0)
            {
                foreach (Component c in componentsToAdd)
                {
                    components.Add(c);
                    c.Added(this);
                }

                componentsToAdd.Clear();
            }
        }

        public T Get<T>() where T : Component
        {
            foreach (Component c in components)
                if (c is T)
                    return c as T;

            return null;
        }

        public void AddChild(Entity e)
        {
            e.Transform.Parent = Transform;
            Transform.AddedChild(e);
        }

        public void RemoveChild(Entity e)
        {
            e.Transform.Parent = null;
            Transform.RemovedChild(e);
        }

        public void RemoveAllChild()
        {
            Transform.RemovedAllChild();
        }
    }
}
