using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{


    public GameManager Manager;
    public Player player;
    public float communityCentreDist;

    public Button nextMonthButton;
    public Button coolBurnButton;
    public Button hotBurnButton;
    public Button planningButton;

    public Transform communityCentrePos;
    //public GameObject communityCentreMenuObj;
    
    [Header("Tutorial Text Game Objects")]
    public GameObject CommunityCentreIntroduction;
    public GameObject OpenMapIntroduction;
    public GameObject FieldGuideIntroduction;
    public GameObject CoolBurnIntroduction;
    public GameObject CoolBurnReaction;
    public GameObject GoYellowSquare;
    public GameObject PlanningCoolBurn;
    public GameObject PlanningComplete;
    public GameObject NextMonth;
    public GameObject CoolBurn2;
    public GameObject ColdSeasonEnd;
    public GameObject SpringStart;
    public GameObject GotoHotBurn;
    public GameObject DoHotBurn;
    public GameObject HotBurnReaction;
    public GameObject BUSHFIRE;
    public GameObject SuppressBushfire;
    public GameObject BushfireReaction;
    public GameObject FiresticksLovesYou;

    public int tutorialPhase;

    [Header("Planning Variables")]
    public Sector Sector9;
    public Sector Sector4;
    public Sector Sector2;
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
            nextMonthButton.interactable = false;
        }

        if (communityCentreDist > 3f && tutorialPhase == 1)
        {
            OpenMapIntroduction.SetActive(true);

            tutorialPhase = 2;
            nextMonthButton.interactable = false;
        }

        if (Manager.rangerBookOpen && tutorialPhase == 2)
        {
            FieldGuideIntroduction.SetActive(true);

            tutorialPhase = 3;
            nextMonthButton.interactable = false;
        }

        if (player.sectorCurrent == Sector9 && tutorialPhase == 3)
        {
            CoolBurnIntroduction.SetActive(true);

            tutorialPhase = 4;
            nextMonthButton.interactable = false;
        }

        if (Sector9.currentStatus == Sector.Status.coolBurn && tutorialPhase == 4)
        {
            CoolBurnReaction.SetActive(true);

            tutorialPhase = 5;
            nextMonthButton.interactable = false;
        }

        if (Manager.rangerBookOpen && tutorialPhase == 5)
        {
            GoYellowSquare.SetActive(true);

            tutorialPhase = 6;
            nextMonthButton.interactable = false;
        }

        if (player.sectorCurrent == Sector4 && tutorialPhase == 6)
        {
            PlanningCoolBurn.SetActive(true);

            tutorialPhase = 7;
            nextMonthButton.interactable = false;
        }

        if (Sector4.plannedTurns != 0 && tutorialPhase == 7)
        {
            PlanningComplete.SetActive(true);
            tutorialPhase = 8;
            nextMonthButton.interactable = false;
        }

        if (communityCentreDist < 2f && tutorialPhase == 8)
        {
            NextMonth.SetActive(true);
            tutorialPhase = 9;
            nextMonthButton.interactable = true;
        }

        if (communityCentreDist < 2f && tutorialPhase == 9)
        {
            NextMonth.SetActive(true);
            tutorialPhase = 10;
            nextMonthButton.interactable = true;
        }

        if (Manager.month == 4 && tutorialPhase == 10)
        {
            CoolBurn2.SetActive(true);
            tutorialPhase = 11;
            nextMonthButton.interactable = false;
        }

        if (Sector4.currentStatus == Sector.Status.coolBurn && tutorialPhase == 11)
        {
            ColdSeasonEnd.SetActive(true);
            tutorialPhase = 12;
            nextMonthButton.interactable = true;
            Manager.timeProgressionRate = 5;
        }

        if (Manager.month == 9 && tutorialPhase == 12)
        {
            SpringStart.SetActive(true);
            tutorialPhase = 13;

            Sector4.currentStatus = Sector.Status.healthy;
            Sector9.currentStatus = Sector.Status.healthy;
            Sector2.currentStatus = Sector.Status.veryDry;

            Manager.timeProgressionRate = 4;
            nextMonthButton.interactable = false;
        }

        if (Manager.rangerBookOpen == true && tutorialPhase == 13)
        {
            GotoHotBurn.SetActive(true);
            tutorialPhase = 14;

            Manager.timeProgressionRate = 1;
            nextMonthButton.interactable = false;
        }

        if (player.sectorCurrent == Sector2 && tutorialPhase == 14)
        {
            DoHotBurn.SetActive(true);

            tutorialPhase = 15;
            nextMonthButton.interactable = false;
        }

        if (Sector2.currentStatus == Sector.Status.hotBurn && tutorialPhase == 15)
        {
            HotBurnReaction.SetActive(true);

            tutorialPhase = 16;
            nextMonthButton.interactable = false;
        }


    }
    

}
