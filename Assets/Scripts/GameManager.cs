using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    Controls controls;
    public List<Sector> sectorList = new List<Sector>();//List of all sectors in the game

    [Header("Fuel Variables")]
    public float fuelSpawnMin;
    public float fuelSpawnMax;

    public float fuelIncreaseRateMin;
    public float fuelIncreaseRateMax;

    [Header("Growth Variables")]
    public float growthSpawnMin;
    public float growthSpawnMax;

    public float growthIncreaseRateMin;
    public float growthIncreaseRateMax;


    [Header("Cool Burn Variables")]
    public Button coolBurnButton;

    public float coolBurnFuelDecreaseMin;
    public float coolBurnFuelDecreaseMax;

    public float coolBurnGrowthDecreaseMin;
    public float coolBurnGrowthDecreaseMax;

    public int coolBurnAPCost;


    [Header("Hot Burn Variables")]
    public Button hotBurnButton;

    public float hotBurnFuelDecreaseMin;
    public float hotBurnFuelDecreaseMax;

    public float hotBurnGrowthDecreaseMin;
    public float hotBurnGrowthDecreaseMax;

    public int hotBurnAPCost;


    [Header("Extinguish Variables")]
    public Button extinguishButton;

    public float extinguishFuelDecreaseMin;
    public float extinguishFuelDecreaseMax;

    public float extinguishGrowthDecreaseMin;
    public float extinguishGrowthDecreaseMax;

    public int extinguishAPCost;


    [Header("Action Point Variables")]
    public int actionPointsMax;
    public int actionPointsIncreaseRate;
    public int actionPointsCurrent;

    public TextMeshProUGUI actionPointsRemainingText;


    [Header("Scoring Variables")]
    public int score;
    public int scoreHigh;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreHighText;


    [Header("Month + Season Variables")]
    public string monthName = "";// Displays the name of the current month Updated by updateSeasonMonthNames()
    public string seasonName = "";// Displays the name of the current Season Updated by updateSeasonMonthNames()

    public TextMeshProUGUI monthText;
    public TextMeshProUGUI seasonText;

    int month = 3;//Current Month Number 
    int monthsTotal = 1; //Total months Ellapsed

    [Header("Ranger Book Variables")]

    public GameObject RangerBook;

    public Image mapSectorTL;
    public Image mapSectorTM;
    public Image mapSectorTR;

    public Image mapSectorML;
    public Image mapSectorMM;
    public Image mapSectorMR;
    
    public Image mapSectorBL;
    public Image mapSectorBM;
    public Image mapSectorBR;

    public bool rangerBookOpen;

    [Header("Environmental Challenge Variables")]

    public bool challengeEnabled;

    private string currentAction;

    float challengeTimer = 0f;

    float challengeRating = 10f;

    int challengePhase = 1;

    public List<GameObject> challengeTargetList = new List<GameObject>();

    public List<Button> challengeButtonList = new List<Button>();


    float[,] currentPattern;

    float[,] pattern1 = {{ -500, 0},
                         { -250, 0},
                         {  0,   0},
                         {  250, 0},
                         {  500, 0}}; 

    float[,] pattern2 = {{ -500, -400},
                         { -250, -200},
                         {  0,   0},
                         {  250, 200},
                         {  500, 400}}; 

    float[,] pattern3 = {{ 500 , 400 },
                         { -200 , 150 },
                         { 0 , -300 },
                         { 600 , 25 },
                         { -300 , 80 }}; 


    void Awake()
    {
        controls = new Controls();

        controls.GamePlay.RangerBook.performed += ctx => rangerBook();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateSeasonMonthNames();

        actionPointsCurrent = actionPointsMax;

        foreach (GameObject gameObject in challengeTargetList)
        {
            challengeButtonList.Add(gameObject.GetComponent<Button>());
        }

    }

    void rangerBook()
    {
        if (rangerBookOpen == false)
        {
            RangerBook.SetActive(true);
            rangerBookOpen = true;
            return;
        }
        if (rangerBookOpen == true)
        {
            RangerBook.SetActive(false);
            rangerBookOpen = false;
            return;
        }
        
    }

    void Update()
    {
        scoreUpdate();

        checkCoolBurnAvailable();//check if this action can be performed
        checkHotBurnAvailable();//check if this action can be performed
        checkExtinguishAvailable();//check if this action can be performed

        updateMiniMapColours();//updates the minimaps fire rating

        environmentalChallenge();//handles all environmental challenge logic


        actionPointsRemainingText.text = "Action Points Remaining: " + actionPointsCurrent;

    }

    private Sector locateSectorInList(int XPos, int YPos)
    {
        foreach (Sector sector in sectorList)
        {
            if (sector.xPos == XPos && sector.yPos == YPos)
            {
                return sector;
            }
        }
        return null;
    }

    private void updateMiniMapColours()
    {
        Sector sectorTL = locateSectorInList(-1, 1);
        Sector sectorTM = locateSectorInList(0, 1);
        Sector sectorTR = locateSectorInList(1, 1);

        Sector sectorML = locateSectorInList(-1, 0);
        Sector sectorMM = locateSectorInList(0, 0);
        Sector sectorMR = locateSectorInList(1, 0);

        Sector sectorBL = locateSectorInList(-1, -1);
        Sector sectorBM = locateSectorInList(0, -1);
        Sector sectorBR = locateSectorInList(1, -1);

        float fuelValueTL = sectorTL.fuelLevel / 100f;
        float fuelValueTM = sectorTM.fuelLevel / 100f;
        float fuelValueTR = sectorTR.fuelLevel / 100f;

        float fuelValueML = sectorML.fuelLevel / 100f;
        float fuelValueMM = sectorMM.fuelLevel / 100f;
        float fuelValueMR = sectorMR.fuelLevel / 100f;

        float fuelValueBL = sectorBL.fuelLevel / 100f;
        float fuelValueBM = sectorBM.fuelLevel / 100f;
        float fuelValueBR = sectorBR.fuelLevel / 100f;


        float growthValueTL = (sectorTL.growthLevel / 250f) - fuelValueTL;
        float growthValueTM = (sectorTM.growthLevel / 250f) - fuelValueTM;
        float growthValueTR = (sectorTR.growthLevel / 250f) - fuelValueTR;

        float growthValueML = (sectorML.growthLevel / 250f) - fuelValueML;
        float growthValueMM = (sectorMM.growthLevel / 250f) - fuelValueMM;
        float growthValueMR = (sectorMR.growthLevel / 250f) - fuelValueMR;
        
        float growthValueBL = (sectorBL.growthLevel / 250f) - fuelValueBL;
        float growthValueBM = (sectorBM.growthLevel / 250f) - fuelValueBM;
        float growthValueBR = (sectorBR.growthLevel / 250f) - fuelValueBR;


        mapSectorTL.color = new Color(fuelValueTL, growthValueTL, 0, 1f);
        mapSectorTM.color = new Color(fuelValueTM, growthValueTM, 0, 1f);
        mapSectorTR.color = new Color(fuelValueTR, growthValueTR, 0, 1f);

        mapSectorML.color = new Color(fuelValueML, growthValueML, 0, 1f);
        mapSectorMM.color = new Color(fuelValueMM, growthValueMM, 0, 1f);
        mapSectorMR.color = new Color(fuelValueMR, growthValueMR, 0, 1f);

        mapSectorBL.color = new Color(fuelValueBL, growthValueBL, 0, 1f);
        mapSectorBM.color = new Color(fuelValueBM, growthValueBM, 0, 1f);
        mapSectorBR.color = new Color(fuelValueBR, growthValueBR, 0, 1f);
    }

    public void beginNextMonth()
    {
        actionPointsCurrent += actionPointsIncreaseRate;

        if (actionPointsCurrent >= actionPointsMax)
        {
            actionPointsCurrent = actionPointsMax;
        }

        month += 1;
        monthsTotal += 1;

        if (month >= 13)
        {
            month = 1;
        }

        updateSeasonMonthNames();

        foreach (Sector sector in sectorList)
        {
            sector.nextMonth();
        }

    }

    public void beginCoolBurn()
    {
        beginEnvironmentalChallenge();
        currentAction = "coolBurn";
        actionPointsCurrent -= coolBurnAPCost;
    }

    public void beginHotBurn()
    {
        beginEnvironmentalChallenge();
        currentAction = "hotBurn";
        actionPointsCurrent -= hotBurnAPCost;
    }

    public void beginExtinguish()
    {
        beginEnvironmentalChallenge();
        currentAction = "extinguish";
        actionPointsCurrent -= extinguishAPCost;
    }

    private void beginEnvironmentalChallenge()
    {
        randomizeChallengePattern();// randomizes current pattern
        challengeEnabled = true;
        challengePhase = 1;
    }

    private void environmentalChallenge()
    {

        if (challengeEnabled == true)
        {
            challengeTimer += Time.deltaTime;



            int iterator = 0;
            foreach (GameObject target in challengeTargetList)
            {
                target.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentPattern[iterator, 0], currentPattern[iterator, 1]);
                iterator += 1;
                target.SetActive(true);
            }

            if (challengePhase == 1)
            {
                challengeButtonList[0].interactable = true;
                challengeButtonList[1].interactable = false;
                challengeButtonList[2].interactable = false;
                challengeButtonList[3].interactable = false;
                challengeButtonList[4].interactable = false;
            }

            if (challengePhase == 2)
            {
                challengeButtonList[0].interactable = false;
                challengeButtonList[1].interactable = true;
                challengeButtonList[2].interactable = false;
                challengeButtonList[3].interactable = false;
                challengeButtonList[4].interactable = false;
            }

            if (challengePhase == 3)
            {
                challengeButtonList[0].interactable = false;
                challengeButtonList[1].interactable = false;
                challengeButtonList[2].interactable = true;
                challengeButtonList[3].interactable = false;
                challengeButtonList[4].interactable = false;
            }

            if (challengePhase == 4)
            {
                challengeButtonList[0].interactable = false;
                challengeButtonList[1].interactable = false;
                challengeButtonList[2].interactable = false;
                challengeButtonList[3].interactable = true;
                challengeButtonList[4].interactable = false;
            }

            if (challengePhase == 5)
            {
                challengeButtonList[0].interactable = false;
                challengeButtonList[1].interactable = false;
                challengeButtonList[2].interactable = false;
                challengeButtonList[3].interactable = false;
                challengeButtonList[4].interactable = true;
            }

            if (currentAction == "coolBurn" && challengePhase > challengeButtonList.Count)
            {
                float Score = challengeTimer / challengeRating;

                player.sectorCurrent.startCoolBurn(Score);
                currentAction = "null";
                challengeEnabled = false;
                challengePhase = 1;

                foreach (GameObject target in challengeTargetList)
                {
                    target.SetActive(false);
                }

                foreach (Button button in challengeButtonList)
                {
                    button.interactable = false;
                }
            }

            if (currentAction == "hotBurn" && challengePhase > challengeButtonList.Count)
            {
                float Score = challengeTimer / challengeRating;

                player.sectorCurrent.startHotBurn(Score);
                currentAction = "null";
                challengeEnabled = false;
                challengePhase = 1;

                foreach (GameObject target in challengeTargetList)
                {
                    target.SetActive(false);
                }

                foreach (Button button in challengeButtonList)
                {
                    button.interactable = false;
                }
            }
            
            
            if (currentAction == "extinguish" && challengePhase > challengeButtonList.Count)
            {
                float Score = challengeTimer / challengeRating;

                player.sectorCurrent.startExtinguish(Score);
                currentAction = "null";
                challengeEnabled = false;
                challengePhase = 1;

                foreach (GameObject target in challengeTargetList)
                {
                    target.SetActive(false);
                }

                foreach (Button button in challengeButtonList)
                {
                    button.interactable = false;
                }
            }
        }
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

    public void nextChallengePhase()
    {
        challengePhase += 1;
    }

    void checkCoolBurnAvailable()
    {
        if (actionPointsCurrent < coolBurnAPCost)
        {
            coolBurnButton.interactable = false;
        }
        if (actionPointsCurrent >= coolBurnAPCost)
        {
            coolBurnButton.interactable = true;
        }
        if (seasonName == "Spring" || seasonName == "Summer")
        {
            coolBurnButton.interactable = false;
        }
    }

    void checkHotBurnAvailable()
    {
        if (actionPointsCurrent < hotBurnAPCost)
        {
            hotBurnButton.interactable = false;
        }
        if (actionPointsCurrent >= hotBurnAPCost)
        {
            hotBurnButton.interactable = true;
        }
        if (seasonName == "Autumn" || seasonName == "Winter")
        {
            hotBurnButton.interactable = false;
        }
    }

    void checkExtinguishAvailable()
    {
        if (actionPointsCurrent < hotBurnAPCost || player.sectorCurrent.wildfire == false)
        {
            extinguishButton.interactable = false;
        }
        if (actionPointsCurrent >= hotBurnAPCost && player.sectorCurrent.wildfire == true)
        {
            extinguishButton.interactable = true;
        }


    }

    void scoreUpdate()
    {
        score = 0;

        foreach (Sector sector in sectorList)
        {
            score += Mathf.RoundToInt(sector.growthLevel * 10f);
        }

        if (score >= scoreHigh)
        {
            scoreHigh = score;
        }

        scoreText.text = "Score: " + score.ToString();
        scoreHighText.text = "High Score: " + scoreHigh.ToString();
    }


    private void updateSeasonMonthNames()
    {
        if (month == 1)
        {
            seasonName = "Summer";
        }
        if (month == 2)
        {
            seasonName = "Summer";
        }
        if (month == 3)
        {
            seasonName = "Autumn";
        }
        if (month == 4)
        {
            seasonName = "Autumn";
        }
        if (month == 5)
        {
            seasonName = "Autumn";
        }
        if (month == 6)
        {
            seasonName = "Winter";
        }
        if (month == 7)
        {
            seasonName = "Winter";
        }
        if (month == 8)
        {
            seasonName = "Winter";
        }
        if (month == 9)
        {
            seasonName = "Spring";
        }
        if (month == 10)
        {
            seasonName = "Spring";
        }
        if (month == 11)
        {
            seasonName = "Spring";
        }
        if (month == 12)
        {
            seasonName = "Summer";
        }

        //update monthName
        if (month == 1)
        {
            monthName = "January";
        }
        if (month == 2)
        {
            monthName = "February";
        }
        if (month == 3)
        {
            monthName = "March";
        }
        if (month == 4)
        {
            monthName = "April";
        }
        if (month == 5)
        {
            monthName = "May";
        }
        if (month == 6)
        {
            monthName = "June";
        }
        if (month == 7)
        {
            monthName = "July";
        }
        if (month == 8)
        {
            monthName = "August";
        }
        if (month == 9)
        {
            monthName = "September";
        }
        if (month == 10)
        {
            monthName = "October";
        }
        if (month == 11)
        {
            monthName = "November";
        }
        if (month == 12)
        {
            monthName = "December";
        }

        monthText.text = "Month: " + monthName;
        seasonText.text = "Season: " + seasonName;
    }

    void OnEnable()
    {
        controls.GamePlay.Enable();
    } 

    void OnDisable()
    {
        controls.GamePlay.Disable();
    } 
    
}
