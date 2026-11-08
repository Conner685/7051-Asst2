using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocomotion : MonoBehaviour
{
    [Header("Input Asset")]
    [SerializeField] InputActionAsset inputActions;

    [Header("Locomotion Settings")]
    [SerializeField] float inputDampTime = 0.05f;
    
    [SerializeField]
    private CapsuleCollider playerCollider;
    
    [SerializeField]
    private Transform startPoint;

    [SerializeField] private EnemyNav enemyNav;

    Animator animator;
    InputAction moveAction;
    private InputAction ghostAction;
    private InputAction resetAction;

    private bool isGhost;

    Vector2 input;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Cache the actions from the "Player" map
        var playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Movement");
        ghostAction = playerMap.FindAction("GhostMode");
        resetAction = playerMap.FindAction("Reset");
    }

    private void Start()
    {
        ghostAction.performed += ToggleGhostMode;
        isGhost = false;
        playerCollider.enabled = true;
        
        resetAction.performed += ResetPosition;
    }

    void OnEnable()
    {
        // Enable the actions when the GameObject is active
        moveAction?.Enable();
        ghostAction?.Enable();
        resetAction?.Enable();
    }

    void OnDisable()
    {
        // Disable the actions when the GameObject is inactive
        moveAction?.Disable();
        ghostAction?.Disable();
        resetAction?.Disable();
    }

    void OnDestroy()
    {
        ghostAction.performed -= ToggleGhostMode;
        resetAction.performed -= ResetPosition;
    }

    void FixedUpdate()
    {
        // Read 2D Vector directly from WASD binding
        input = moveAction.ReadValue<Vector2>();

        // Pass directional inputs to Animator with damping
        animator.SetFloat("InputX", input.x, inputDampTime, Time.deltaTime);
        animator.SetFloat("InputY", input.y, inputDampTime, Time.deltaTime);
    }

    private void ToggleGhostMode(InputAction.CallbackContext context)
    {
        isGhost = !isGhost;
        playerCollider.enabled = !isGhost;
    }
    
    private void ResetPosition(InputAction.CallbackContext context)
    { 
        transform.position = startPoint.position;
    }
}