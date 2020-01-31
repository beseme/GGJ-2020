using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ActorManager : MonoBehaviour
{
    public static ActorManager actorManager;
    private HashSet<Actor> actors = new HashSet<Actor>();

    // Start is called before the first frame update
    void Awake()
    {
        actorManager = this;
    }

    // register actor 
    public void register(Actor actor)
    {
        actors.Add(actor);
    }

    // unregister actor
    public void unregister(Actor actor)
    {
        actors.Remove(actor);
    }

    // returns all active actors in the scene
    public IEnumerable<Actor> getAllActors()
    {
        return actors;
    }

    // returns all active actors that are currently rendered
    public IEnumerable<Actor> getAllVisibleActors()
    {
        return actors.Where(a => a.isVisible());
    }
}
