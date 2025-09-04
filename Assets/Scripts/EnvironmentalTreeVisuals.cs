using UnityEngine;

public class EnvironmentalTreeVisuals : MonoBehaviour
{
    public GameManager Manager;

    float sectorDetectionRaycastDistance = 100f;// How long Sector detect raycast is 
    RaycastHit sectorDetectionHit;// Output variable for sector detection raycast
    public Sector sectorCurrent;// Current sector the tree is in

    public SpriteRenderer spriteRenderer;

    public Sprite Healthy;
    public Sprite CoolBurn;
    public Sprite Burned;

    int growthTurns = 6;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        detectCurrentSector();
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager

        Manager.treeList.Add(gameObject.GetComponent<EnvironmentalTreeVisuals>());
    }

    // Update is called once per frame
    void Update()
    {
        if (sectorCurrent.burned == true)
        {
            growthTurns = 0;
        }
        if (sectorCurrent.coolBurned == true)
        {
            growthTurns = 3;
        }

        if (growthTurns < 3)
        {
            spriteRenderer.sprite = Burned;
        }
        if (growthTurns >= 3 && growthTurns < 7)
        {
            spriteRenderer.sprite = CoolBurn;
        }
        if (growthTurns >= 7)
        {
            spriteRenderer.sprite = Healthy;
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

    public void nextTurn()
    {
        growthTurns += 1;
    }
}
