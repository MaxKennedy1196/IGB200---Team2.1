using UnityEngine;

public class Player : MonoBehaviour
{

    float speed = 10f;// How fast the player is 

    public SpriteRenderer spriteRenderer;//

    Controls controls;

    public Vector2 playerMovementInput;//input vector for play movement 

    public Rigidbody2D rb;//Player Rigidbody

    [Header("Sector Detection Variables")]

    float sectorDetectionRaycastDistance = 100f;// How long Sector detect raycast is 
    RaycastHit sectorDetectionHit;// Output variable for sector detection raycast
    public Sector sectorCurrent;// Current sector the player is in

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        controls = new Controls();

        controls.GamePlay.PlayerMove.performed += ctx => playerMovementInput = ctx.ReadValue<Vector2>();
        controls.GamePlay.PlayerMove.canceled += ctx => playerMovementInput = Vector2.zero;
    }

    void Start()
    {

    }

    void Update()
    {
        rb.linearVelocity = playerMovementInput * speed;
        detectCurrentSector();
    }

    void detectCurrentSector()
    {
        Debug.DrawRay(transform.position, transform.forward * sectorDetectionRaycastDistance, Color.red); 
        if (Physics.Raycast(transform.position, transform.forward, out sectorDetectionHit, sectorDetectionRaycastDistance))
        {
            GameObject sectorGameObject = sectorDetectionHit.collider.gameObject;
            sectorCurrent = sectorGameObject.GetComponent<Sector>();
            Debug.Log("Raycast sectorDetectionHit: " + sectorDetectionHit.collider.gameObject.name);
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
}
