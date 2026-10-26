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
    InputAction sprintAction;

    Vector2 input;
    bool isRunning = false;

    void Awake()
    {
        animator = GetComponent<Animator>();

        // Cache the actions from the "Player" map
        var playerMap = inputActions.FindActionMap("Player");
        moveAction = playerMap.FindAction("Movement");
        sprintAction = playerMap.FindAction("Sprint");
    }

    void OnEnable()
    {
        // Enable the actions when the GameObject is active
        moveAction?.Enable();
        sprintAction?.Enable();
    }

    void OnDisable()
    {
        // Disable the actions when the GameObject is inactive
        moveAction?.Disable();
        sprintAction?.Disable();
    }

    void Update()
    {
        // Read 2D Vector directly from WASD binding
        input = moveAction.ReadValue<Vector2>();

        // Pass directional inputs to Animator with damping
        animator.SetFloat("InputX", input.x, inputDampTime, Time.deltaTime);
        animator.SetFloat("InputY", input.y, inputDampTime, Time.deltaTime);

        // Toggle running when Sprint (Left Shift) is pressed
        if (sprintAction.WasPressedThisFrame())
        {
            isRunning = !isRunning;

            // Lower aiming animation when running. Aimlayer is on layer 1
            animator.SetLayerWeight(1, isRunning ? 0f : 1f);
            animator.SetBool("IsRunning", isRunning);
        }
    }
}