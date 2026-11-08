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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 targetPos = new Vector3();
        
        float targetPosX = target.position.x;
        float targetPosZ = target.position.z;
        
        targetPos.x = (targetPosX > transform.position.x) ? targetPosX - successRadius : targetPosX + successRadius;
        targetPos.z = (targetPosZ > transform.position.z) ? targetPosZ - successRadius : targetPosZ + successRadius;
        targetPos.y = 0f;
        
        _agent.SetDestination(targetPos);
        _animator.SetFloat("InputX", _agent.velocity.normalized.x, dampTime, Time.deltaTime);
        _animator.SetFloat("InputY", _agent.velocity.normalized.z, dampTime, Time.deltaTime);
    }
}
