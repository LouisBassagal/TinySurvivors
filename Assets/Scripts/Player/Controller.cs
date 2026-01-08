using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    [Header("Player Settings")]
    public float moveSpeed = 10f;

    [Header("Input Actions")]
    [SerializeField]
    private InputActionAsset m_inputActions;
    private InputActionMap m_playerActionMap;
    private InputAction m_moveAction;

    [Header("Animation Manager")]
    [SerializeField]
    private AnimationManager animationManager;

    private GameObject m_player;
    private bool m_isMoving = false;

    private float m_autoAttackCountdown = 0f;

    void Awake()
    {
        m_playerActionMap = m_inputActions.FindActionMap("Player");
        m_moveAction = m_playerActionMap.FindAction("Move");

        m_moveAction.performed += OnMovePerformed;
        m_moveAction.canceled += OnMoveCanceled;
        m_player = this.gameObject;
    }

    private void Update()
    {
        HandleMovement();
        HandleAttack();
    }

    /// <summary>
    /// Handles player movement based on input.
    /// </summary>
    private void HandleMovement()
    {
        animationManager.SetMoving(m_isMoving);
        if (!m_isMoving)
            return;

        Vector2 movement = m_moveAction.ReadValue<Vector2>();
        m_player.transform.Translate(moveSpeed * Time.deltaTime * (Vector3)movement);
    }

    private void HandleAttack()
    {
        if (m_autoAttackCountdown > 0f)
        {
            m_autoAttackCountdown -= Time.deltaTime;
            return;
        }

        animationManager.TriggerAttack();
        m_autoAttackCountdown = 2.5f;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();

        if (movement.x < 0)
            m_player.transform.localScale = new Vector3(-1, 1, 1);
        else if (movement.x > 0)
            m_player.transform.localScale = new Vector3(1, 1, 1);

        m_isMoving = true;
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        m_isMoving = false;
    }
}
