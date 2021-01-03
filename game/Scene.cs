using System;
using System.Collections.Generic;
using System.Text;

namespace SMF.game
{
    class Scene
    {
        List<Actor> objects;
        public void Instantiate(Actor obj)
        {
            objects.Add(obj);
        }
        public void Destroy(Actor obj)
        {
            objects.Remove(obj);
        }
        public List<Actor> GetActorsOfClass(Type class_searched)
        {
            List<Actor> list = new List<Actor>();
            foreach (Actor obj in objects)
            {
                if (obj.GetType() == class_searched)
                    list.Add(obj);
            }
            return list;
        }
    }
}
