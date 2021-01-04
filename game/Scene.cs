using SFML.Graphics;
using SMF.engine;
using System;
using System.Collections.Generic;

namespace SMF.game
{
    public class Scene
    {
        private List<Actor> actorsToAdd = new List<Actor>();
        private List<Actor> actorsToRemove = new List<Actor>();
        private List<Actor> actors = new List<Actor>();

        public Actor Instantiate(Actor obj)
        {
            actorsToAdd.Add(obj);
            return obj;
        }
        public void Destroy(Actor obj)
        {
            actorsToRemove.Add(obj);
        }
        public List<Actor> GetActorsOfClass(Type class_searched)
        {
            List<Actor> list = new List<Actor>();
            foreach (Actor obj in actors)
            {
                if (obj.GetType() == class_searched)
                    list.Add(obj);
            }
            return list;
        }
        public class SceneController
        {
            public Scene scene = new Scene();
            public void Update(float dt, Input input, AssetManager assetMgr)
            {
                for (int i = 0; i < scene.actorsToAdd.Count; i++)
                {
                    scene.actors.Add(scene.actorsToAdd[i]);
                    scene.actorsToAdd.Clear();
                }
                for (int i = 0; i < scene.actorsToAdd.Count; i++)
                {
                    scene.actors.Remove(scene.actorsToRemove[i]);
                    scene.actorsToRemove.Clear();
                }

                foreach (Actor a in scene.actors)
                    a.Update(dt, input, scene, assetMgr);
            }
            public void Draw(RenderWindow w)
            {
                for (int i = 0; i <= 8; i++)
                {
                    foreach (Actor a in scene.actors)
                        if (a.DrawLayer == i)
                            a.Draw(w);
                }
                
            }
        }
    }
}
