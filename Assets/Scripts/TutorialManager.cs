using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class TutorialManager : MonoBehaviour
{


    public GameManager Manager;
    public Player player;
    public float communityCentreDist;

    public Button nextMonthButton;
    public Button coolBurnButton;
    public Button hotBurnButton;
    public Button planningButton;
    public Button awarenessButton;

    public Transform communityCentrePos;
    //public GameObject communityCentreMenuObj;

    [Header("Tutorial Text Game Objects")]
    public GameObject WelcomeToFiresticks;
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
    public Sector Sector7;
    public GameObject TownBorders;

    public GameObject Sector7Borders;

    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        player = Manager.player;

        //Manager.actionPointsCurrent = 7;
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

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = true;
            Manager.extinguishInteractableOverride = false;

        }

        if (Manager.awarenessRaised == true && tutorialPhase == 1)
        {
            OpenMapIntroduction.SetActive(true);

            tutorialPhase = 2;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (Manager.rangerBookOpen && tutorialPhase == 2)
        {
            FieldGuideIntroduction.SetActive(true);

            tutorialPhase = 3;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (player.sectorCurrent == Sector9 && tutorialPhase == 3)
        {
            CoolBurnIntroduction.SetActive(true);

            tutorialPhase = 4;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = true;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (Sector9.currentStatus == Sector.Status.coolBurn && tutorialPhase == 4)
        {
            CoolBurnReaction.SetActive(true);

            tutorialPhase = 5;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (Manager.rangerBookOpen && tutorialPhase == 5)
        {
            GoYellowSquare.SetActive(true);

            tutorialPhase = 6;
            nextMonthButton.interactable = false;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (player.sectorCurrent == Sector4 && tutorialPhase == 6)
        {
            PlanningCoolBurn.SetActive(true);

            tutorialPhase = 7;
            nextMonthButton.interactable = false;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = true;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (Sector4.plannedTurns != 0 && tutorialPhase == 7)
        {
            PlanningComplete.SetActive(true);
            tutorialPhase = 8;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (communityCentreDist < 2f && tutorialPhase == 8)
        {
            NextMonth.SetActive(true);
            tutorialPhase = 9;

            Manager.nextMonthInteractableOverride = true;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }


        if (Manager.month == 8 && tutorialPhase == 9)
        {
            CoolBurn2.SetActive(true);
            tutorialPhase = 10;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = true;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (Sector4.currentStatus == Sector.Status.coolBurn && tutorialPhase == 10)
        {
            ColdSeasonEnd.SetActive(true);
            tutorialPhase = 11;
            //Manager.timeProgressionRate = 5;

            Manager.nextMonthInteractableOverride = true;
            Manager.coolBurnInteractableOverride = true;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
            Sector7Borders.SetActive(false);
        }


        if (Manager.rangerBookOpen == true && tutorialPhase == 12)
        {
            GotoHotBurn.SetActive(true);
            tutorialPhase = 13;

            Manager.timeProgressionRate = 1;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
            Sector7Borders.SetActive(true);
        }

        if (player.sectorCurrent == Sector2 && tutorialPhase == 13)
        {
            DoHotBurn.SetActive(true);

            tutorialPhase = 14;

            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = true;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
            Sector7Borders.SetActive(true);
        }

        if (Sector2.currentStatus == Sector.Status.hotBurn && tutorialPhase == 14)
        {
            HotBurnReaction.SetActive(true);

            tutorialPhase = 15;

            Manager.nextMonthInteractableOverride = true;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
            Sector7Borders.SetActive(false);
            
        }

        if (Manager.month == 10 && tutorialPhase == 15)
        {
            BUSHFIRE.SetActive(true);
            Sector7.wildfire = true;

            tutorialPhase = 16;
            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
            Sector7Borders.SetActive(false);
        }

        if (player.sectorCurrent == Sector7 && tutorialPhase == 16)
        {
            SuppressBushfire.SetActive(true);

            tutorialPhase = 17;
            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = true;
        }

        if (Sector7.currentStatus == Sector.Status.veryDry && Sector7.wildfire == false && tutorialPhase == 17)
        {
            BushfireReaction.SetActive(true);

            tutorialPhase = 18;
            Manager.nextMonthInteractableOverride = false;
            Manager.coolBurnInteractableOverride = false;
            Manager.hotBurnInteractableOverride = false;
            Manager.planningInteractableOverride = false;
            Manager.awarenessInteractableOverride = false;
            Manager.extinguishInteractableOverride = false;
        }

        if (BushfireReaction.activeInHierarchy == false && tutorialPhase == 18)
        {
            SceneManager.LoadScene("Tutorial2Game");
        }
    }

    public void repeatMessage()
    {
        print("REPEAT MESSAGE");

        if (tutorialPhase == 0)
        {
            WelcomeToFiresticks.SetActive(true);
        }

        if (tutorialPhase == 1)
        {
            CommunityCentreIntroduction.SetActive(true);
        }

        if (tutorialPhase == 2)
        {
            OpenMapIntroduction.SetActive(true);
        }

        if (tutorialPhase == 3)
        {
            FieldGuideIntroduction.SetActive(true);
        }

        if (tutorialPhase == 4)
        {
            CoolBurnIntroduction.SetActive(true);
        }

        if (tutorialPhase == 5)
        {
            CoolBurnReaction.SetActive(true);
        }

        if (tutorialPhase == 6)
        {
            GoYellowSquare.SetActive(true);
        }

        if (tutorialPhase == 7)
        {
            PlanningCoolBurn.SetActive(true);
        }

        if (tutorialPhase == 8)
        {
            PlanningComplete.SetActive(true);
        }

        if (tutorialPhase == 9)
        {
            NextMonth.SetActive(true);
        }

        if (tutorialPhase == 10)
        {
            CoolBurn2.SetActive(true);
        }

        if (tutorialPhase == 11)
        {
            ColdSeasonEnd.SetActive(true);
        }

        if (tutorialPhase == 12)
        {
            SpringStart.SetActive(true);
        }

        if (tutorialPhase == 13)
        {
            GotoHotBurn.SetActive(true);
        }

        if (tutorialPhase == 14)
        {
            DoHotBurn.SetActive(true);
        }

        if (tutorialPhase == 15)
        {
            HotBurnReaction.SetActive(true);
        }

        if (tutorialPhase == 16)
        {
            BUSHFIRE.SetActive(true);
        }

        if (tutorialPhase == 17)
        {
            SuppressBushfire.SetActive(true);
        }

        if (tutorialPhase == 18)
        {
            BushfireReaction.SetActive(true);
        }
    }

    public void OnSpringFadeComplete()
    {
        if (Manager.month == 9 && tutorialPhase == 11)
        {
            StartCoroutine(SpringFadeSequence());
        }
    }

    private IEnumerator SpringFadeSequence()
    {
        SpringStart.SetActive(true);
        tutorialPhase = 12;

        Sector4.currentStatus = Sector.Status.healthy;
        Sector9.currentStatus = Sector.Status.healthy;
        Sector2.currentStatus = Sector.Status.veryDry;
        Sector7.currentStatus = Sector.Status.veryDry;

        Manager.timeProgressionRate = 4;

        Manager.nextMonthInteractableOverride = false;
        Manager.coolBurnInteractableOverride = false;
        Manager.hotBurnInteractableOverride = false;
        Manager.planningInteractableOverride = false;
        Manager.awarenessInteractableOverride = false;
        Manager.extinguishInteractableOverride = false;

        Debug.Log("SPRING FADE COMPLETE");

        var fadeUI = FindObjectOfType<FadeUI>();
        if (fadeUI != null)
        {
            fadeUI.OnFadeComplete = null;
        }

        yield return null;
    }


}
