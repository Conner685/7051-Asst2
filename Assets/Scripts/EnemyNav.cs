using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyNav : MonoBehaviour
{
    [SerializeField] 
    private Transform target;
    [FormerlySerializedAs("_dampTime")] [SerializeField]
    private float dampTime = 0.05f;
    [SerializeField]
    private float successRadius = 1f;
    [SerializeField]
    private NavMeshSurface navMeshSurface;
    
    private NavMeshAgent _agent;
    private Animator _animator;
    
    
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _agent.SetDestination(target.position);
        if (_agent.remainingDistance <= successRadius && !_agent.pathPending)
        {
            _agent.SetDestination(transform.position);
        }
        _animator.SetFloat("InputX", _agent.velocity.normalized.x, dampTime, Time.deltaTime);
        _animator.SetFloat("InputY", _agent.velocity.normalized.z, dampTime, Time.deltaTime);
    }
}
