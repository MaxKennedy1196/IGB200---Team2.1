using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sector : MonoBehaviour
{
    //Script for describing a sector and all related functions 
    public GameManager Manager;

    [Header("Sector Variables")]

    public int xPos;//x Position of this sector
    public int yPos;//y Position of this sector

    public float fuelLevel;//Current Fuel Level
    public float growthLevel;//Current Growth Level

    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI growthText;

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
                         { 6 , 2.5f },
                         { -3 , 8 }};


    float challengeScoreBonus = 0.4f;

    float challengeScoreBonusPlanned = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        Manager.sectorList.Add(gameObject.GetComponent<Sector>());//Add this sector to SectorList in the GameManager 

        fuelLevel += Random.Range(Manager.fuelSpawnMin, Manager.fuelSpawnMax);
        growthLevel += Random.Range(Manager.growthSpawnMin, Manager.growthSpawnMax);

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
        fuelText.text = "Fuel:" + Mathf.RoundToInt(fuelLevel);
        growthText.text = "Growth:" + Mathf.RoundToInt(growthLevel);

        if (fuelLevel <= 0f)//cap min fuel
        {
            fuelLevel = 0f;
        }

        if (fuelLevel >= 100f)//cap max fuel
        {
            fuelLevel = 100f;
        }

        if (growthLevel <= 0f)//cap min growth
        {
            growthLevel = 0f;
        }

        if (growthLevel >= 250f)//cap max growth
        {
            growthLevel = 250f;
        }

        if (wildfire == true)
        {
            fireImage.enabled = true;
        }
        if (wildfire == false)
        {
            fireImage.enabled = false;
        }

        environmentalChallenge();//handles all environmental challenge logic
    }

    void sectorInit()
    {
        fuelText.text = "Fuel:" + Mathf.RoundToInt(fuelLevel);
        growthText.text = "Growth:" + Mathf.RoundToInt(growthLevel);

        wildfire = false;
        fireImage.enabled = false;
    }

    public void nextMonth()
    {
        plannedTurns -= 1;
        if (plannedTurns <= 0)
        {
            plannedTurns = 0;
        }


        if (wildfire == true)
            {
                fuelLevel = 0f;
                growthLevel = 0f;
                wildfire = false;
                return;
            }
        if (wildfire == false)
        {
            fuelLevel += Random.Range(Manager.fuelIncreaseRateMin, Manager.fuelIncreaseRateMax);
            growthLevel += Random.Range(Manager.growthIncreaseRateMin, Manager.growthIncreaseRateMax);

            if (Manager.seasonName == "Spring")
            {
                float wildfireChance = Random.Range(80f, 100f);

                if (wildfireChance <= fuelLevel)
                {
                    startWildFire();
                }
            }
            if (Manager.seasonName == "Summer")
            {
                float wildfireChance = Random.Range(60f, 100f);

                if (wildfireChance <= fuelLevel)
                {
                    startWildFire();
                }
            }
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

        fuelLevel -= Random.Range(Manager.coolBurnFuelDecreaseMin * challengeScoreBonus, Manager.coolBurnFuelDecreaseMax * challengeScoreBonus);
        growthLevel -= Random.Range(Manager.coolBurnGrowthDecreaseMin, Manager.coolBurnGrowthDecreaseMax);

        print("Cool Burn Performed: " + challengeScore );
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
            

        fuelLevel -= Random.Range(Manager.hotBurnFuelDecreaseMin * challengeScoreBonus, Manager.hotBurnFuelDecreaseMax * challengeScoreBonus);
        growthLevel -= Random.Range(Manager.hotBurnGrowthDecreaseMin , Manager.hotBurnGrowthDecreaseMax);

        print("Hot Burn Performed");
    }

    private void completePlanning(float challengeScore)
    {
        plannedTurns = Manager.planningDuration;

        print("Planning Complete");
    }

    private void completeExtinguish(float challengeScore)
    {
        fuelLevel -= Random.Range(Manager.extinguishFuelDecreaseMin, Manager.extinguishFuelDecreaseMax);
        growthLevel -= Random.Range(Manager.extinguishGrowthDecreaseMin, Manager.extinguishGrowthDecreaseMax);

        wildfire = false;

        print("Extinguish Performed");
    }



    private void beginEnvironmentalChallenge()
    {
        randomizeChallengePattern();// randomizes current pattern
        challengeEnabled = true;
        challengePhase = 1;
    }
    
    private void randomizeChallengePattern()
    {
        int randomizer = Random.Range(1, 4);

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
