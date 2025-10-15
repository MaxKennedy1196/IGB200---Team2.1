

using UnityEngine;
using UnityEngine.Animations;

public class Player : MonoBehaviour
{
    public GameManager Manager;

    float speed = 15f;// How fast the player is 
    float maxSpeed = 15f;

    public SpriteRenderer spriteRenderer;//

    Controls controls;

    public Vector2 playerMovementInput;//input vector for play movement 
    Vector2 combinedInput;

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

    public enum Hat
    {
        Akubra,
        BillyHat,
        BinChickenHat,
        BucketHat,
        CorkHat,
        Default,
        Helmet,
        NoHat
    }

    public Hat currentHat;

    public RuntimeAnimatorController Akubra;
    public RuntimeAnimatorController BillyHat;
    public RuntimeAnimatorController BinChickenHat;
    public RuntimeAnimatorController BucketHat;
    public RuntimeAnimatorController CorkHat;
    public RuntimeAnimatorController Default;
    public RuntimeAnimatorController Helmet;
    public RuntimeAnimatorController NoHat;



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
            combinedInput = playerMovementInput + joystickInput;

            combinedInput = Vector2.ClampMagnitude(combinedInput, 1f);

            rb.linearVelocity = combinedInput * speed;
        }


        detectCurrentSector();

        anim.SetFloat("Horizontal", combinedInput.x);
        anim.SetFloat("Vertical", combinedInput.y);



        if (combinedInput != Vector2.zero)
        {
            anim.SetFloat("LastHorizontal", combinedInput.x);
            anim.SetFloat("LastVertical", combinedInput.y);
        }

        updateHatAnimController();

    }

    void updateHatAnimController()
    {
        if (currentHat == Hat.Akubra)
        {
            anim.runtimeAnimatorController = Akubra;
        }
        if (currentHat == Hat.BillyHat)
        {
            anim.runtimeAnimatorController = BillyHat;
        }
        if (currentHat == Hat.BinChickenHat)
        {
            anim.runtimeAnimatorController = BinChickenHat;
        }
        if (currentHat == Hat.BucketHat)
        {
            anim.runtimeAnimatorController = BucketHat;
        }
        if (currentHat == Hat.CorkHat)
        {
            anim.runtimeAnimatorController = CorkHat;
        }
        if (currentHat == Hat.Default)
        {
            anim.runtimeAnimatorController = Default;
        }
        if (currentHat == Hat.Helmet)
        {
            anim.runtimeAnimatorController = Helmet;
        }
        if(currentHat == Hat.NoHat)
        {
            anim.runtimeAnimatorController = NoHat;
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
