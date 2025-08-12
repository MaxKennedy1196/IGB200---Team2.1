using UnityEngine;

public class LayeringFix : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public GameManager Manager;
    public Player player;
    Transform playerTransform;
    public float yPos;
    public int orderPos;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find gamemanager
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();//find Player     
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(spriteRenderer == null)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();// Get renderer
        }

        
    }

    void Update()
    {
        Vector2 positionScaledToCamera = transform.position - playerTransform.position;
        yPos = positionScaledToCamera.y * 16;
        yPos = Mathf.Round(yPos);
        orderPos = (int)yPos;
        spriteRenderer.sortingOrder = orderPos * -1;
    }

}
