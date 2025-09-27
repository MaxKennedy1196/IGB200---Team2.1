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

    int growthTurns = 8;
    public bool isInMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        detectCurrentSector();
        if (isInMenu == false)
        {
            Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
            Manager.treeList.Add(gameObject.GetComponent<EnvironmentalTreeVisuals>());
        }
            
        
            
    }

    // Update is called once per frame
    void Update()
    {
        if (sectorCurrent != null)
        {
            if (sectorCurrent.currentStatus == Sector.Status.incinerated)
            {
                growthTurns = 0;
            }
            if (sectorCurrent.currentStatus == Sector.Status.hotBurn)
            {
                growthTurns = 0;
            }
            if (sectorCurrent.currentStatus == Sector.Status.coolBurn)
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
            
    }

    void detectCurrentSector()
    {
        Debug.DrawRay(transform.position, transform.forward * sectorDetectionRaycastDistance, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out sectorDetectionHit, sectorDetectionRaycastDistance))
        {
            if (sectorDetectionHit.collider.gameObject != null)
            {
                GameObject sectorGameObject = sectorDetectionHit.collider.gameObject;
                sectorCurrent = sectorGameObject.GetComponent<Sector>();
            }
                

        }
        else
        {

        }
    }

    public void nextTurn()
    {
        growthTurns += 1;
    }
}
