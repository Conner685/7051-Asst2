using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLocomotion : MonoBehaviour
{
    [Header("Input Asset")]
    [SerializeField] InputActionAsset inputActions;

    [Header("Locomotion Settings")]
    [SerializeField] float inputDampTime = 0.05f;

    Animator animator;
    InputAction moveAction;

    Vector2 input;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Cache the actions from the "Player" map
        var playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Movement");
    }

    void OnEnable()
    {
        // Enable the actions when the GameObject is active
        moveAction?.Enable();
    }

    void OnDisable()
    {
        // Disable the actions when the GameObject is inactive
        moveAction?.Disable();
    }

    void Update()
    {
        // Read 2D Vector directly from WASD binding
        input = moveAction.ReadValue<Vector2>();

        // Pass directional inputs to Animator with damping
        animator.SetFloat("InputX", input.x, inputDampTime, Time.deltaTime);
        animator.SetFloat("InputY", input.y, inputDampTime, Time.deltaTime);
    }
}