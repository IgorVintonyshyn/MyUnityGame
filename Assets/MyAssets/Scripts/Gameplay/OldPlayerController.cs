using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewPlayerController : MonoBehaviour
{
    public CharacterController player;
    public Animator playerAnimator;
    public Camera mainCamera;

    [SerializeField] private PlayerAttack weaponHitbox;

    [Range(1, 10)]
    [SerializeField] private float moveSpeed = 5f;

    [SerializeField] private float attackCooldown = 0.4f;

    private float baseMoveSpeed;
    private float rotationSpeed = 15f;

    private Vector2 moveInput;

    private bool isAttackingHeld;
    private float attackTimer;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimator = GetComponent<Animator>();

        if (mainCamera == null)
            mainCamera = Camera.main;

        baseMoveSpeed = moveSpeed;
    }

    void Update()
    {
        Move();
        LookAtMouse();
        HandleAttack();
    }

    // ---------------- MOVEMENT ----------------

    private void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        player.Move(move * moveSpeed * Time.deltaTime);

        playerAnimator.SetBool("InMove", moveInput.sqrMagnitude > 0.01f);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // ---------------- ATTACK ----------------

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
            isAttackingHeld = true;

        if (context.canceled)
            isAttackingHeld = false;
    }

    private void HandleAttack()
    {
        if (!isAttackingHeld) return;

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            Attack();
            attackTimer = attackCooldown;
        }
    }

    private void Attack()
    {
        playerAnimator.SetTrigger("Attack");

        
    }

   

    // ---------------- BLOCK ----------------

    public void OnBlock(InputAction.CallbackContext context)
    {
        bool isBlocking = context.ReadValueAsButton();

        moveSpeed = isBlocking ? baseMoveSpeed / 2 : baseMoveSpeed;

        playerAnimator.SetBool("IsBlocking", isBlocking);
    }

    // ---------------- LOOK ----------------

    private void LookAtMouse()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);

            Vector3 dir = point - transform.position;
            dir.y = 0;

            if (dir.sqrMagnitude > 0.001f)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * rotationSpeed);
            }
        }
    }
}