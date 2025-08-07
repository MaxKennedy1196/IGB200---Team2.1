using UnityEngine;

public class Player : MonoBehaviour
{

    float xInput;
    float yInput;

    float speed = 5f;

    public SpriteRenderer spriteRenderer;

    Controls controls;

    public Vector2 PlayerMovementInput;

    public Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        controls = new Controls();

        controls.GamePlay.PlayerMove.performed += ctx => PlayerMovementInput = ctx.ReadValue<Vector2>();
        controls.GamePlay.PlayerMove.canceled += ctx => PlayerMovementInput = Vector2.zero;
    }

    void Start()
    {

    }


    void Update()
    {
        _rb.linearVelocity = PlayerMovementInput * speed;
    }
    
    void OnEnable()
    {
        controls.GamePlay.Enable();
    } 

    void OnDisable()
    {
        controls.GamePlay.Disable();
    } 
}
