
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameManager Manager;

    float speed = 15f;// How fast the player is 
    float maxSpeed = 15f;

    public SpriteRenderer spriteRenderer;//

    Controls controls;

    public Vector2 playerMovementInput;//input vector for play movement 

    public Rigidbody2D rb;//Player Rigidbody

    // Xavier this is my attempt to add joystick support because i could not understand your code - Max :)
    public Joystick joystick;

    [Header("Sector Detection Variables")]

    float sectorDetectionRaycastDistance = 100f;// How long Sector detect raycast is 
    RaycastHit sectorDetectionHit;// Output variable for sector detection raycast
    public Sector sectorCurrent;// Current sector the player is in

    [Header("Animation Variables")]
    public Animator anim;

    bool isLocked = false;
    public bool isInMenu;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        controls = new Controls();

        controls.GamePlay.PlayerMove.performed += ctx => playerMovementInput = ctx.ReadValue<Vector2>();
        controls.GamePlay.PlayerMove.canceled += ctx => playerMovementInput = Vector2.zero;
    }

    void Start()
    {
        if (isInMenu == false)
        {
            Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        }
    }



    void Update()
    {
        if (isLocked == true)
        {
            playerMovementInput = new Vector2(0, 0);
            speed = 0f;
        }
        if (isLocked == false)
        {
            speed = maxSpeed;

            Vector2 joystickInput = new Vector2(joystick.Horizontal, joystick.Vertical);
            Vector2 combinedInput = playerMovementInput + joystickInput;

            combinedInput = Vector2.ClampMagnitude(combinedInput, 1f);

            rb.linearVelocity = combinedInput * speed;

        }


        detectCurrentSector();

        anim.SetFloat("Horizontal", playerMovementInput.x);
        anim.SetFloat("Vertical", playerMovementInput.y);

        anim.SetFloat("Horizontal", joystick.Horizontal);
        anim.SetFloat("Vertical", joystick.Vertical);

        if (playerMovementInput != Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", playerMovementInput.x);
            anim.SetFloat("LastVertical", playerMovementInput.y);
        }

    }

    void detectCurrentSector()
    {
        Debug.DrawRay(transform.position, transform.forward * sectorDetectionRaycastDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out sectorDetectionHit, sectorDetectionRaycastDistance))
        {
            GameObject sectorGameObject = sectorDetectionHit.collider.gameObject;
            sectorCurrent = sectorGameObject.GetComponent<Sector>();
            // Debug.Log("Raycast sectorDetectionHit: " + sectorDetectionHit.collider.gameObject.name);
        }
        else
        {
            //Debug.Log("Nothing Detected");
        }
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    }

    void OnDisable()
    {
        controls.GamePlay.Disable();
    }

    public void lockPlayer()
    {
        isLocked = true;
        playerMovementInput = new Vector2(0, 0);
        speed = 0f;
        rb.linearVelocity = playerMovementInput * speed;
    }
    public void unlockPlayer()
    {
        isLocked = false;
    }
}
