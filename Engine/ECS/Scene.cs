using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public class Scene : IEnumerable<Entity>
    {
        private List<Entity> entities;
        private HashSet<Entity> entitiesToRemove;
        private HashSet<Entity> entitiesToAdd;

        public Scene()
        {
            entities = new List<Entity>();
            entitiesToRemove = new HashSet<Entity>();
            entitiesToAdd = new HashSet<Entity>();
        }

        public virtual void Update()
        {
            foreach (Entity e in entities)
                e.Update();

            UpdateEntities();
        }

        public virtual void Draw()
        {
            foreach (Entity e in entities)
                e.Draw();
        }

        public void Add(Entity e)
        {
            if (e.Scene == null)
                entitiesToAdd.Add(e);
        }

        public void Remove(Entity e)
        {
            if (e.Scene == this)
                entitiesToRemove.Add(e);
        }

        public void UpdateEntities()
        {
            if (entitiesToRemove.Count > 0)
            {
                foreach (Entity e in entitiesToRemove)
                {
                    entities.Remove(e);
                    e.Removed();
                }

                entitiesToRemove.Clear();
            }

            if (entitiesToAdd.Count > 0)
            {
                foreach (Entity e in entitiesToAdd)
                {
                    entities.Add(e);
                    e.Added(this);
                }

                entitiesToAdd.Clear();
            }

            if (entities.Count > 0)
                foreach (Entity e in entities)
                    e.UpdateComponents();
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entities.GetEnumerator();
        }
    }
}
