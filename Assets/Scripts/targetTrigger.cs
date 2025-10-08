using UnityEngine;

public class targetTrigger : MonoBehaviour
{
    public GameManager Manager;
    
    public Sector sector;

    public Player player;
    public Transform playerTransform;

    public bool isActive;

    public float playerDistance;

    SpriteRenderer spriteRenderer;

    public AudioSource audioSource;

    public AudioClip activatedClip;

    public GameObject fireSprite;

    public GameObject planningFlag;

    public enum state
    {
        preActivation,
        activated,
        postActivation,
    }

    public state currentState;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        player = Manager.player;
        playerTransform = player.GetComponent<Transform>();

        sector = gameObject.GetComponentInParent<Sector>();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        

    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == state.preActivation)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        if (currentState == state.activated)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1);
            playerDistance = Vector3.Distance(playerTransform.position, transform.position);

            if (playerDistance <= 2f)
            {
                audioSource.PlayOneShot(activatedClip);
                sector.nextChallengePhase();
            }
        }
        if (currentState == state.postActivation)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0f);
        }
        
    }
}
