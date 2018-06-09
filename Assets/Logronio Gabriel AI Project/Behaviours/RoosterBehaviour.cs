using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoosterBehaviour : MonoBehaviour {

    public enum RoosterStates { ROAMING, ATTACKING}
    public RoosterStates CurrentState;

    private NavMeshAgent agent;
    private Animator anim;

    public LayerMask PlayersLayer;
    private Collider[] NearbyPlayers = new Collider[4];

    private GameObject CurrentTarget;
    private Vector3 CurrentDestination;

    private float RoamingTimer;
    public float WalkingTime = 7;
    public float WaitingTime = 6f;

    // Use this for initialization
    void Start () {

        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        CurrentDestination = transform.position;
        CurrentTarget = null;
        CurrentState = RoosterStates.ROAMING;

    }

    // Update is called once per frame
    void Update () {

        if (Physics.OverlapSphereNonAlloc(transform.position, 4f, NearbyPlayers, PlayersLayer) == 0)
        {
            RoamingState();
        }

    }

    public void Alarm(GameObject target)
    {
        CurrentTarget = target;
        CurrentDestination = target.transform.position;
        RoamingTimer = WalkingTime + WaitingTime;
    }

    public void RoamingState()
    {
        CurrentState = RoosterStates.ROAMING;

        if (RoamingTimer < WalkingTime + WaitingTime)
        {
            RoamingTimer += Time.deltaTime;
        }

        if (RoamingTimer >= WalkingTime + WaitingTime)
        {
            anim.SetBool("Walking", true);
            float RandomX = Random.Range(-1f, 1f);
            float RandomZ = Random.Range(-1f, 1f);
            Vector3 CurrentDirection = new Vector3(RandomX, transform.position.y, RandomZ).normalized;
            CurrentDestination = transform.position + CurrentDirection * 30;
            RoamingTimer = 0;
        }
        else if (RoamingTimer > WalkingTime && RoamingTimer < (WalkingTime + WaitingTime))
        {
            anim.SetBool("Walking", false);
            CurrentDestination = transform.position;
        }

        agent.speed = 1.5f;

        Debug.DrawLine(transform.position, CurrentDestination, Color.green);
        agent.SetDestination(CurrentDestination);

    }

    public void AttackingState()
    {
        CurrentState = RoosterStates.ATTACKING;

    }

    private GameObject FindNearest(Collider[] Neighborgs)
    {
        float distance = 0f;
        float nearestDistance = float.MaxValue;
        GameObject NearestElement = null;

        foreach (Collider NearbyElement in Neighborgs)
        {
            if (NearbyElement != null)
            {
                distance = Vector3.Distance(NearbyElement.transform.position, transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    NearestElement = NearbyElement.gameObject;
                }
            }
        }
        return NearestElement;
    }
}
