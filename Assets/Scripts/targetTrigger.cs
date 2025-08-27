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
        if (isActive == true)
        {
            spriteRenderer.color = new Color(1, 0, 0, 1);
            playerDistance = Vector3.Distance(playerTransform.position, transform.position);

            if (playerDistance <= 2f)
            {
                sector.nextChallengePhase();
            }
        }
        if (isActive == false)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
        }
    }
}
