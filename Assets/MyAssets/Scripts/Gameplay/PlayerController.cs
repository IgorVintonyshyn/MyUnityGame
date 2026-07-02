using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Animator animator;
    public Camera mainCamera;

    [Header("Attack")]
    [SerializeField] private PlayerAttack weaponHitbox;
    [SerializeField] private float attackCooldown = 0.4f;
    [SerializeField] private float attackDistance = 3f;

    private bool attackHeld;
    private float attackTimer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 15f;

    private Vector2 moveInput;
    private Vector2 lookInput;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private bool isMobile;

    private PlayerInput input;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        input = GetComponent<PlayerInput>();

        isMobile = Application.isMobilePlatform;
    }

    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.up, transform.forward * attackDistance, Color.red);
        HandleMovement();
        HandleRotation();
        HandleAttack();
    }

    // ---------------- INPUT ----------------

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        attackHeld = context.ReadValueAsButton();
    }

    // ---------------- MOVEMENT ----------------

    private void HandleMovement()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        animator.SetBool("InMove", move.sqrMagnitude > 0.01f);
    }

    // ---------------- ROTATION ----------------

    private void HandleRotation()
    {
        Vector3 direction;

        if (isMobile)
        {
            direction = new Vector3(lookInput.x, 0, lookInput.y);
        }
        else
        {
            if (Mouse.current == null) return;

            Vector2 mousePos = Mouse.current.position.ReadValue();

            Ray ray = mainCamera.ScreenPointToRay(mousePos);
            Plane ground = new Plane(Vector3.up, Vector3.zero);

            if (!ground.Raycast(ray, out float distance))
                return;

            Vector3 point = ray.GetPoint(distance);
            direction = point - transform.position;
        }

        direction.y = 0;

        if (direction.sqrMagnitude < 0.001f)
            return;

        Quaternion rot = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            rot,
            Time.deltaTime * rotationSpeed
        );
    }

    // ---------------- ATTACK ----------------

    private void HandleAttack()
    {
        if (isMobile)
        {
            AutoAttack();
        }
        else
        {
            ManualAttack();
        }
    }

    private void ManualAttack()
    {
        if (!attackHeld) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
            
        }
    }

    private void AutoAttack()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer > 0f)
            return;
        
        Ray ray = new Ray(transform.position + Vector3.up, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, attackDistance, enemyLayer))
        {
            Attack();
            attackTimer = attackCooldown;
            
        }
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        
    }
}