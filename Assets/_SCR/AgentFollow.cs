using UnityEngine;
using UnityEngine.AI;

public class AgentFollowTarget : MonoBehaviour
{
    public Transform target; // The target object for the agent to follow
    private NavMeshAgent agent; // The NavMeshAgent component

    void Start()
    {
        // Get the NavMeshAgent component attached to this object
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // If a target is assigned, update the agent's destination to the target's position
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
