using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class GAction : MonoBehaviour
{
    public string actionName = "Action";
    public float cost = 1f;
    public GameObject target;
    public GameObject targetTag;
    public float duration = 0;
    public WorldState[] preConditions;
    public WorldState[] afterEffects;
    public NavMeshAgent agent;

    public Dictionary<string, int> precondition;
    public Dictionary<string, int> effects;

    public WorldStates agentBeliefs;

    public bool running = false;

    public GAction()
    {
        precondition = new Dictionary<string, int>();
        effects = new Dictionary<string, int>();
    }
    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if(precondition != null)
        {
            foreach (WorldState w in preConditions)
            {
                precondition.Add(w.key, w.value);
            }
        }
        if(afterEffects != null)
        {
            foreach(WorldState w in afterEffects)
            {
                effects.Add(w.key, w.value);
            }
        }

    }

    public bool IsAchievable()
    {
        return true;
    }

    public bool IsAchievableGiven(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in precondition)
        {
            if (!conditions.ContainsKey(p.Key)) { return false; }
        }
        return true;
    }

    public abstract bool PrePerform();
    public abstract bool PostPerform();


}
