using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;

    const string PLAYER_STRING = "Player";


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        
        // Randomize animation start time
        Animator animator = GetComponentInChildren<Animator>();
        animator.Play(0, 0, Random.Range(0f, 1f));
    }

    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    void Update()
    {
        if (!player) return;
        
        agent.SetDestination(player.transform.position);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PLAYER_STRING))
        {
            EnemyHealth enemyHealth = GetComponent<EnemyHealth>();
            enemyHealth.SelfDestruct();
        }
    }
}
