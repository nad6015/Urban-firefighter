using UnityEngine;
using UnityEngine.AI;

public class Civilian : Interactable
{
    public GameObject indicator;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private GameObject escapePoint;
    private ObjectivesController controller;
    private static LevelManager manager;
    void Start()
    {
        animator = GetComponent<Animator>();
        escapePoint = GameObject.FindGameObjectWithTag("Escape point");
        navMeshAgent = GetComponent<NavMeshAgent>();
        controller = GameObject.FindGameObjectWithTag("UI_Controller").GetComponent<ObjectivesController>();
        if (manager == null)
        {
            manager = FindObjectOfType<LevelManager>();
        }
    }

    public override void Interact(GameObject gameObject)
    {
        if (!animator.GetBool("isRescued"))
        {
            indicator.SetActive(false);
            animator.SetBool("isRescued", true);
            controller.SaveLife();
            manager.DecreaseCivilianCount();
            manager.IsLevelCompleted();
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void Update()
    {
        // Destination reached check code referenced from - https://discussions.unity.com/t/how-can-i-tell-when-a-navmeshagent-has-reached-its-destination/52403
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            animator.SetBool("isMoving", false);
        }
    }

    internal void Escape()
    {
        animator.SetBool("isMoving", true);
        navMeshAgent.SetDestination(escapePoint.transform.position);
    }
}
