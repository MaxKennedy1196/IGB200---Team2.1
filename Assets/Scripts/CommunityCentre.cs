using UnityEngine;

public class CommunityCentre : MonoBehaviour
{
    public GameManager Manager;
    public Player player;
    public Transform playerTransform;
    public float playerDistance;

    public bool playerInRange;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        player = Manager.player;
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerDistance = Vector3.Distance(playerTransform.position, transform.position);

        if (playerDistance <= 2f)
        {
            playerInRange = true;
        }
        if (playerDistance > 2f)
        {
            playerInRange = false;
        }
    }
    

}
