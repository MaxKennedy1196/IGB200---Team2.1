using UnityEngine;

public class Player : MonoBehaviour
{

    float xInput;
    float yInput;

    public SpriteRenderer spriteRenderer;

    Controls controls;

    public Vector2 PlayerMovementInput;

    void Awake()
    {
        controls = new Controls();

        controls.GamePlay.PlayerMove.performed += ctx => PlayerMovementInput = ctx.ReadValue<Vector2>();
        controls.GamePlay.PlayerMove.canceled += ctx => PlayerMovementInput = Vector2.zero;
    }

    void Start()
    {

    }


    void Update()
    {

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
