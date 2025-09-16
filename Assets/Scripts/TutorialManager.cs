using UnityEngine;

public class TutorialManager : MonoBehaviour
{


    public GameManager Manager;
    public Player player;
    public float communityCentreDist;

    public Transform communityCentrePos;

    public GameObject CommunityCentreIntroduction;
    public GameObject OpenMapIntroduction;
    public GameObject FieldGuideIntroduction;
    public GameObject CoolBurnIntroduction;
    public GameObject CoolBurnReaction;

    int tutorialPhase = 0;

    public Sector Sector9;
    public GameObject TownBorders;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        player = Manager.player;
    }

    // Update is called once per frame
    void Update()
    {
        communityCentreDist = Vector3.Distance(communityCentrePos.position, player.transform.position);

        if (tutorialPhase <= 2)
        {
            TownBorders.SetActive(true);
        }
        if (tutorialPhase > 2)
        {
            TownBorders.SetActive(false);
        }

        if (communityCentreDist < 2f && tutorialPhase == 0)
        {
            CommunityCentreIntroduction.SetActive(true);

            tutorialPhase = 1;
        }

        if (communityCentreDist > 3f && tutorialPhase == 1)
        {
            OpenMapIntroduction.SetActive(true);

            tutorialPhase = 2;
        }

        if (Manager.rangerBookOpen && tutorialPhase == 2)
        {
            FieldGuideIntroduction.SetActive(true);

            tutorialPhase = 3;
        }

        if (player.sectorCurrent == Sector9 && tutorialPhase == 3)
        {
            CoolBurnIntroduction.SetActive(true);

            tutorialPhase = 4;
        }

        if (Sector9.currentStatus == Sector.Status.coolBurn && tutorialPhase == 4)
        {
            CoolBurnReaction.SetActive(true);

            tutorialPhase = 5;
        }

    }
    

}
