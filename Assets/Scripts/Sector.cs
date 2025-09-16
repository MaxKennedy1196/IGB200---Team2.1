using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Data;



public class Sector : MonoBehaviour
{
    //Script for describing a sector and all related functions 
    public GameManager Manager;

    [Header("Sector Variables")]

    public int xPos;//x Position of this sector
    public int yPos;//y Position of this sector



    public enum Status
    {
        incinerated,
        hotBurn,
        coolBurn,
        healthy,
        dry,
        veryDry
    }

    public Status currentStatus;

    [Header("Planning Variables")]

    public int plannedTurns;

    [Header("Wildfire Variables")]
    public Image fireImage;
    
    public bool wildfire;

    [Header("Environmental Challenge Variables")]

    public bool challengeEnabled;
    private string currentAction;
    float challengeTimer = 0f;
    float challengeRating = 20f;
    int challengePhase = 1;

    public GameObject challengeBorders;
    public List<GameObject> challengeTargetList = new List<GameObject>();
    public List<targetTrigger> challengeTriggerList = new List<targetTrigger>();

    public GameObject tileMapHealthy;
    public GameObject tileMapYellow;
    public GameObject tileMapOrange;
    public GameObject tileMapBurnedOverlay;

    public bool incinerated;
    public bool coolBurned;

    public bool randomInital;

    public bool isTownCentre;

    float[,] currentPattern;

    float[,] pattern1 = {{ -5, 0},
                         { -2.5f, 0},
                         {  0,   0},
                         {  2.5f, 0},
                         {  5, 0}};

    float[,] pattern2 = {{ -5, -4},
                         { -2.5f, -2},
                         {  0,   0},
                         {  2.5f, 2},
                         {  5, 4}};

    float[,] pattern3 = {{ 5 , 4},
                         { -2 , 1.5f},
                         { 0 , -3 },
                         { 5 , 2.5f },
                         { -3 , 5 }};

    float[,] pattern4 = {{ -5, -5},
                         { -2.5f, 5},
                         {  0,   -5},
                         {  2.5f, 5},
                         {  5, -5}};

    float[,] pattern5 = {{ -5, 5},
                         { -2.5f, -5},
                         {  0,   5},
                         {  2.5f, -5},
                         {  5, 5}};

    float[,] pattern6 = {{-5, 5  },
                         {-5, -5  },
                         {5, -5  },
                         {5, 5 },
                         {0, 0 }};

    float challengeScoreBonus = 0.4f;

    float challengeScoreBonusPlanned = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        Manager.sectorList.Add(gameObject.GetComponent<Sector>());//Add this sector to SectorList in the GameManager 


        sectorInit();


        foreach (GameObject target in challengeTargetList)
        {
            challengeTriggerList.Add(target.GetComponent<targetTrigger>());
        }

        //foreach (targetTrigger trigger in challengeTriggerList)
        //{
        //    trigger.sector = gameObject.GetComponent<Sector>();
        //}


    }



    void Update()
    {
        if (isTownCentre == true)
        {
            currentStatus = Status.healthy;
            wildfire = false;
        }


        if (wildfire == true)
        {
            fireImage.enabled = true;
        }
        if (wildfire == false)
        {
            fireImage.enabled = false;
        }

        updateDisplayTiles();

        environmentalChallenge();//handles all environmental challenge logic

        
    }

    private void updateDisplayTiles()
    {

        tileMapOrange.SetActive(false);
        tileMapYellow.SetActive(false);
        tileMapHealthy.SetActive(false);
        tileMapBurnedOverlay.SetActive(false);

        if (currentStatus == Status.veryDry)
        {
            tileMapOrange.SetActive(true);
        }
        if (currentStatus == Status.dry)
        {
            tileMapYellow.SetActive(true);
        }
        if (currentStatus == Status.healthy)
        {
            tileMapHealthy.SetActive(true);
        }
        if (currentStatus == Status.incinerated)
        {
            tileMapBurnedOverlay.SetActive(true);
        }
    }

    void sectorInit()
    {
        if (randomInital == true)
        {
            currentStatus = Status.healthy;

            dryRandomise();

            wildfire = false;
            fireImage.enabled = false;
        }
            
    }

    void dryRandomise()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.dryChance)
        {
            currentStatus = Status.dry;
        }
    }

    void veryDryRandomise()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.veryDryChance)
        {
            currentStatus = Status.veryDry;
        }
    }

    void healthyRandomiseFromCoolBurn()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromCoolBurn)
        {
            currentStatus = Status.healthy;
        }
    }

    void healthyRandomiseFromHotBurn()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromHotBurn)
        {
            currentStatus = Status.healthy;
        }
    }

    void healthyRandomiseFromIncinerated()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromIncinerated)
        {
            currentStatus = Status.healthy;
        }
    }
    

    public void nextMonth()
    {
        incinerated = false;
        coolBurned = false;

        plannedTurns -= 1;
        if (plannedTurns <= 0)
        {
            plannedTurns = 0;
        }


        if (wildfire == true)
        {
            completeWildfire();
            return;
        }
        if (wildfire == false)
        {
            updateStatus();


            if (currentStatus == Status.veryDry)
            {
                if (Manager.seasonName == "Spring")
                {
                    float randomiser = Random.Range(0f, 100f);

                    if (randomiser <= Manager.wildfireChanceSpring)
                    {
                        startWildFire();
                    }
                }
                if (Manager.seasonName == "Summer")
                {
                    float randomiser = Random.Range(0f, 100f);

                    if (randomiser <= Manager.wildfireChanceSummer)
                    {
                        startWildFire();
                    }
                }
            }

            return;
        }

    }

    void updateStatus()
    {
        if (currentStatus == Status.coolBurn)
        {
            healthyRandomiseFromCoolBurn();
            return;
        }
        if (currentStatus == Status.hotBurn)
        {
            healthyRandomiseFromHotBurn();
            return;
        }
        if (currentStatus == Status.incinerated)
        {
            healthyRandomiseFromIncinerated();
            return;
        }
        if (currentStatus == Status.healthy)
        {
            dryRandomise();
            return;
        }
        if (currentStatus == Status.dry)
        {
            veryDryRandomise();
            return;
        }
    }

    private void startWildFire()
    {
        fireImage.enabled = true;
        wildfire = true;

        print("WILDFIRE!!!");

    }

    public void startCoolBurn()
    {
        currentAction = "coolBurn";
        beginEnvironmentalChallenge();
    }

    public void startHotBurn()
    {
        currentAction = "hotBurn";

        beginEnvironmentalChallenge();
    }

    public void startPlanning()
    {
        currentAction = "planning";
        beginEnvironmentalChallenge();
    }

    public void startExtinguish()
    {
        currentAction = "extinguish";
        beginEnvironmentalChallenge();
    }


    private void completeWildfire()
    {
        wildfire = false;
        currentStatus = Status.incinerated;
    }

    private void completeCoolBurn(float challengeScore)
    {
        if (plannedTurns == 0)
        {
            challengeScore += challengeScoreBonus;
        }
        if (plannedTurns != 0)
        {
            challengeScore += challengeScoreBonusPlanned;
            plannedTurns = 0;
        }

        currentStatus = Status.coolBurn;
        print("Cool Burn Performed: " + challengeScore);
    }

    private void completeHotBurn(float challengeScore)
    {
        if (plannedTurns == 0)
        {
            challengeScore += challengeScoreBonus;
        }
        if (plannedTurns != 0)
        {
            challengeScore += challengeScoreBonusPlanned;
            plannedTurns = 0;
        }

        currentStatus = Status.hotBurn;
        print("Hot Burn Performed");
    }

    private void completePlanning(float challengeScore)
    {
        plannedTurns = Manager.planningDuration;

        print("Planning Complete");
    }

    private void completeExtinguish(float challengeScore)
    {
        wildfire = false;
        print("Extinguish Performed");
    }



    private void beginEnvironmentalChallenge()
    {
        randomizeChallengePattern();// randomizes current pattern
        challengeEnabled = true;
        challengePhase = 1;
        challengeTimer = 0f;
    }

    private void randomizeChallengePattern()
    {
        int randomizer = Random.Range(1, 7);

        if (randomizer == 1)
        {
            currentPattern = pattern1;
        }
        if (randomizer == 2)
        {
            currentPattern = pattern2;
        }
        if (randomizer == 3)
        {
            currentPattern = pattern3;
        }
        if (randomizer == 4)
        {
            currentPattern = pattern4;
        }
        if (randomizer == 5)
        {
            currentPattern = pattern5;
        }
        if (randomizer == 6)
        {
            currentPattern = pattern6;
        }
    }

    private void environmentalChallenge()
    {

        if (challengeEnabled == true)
        {
            challengeTimer += Time.deltaTime;
            print(challengeTimer);

            challengeBorders.SetActive(true);

            int iterator = 0;
            foreach (GameObject target in challengeTargetList)
            {
                target.transform.position = transform.position + new Vector3(currentPattern[iterator, 0], currentPattern[iterator, 1]);
                iterator += 1;
                target.SetActive(true);
            }

            if (challengePhase == 1)
            {
                challengeTriggerList[0].isActive = true;
                challengeTriggerList[1].isActive = false;
                challengeTriggerList[2].isActive = false;
                challengeTriggerList[3].isActive = false;
                challengeTriggerList[4].isActive = false;
            }

            if (challengePhase == 2)
            {
                challengeTriggerList[0].isActive = false;
                challengeTriggerList[1].isActive = true;
                challengeTriggerList[2].isActive = false;
                challengeTriggerList[3].isActive = false;
                challengeTriggerList[4].isActive = false;
            }

            if (challengePhase == 3)
            {
                challengeTriggerList[0].isActive = false;
                challengeTriggerList[1].isActive = false;
                challengeTriggerList[2].isActive = true;
                challengeTriggerList[3].isActive = false;
                challengeTriggerList[4].isActive = false;
            }

            if (challengePhase == 4)
            {
                challengeTriggerList[0].isActive = false;
                challengeTriggerList[1].isActive = false;
                challengeTriggerList[2].isActive = false;
                challengeTriggerList[3].isActive = true;
                challengeTriggerList[4].isActive = false;
            }

            if (challengePhase == 5)
            {
                challengeTriggerList[0].isActive = false;
                challengeTriggerList[1].isActive = false;
                challengeTriggerList[2].isActive = false;
                challengeTriggerList[3].isActive = false;
                challengeTriggerList[4].isActive = true;
            }

            if (currentAction == "coolBurn" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeCoolBurn(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "hotBurn" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeHotBurn(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "extinguish" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeExtinguish(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "planning" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completePlanning(challengeScoreInverted);
                environmentalChallengeReset();
            }
        }
        if (challengeEnabled == false)
        {
            challengeBorders.SetActive(false);
        }
    }

    private void environmentalChallengeReset()
    {
        currentAction = "null";
        challengeEnabled = false;
        challengePhase = 1;

        foreach (GameObject target in challengeTargetList)
        {
            target.SetActive(false);
        }

        foreach (targetTrigger trigger in challengeTriggerList)
        {
            trigger.isActive = false;
        }
    }

    public void nextChallengePhase()
    {
        challengePhase += 1;
    }




}
