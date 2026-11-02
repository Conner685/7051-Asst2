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

    Animator animator;
    InputAction moveAction;
    private InputAction ghostAction;

    private bool isGhost;

    Vector2 input;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Cache the actions from the "Player" map
        var playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Movement");
        ghostAction = playerMap.FindAction("GhostMode");
    }

    private void Start()
    {
        ghostAction.performed += ToggleGhostMode;
        isGhost = false;
        playerCollider.enabled = true;
    }

    void OnEnable()
    {
        // Enable the actions when the GameObject is active
        moveAction?.Enable();
        ghostAction?.Enable();
    }

    void OnDisable()
    {
        // Disable the actions when the GameObject is inactive
        moveAction?.Disable();
        ghostAction?.Disable();
    }

    void OnDestroy()
    {
        ghostAction.performed -= ToggleGhostMode;
    }

    void Update()
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
}