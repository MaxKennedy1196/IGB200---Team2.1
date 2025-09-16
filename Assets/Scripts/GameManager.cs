using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEditor.ShaderKeywordFilter;
using Mono.Cecil.Cil;
using UnityEditor.PackageManager;

public class GameManager : MonoBehaviour
{
    public Player player;
    Controls controls;
    public List<Sector> sectorList = new List<Sector>();//List of all sectors in the game
    [SerializeField]

    public GameObject[] Seasons = new GameObject[4];

    [Header("Status Change Chances")]
    public float healthyChanceFromCoolBurn;
    public float healthyChanceFromHotBurn;
    public float healthyChanceFromIncinerated;
    public float dryChance;
    public float veryDryChance;

    [Header("Wildfire Occuring Chances")]
    public float wildfireChanceSpring;
    public float wildfireChanceSummer;


    [Header("Cool Burn Variables")]
    public Button coolBurnButton;
    public GameObject coolBurnButtonGameObject;

    public int coolBurnAPCost;


    [Header("Hot Burn Variables")]
    public Button hotBurnButton;
    public GameObject hotBurnButtonGameObject;

    public int hotBurnAPCost;


    [Header("Planning Variables")]
    public Button planningButton;

    public int planningDuration;

    public int planningAPDiscount;

    public int planningAPCost;


    [Header("Extinguish Variables")]
    public Button extinguishButton;


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


    public GameObject playerSectorTL;
    public GameObject playerSectorTM;
    public GameObject playerSectorTR;

    public GameObject playerSectorML;
    public GameObject playerSectorMM;
    public GameObject playerSectorMR;

    public GameObject playerSectorBL;
    public GameObject playerSectorBM;
    public GameObject playerSectorBR;

    public bool rangerBookOpen;

    [Header("Community Centre Variables")]

    public CommunityCentre centre;

    public GameObject communityCentreMenu;
    public GameObject communityCentreButtonGameObject;

    [Header("Awareness Campaign Variables")]

    public int awarenessAPCost;
    public Button awarenessButton;
    public TextMeshProUGUI awarenessLevelText;
    int awarenessLevel;

    [Header("Tree List")]

    public List<EnvironmentalTreeVisuals> treeList = new List<EnvironmentalTreeVisuals>();//List of all trees in the game

    [Header("UI Elements")]

    // Attention lord Xavier, I am adding this variable to hide UI when needed
    public bool UIActive;//this pleases lord xavier

    Color incineratedColor = new Color(0f, 0f, 0f, 1f);
    Color hotBurnedColor = new Color(0f, 0.1f, 0f, 1f);
    Color coolBurnedColor = new Color(0f, 0.3f, 0f, 1f);
    Color healthyColor = new Color(0.2f, 0.8f, 0.2f, 1f);
    Color dryColor = new Color(1f, 1f, 0.2f, 1f);
    Color verDryColor = new Color(0.8f, 0.4f, 0f, 1f);
    Color wildFireColor = new Color(1f, 0f, 0f, 1f);

    void Awake()
    {
        controls = new Controls();

        controls.GamePlay.RangerBook.performed += ctx => rangerBookMenuCheck();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateSeasonMonthNames();

        actionPointsCurrent = actionPointsMax;
    }

    public void rangerBookMenuCheck()
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
        checkCoolBurnAvailable();//check if this action can be performed
        checkHotBurnAvailable();//check if this action can be performed
        checkExtinguishAvailable();//check if this action can be performed
        checkPlanningAvailable();
        checkAwarenessAvailable();//check whether the player can purchase an awareness campaign
        checkCommunityCentreAvailable();//check whether the player is in range of the community centre

        updateMiniMapColours();//updates the minimaps fire rating
        updatePlayerMiniMapPosition();

        updateCommunityCentreText();

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

    private void updatePlayerMiniMapPosition()
    {
        if (player.sectorCurrent.xPos == -1 && player.sectorCurrent.yPos == 1)
        {
            playerSectorTL.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != 1)
        {
            playerSectorTL.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == 1)
        {
            playerSectorTM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != 1)
        {
            playerSectorTM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == 1)
        {
            playerSectorTR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != 1)
        {
            playerSectorTR.SetActive(false);
        }



        if (player.sectorCurrent.xPos == -1 && player.sectorCurrent.yPos == 0)
        {
            playerSectorML.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != 0)
        {
            playerSectorML.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == 0)
        {
            playerSectorMM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != 0)
        {
            playerSectorMM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == 0)
        {
            playerSectorMR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != 0)
        {
            playerSectorMR.SetActive(false);
        }



        if (player.sectorCurrent.xPos == -1 && player.sectorCurrent.yPos == -1)
        {
            playerSectorBL.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != -1)
        {
            playerSectorBL.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == -1)
        {
            playerSectorBM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != -1)
        {
            playerSectorBM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == -1)
        {
            playerSectorBR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != -1)
        {
            playerSectorBR.SetActive(false);
        }

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

        Sector.Status statusTL = sectorTL.currentStatus;
        Sector.Status statusTM = sectorTM.currentStatus;
        Sector.Status statusTR = sectorTR.currentStatus;

        Sector.Status statusML = sectorML.currentStatus;
        Sector.Status statusMM = sectorMM.currentStatus;
        Sector.Status statusMR = sectorMR.currentStatus;

        Sector.Status statusBL = sectorBL.currentStatus;
        Sector.Status statusBM = sectorBM.currentStatus;
        Sector.Status statusBR = sectorBR.currentStatus;

        //TL
        if (statusTL == Sector.Status.incinerated)
        {
            mapSectorTL.color = incineratedColor;
        }
        if (statusTL == Sector.Status.hotBurn)
        {
            mapSectorTL.color = hotBurnedColor;
        }
        if (statusTL == Sector.Status.coolBurn)
        {
            mapSectorTL.color = coolBurnedColor;
        }
        if (statusTL == Sector.Status.healthy)
        {
            mapSectorTL.color = healthyColor;
        }
        if (statusTL == Sector.Status.dry)
        {
            mapSectorTL.color = dryColor;
        }
        if (statusTL == Sector.Status.veryDry)
        {
            mapSectorTL.color = verDryColor;
        }
        if (sectorTL.wildfire == true)
        {
            mapSectorTL.color = wildFireColor;
        }

        //TM
        if (statusTM == Sector.Status.incinerated)
        {
            mapSectorTM.color = incineratedColor;
        }
        if (statusTM == Sector.Status.hotBurn)
        {
            mapSectorTM.color = hotBurnedColor;
        }
        if (statusTM == Sector.Status.coolBurn)
        {
            mapSectorTM.color = coolBurnedColor;
        }
        if (statusTM == Sector.Status.healthy)
        {
            mapSectorTM.color = healthyColor;
        }
        if (statusTM == Sector.Status.dry)
        {
            mapSectorTM.color = dryColor;
        }
        if (statusTM == Sector.Status.veryDry)
        {
            mapSectorTM.color = verDryColor;
        }
        if (sectorTM.wildfire == true)
        {
            mapSectorTM.color = wildFireColor;
        }

        //TR
        if (statusTR == Sector.Status.incinerated)
        {
            mapSectorTR.color = incineratedColor;
        }
        if (statusTR == Sector.Status.hotBurn)
        {
            mapSectorTR.color = hotBurnedColor;
        }
        if (statusTR == Sector.Status.coolBurn)
        {
            mapSectorTR.color = coolBurnedColor;
        }
        if (statusTR == Sector.Status.healthy)
        {
            mapSectorTR.color = healthyColor;
        }
        if (statusTR == Sector.Status.dry)
        {
            mapSectorTR.color = dryColor;
        }
        if (statusTR == Sector.Status.veryDry)
        {
            mapSectorTR.color = verDryColor;
        }
        if (sectorTR.wildfire == true)
        {
            mapSectorTR.color = wildFireColor;
        }

        //ML
        if (statusML == Sector.Status.incinerated)
        {
            mapSectorML.color = incineratedColor;
        }
        if (statusML == Sector.Status.hotBurn)
        {
            mapSectorML.color = hotBurnedColor;
        }
        if (statusML == Sector.Status.coolBurn)
        {
            mapSectorML.color = coolBurnedColor;
        }
        if (statusML == Sector.Status.healthy)
        {
            mapSectorML.color = healthyColor;
        }
        if (statusML == Sector.Status.dry)
        {
            mapSectorML.color = dryColor;
        }
        if (statusML == Sector.Status.veryDry)
        {
            mapSectorML.color = verDryColor;
        }
        if (sectorML.wildfire == true)
        {
            mapSectorML.color = wildFireColor;
        }

        //MM
        if (statusMM == Sector.Status.incinerated)
        {
            mapSectorMM.color = incineratedColor;
        }
        if (statusMM == Sector.Status.hotBurn)
        {
            mapSectorMM.color = hotBurnedColor;
        }
        if (statusMM == Sector.Status.coolBurn)
        {
            mapSectorMM.color = coolBurnedColor;
        }
        if (statusMM == Sector.Status.healthy)
        {
            mapSectorMM.color = healthyColor;
        }
        if (statusMM == Sector.Status.dry)
        {
            mapSectorMM.color = dryColor;
        }
        if (statusMM == Sector.Status.veryDry)
        {
            mapSectorMM.color = verDryColor;
        }
        if (sectorMM.wildfire == true)
        {
            mapSectorMM.color = wildFireColor;
        }

        //MR
        if (statusMR == Sector.Status.incinerated)
        {
            mapSectorMR.color = incineratedColor;
        }
        if (statusMR == Sector.Status.hotBurn)
        {
            mapSectorMR.color = hotBurnedColor;
        }
        if (statusMR == Sector.Status.coolBurn)
        {
            mapSectorMR.color = coolBurnedColor;
        }
        if (statusMR == Sector.Status.healthy)
        {
            mapSectorMR.color = healthyColor;
        }
        if (statusMR == Sector.Status.dry)
        {
            mapSectorMR.color = dryColor;
        }
        if (statusMR == Sector.Status.veryDry)
        {
            mapSectorMR.color = verDryColor;
        }
        if (sectorMR.wildfire == true)
        {
            mapSectorMR.color = wildFireColor;
        }

        //BL
        if (statusBL == Sector.Status.incinerated)
        {
            mapSectorBL.color = incineratedColor;
        }
        if (statusBL == Sector.Status.hotBurn)
        {
            mapSectorBL.color = hotBurnedColor;
        }
        if (statusBL == Sector.Status.coolBurn)
        {
            mapSectorBL.color = coolBurnedColor;
        }
        if (statusBL == Sector.Status.healthy)
        {
            mapSectorBL.color = healthyColor;
        }
        if (statusBL == Sector.Status.dry)
        {
            mapSectorBL.color = dryColor;
        }
        if (statusBL == Sector.Status.veryDry)
        {
            mapSectorBL.color = verDryColor;
        }
        if (sectorBL.wildfire == true)
        {
            mapSectorBL.color = wildFireColor;
        }

        //BM
        if (statusBM == Sector.Status.incinerated)
        {
            mapSectorBM.color = incineratedColor;
        }
        if (statusBM == Sector.Status.hotBurn)
        {
            mapSectorBM.color = hotBurnedColor;
        }
        if (statusBM == Sector.Status.coolBurn)
        {
            mapSectorBM.color = coolBurnedColor;
        }
        if (statusBM == Sector.Status.healthy)
        {
            mapSectorBM.color = healthyColor;
        }
        if (statusBM == Sector.Status.dry)
        {
            mapSectorBM.color = dryColor;
        }
        if (statusBM == Sector.Status.veryDry)
        {
            mapSectorBM.color = verDryColor;
        }
        if (sectorBM.wildfire == true)
        {
            mapSectorBM.color = wildFireColor;
        }

        //BR
        if (statusBR == Sector.Status.incinerated)
        {
            mapSectorBR.color = incineratedColor;
        }
        if (statusBR == Sector.Status.hotBurn)
        {
            mapSectorBR.color = hotBurnedColor;
        }
        if (statusBR == Sector.Status.coolBurn)
        {
            mapSectorBR.color = coolBurnedColor;
        }
        if (statusBR == Sector.Status.healthy)
        {
            mapSectorBR.color = healthyColor;
        }
        if (statusBR == Sector.Status.dry)
        {
            mapSectorBR.color = dryColor;
        }
        if (statusBR == Sector.Status.veryDry)
        {
            mapSectorBR.color = verDryColor;
        }
        if (sectorBR.wildfire == true)
        {
            mapSectorBR.color = wildFireColor;
        }
        

    }

    public void beginNextMonth()
    {
        scoreUpdate();

        foreach (EnvironmentalTreeVisuals tree in treeList)
        {
            tree.nextTurn();
        }

        actionPointsCurrent += actionPointsIncreaseRate;

        actionPointsCurrent += 25 * awarenessLevel;
        awarenessLevel = 0;

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


        // note to fix currently plays the month before


        float lastmonth = month;

        if (lastmonth == 3)
        {
            StartCoroutine(Seasons[1].GetComponent<FadeUI>().FadeInAndOut(Seasons[1].gameObject));
        }

        else if (lastmonth == 6)
        {
            StartCoroutine(Seasons[2].GetComponent<FadeUI>().FadeInAndOut(Seasons[2].gameObject));
        }

        else if (lastmonth == 9)
        {
            StartCoroutine(Seasons[3].GetComponent<FadeUI>().FadeInAndOut(Seasons[3].gameObject));
        }

        else if (lastmonth == 12)
        {
            StartCoroutine(Seasons[0].GetComponent<FadeUI>().FadeInAndOut(Seasons[0].gameObject));
        }

        updateSeasonMonthNames();

        
        foreach (Sector sector in sectorList)
        {
            sector.nextMonth();
        }


    }

    public void beginCoolBurn()
    {
        player.sectorCurrent.startCoolBurn();
        if (player.sectorCurrent.plannedTurns != 0)
        {
            actionPointsCurrent -= (coolBurnAPCost - planningAPDiscount);

        }
        if (player.sectorCurrent.plannedTurns == 0)
        {
            actionPointsCurrent -= coolBurnAPCost;
        }
            
    }

    public void beginHotBurn()
    {
        player.sectorCurrent.startHotBurn();
        if (player.sectorCurrent.plannedTurns != 0)
        {
            actionPointsCurrent -= (hotBurnAPCost - planningAPDiscount);
            
        }
        if (player.sectorCurrent.plannedTurns == 0)
        {
            actionPointsCurrent -= hotBurnAPCost;
        }
    }

    public void beginPlanning()
    {
        player.sectorCurrent.startPlanning();
        actionPointsCurrent -= planningAPCost;
    }

    public void beginExtinguish()
    {
        player.sectorCurrent.startExtinguish();
        actionPointsCurrent -= extinguishAPCost;
    }


    void checkCoolBurnAvailable()
    {
        
        if (seasonName == "Spring" || seasonName == "Summer")
        {
            coolBurnButtonGameObject.SetActive(false);
        }
        if (seasonName == "Autumn" || seasonName == "Winter")
        {
            coolBurnButtonGameObject.SetActive(true);
            if (actionPointsCurrent < coolBurnAPCost)
            {
                coolBurnButton.interactable = false;
            }
            if (actionPointsCurrent >= coolBurnAPCost)
            {
                coolBurnButton.interactable = true;
            }
            if (player.sectorCurrent.currentStatus == Sector.Status.coolBurn || player.sectorCurrent.currentStatus == Sector.Status.hotBurn || player.sectorCurrent.currentStatus == Sector.Status.incinerated)
            {
                coolBurnButton.interactable = false;
            }
        }
    }

    void checkHotBurnAvailable()
    {



        if (seasonName == "Spring" || seasonName == "Summer")
        {
            hotBurnButtonGameObject.SetActive(true);
            if (actionPointsCurrent < hotBurnAPCost)
            {
                hotBurnButton.interactable = false;
            }
            if (actionPointsCurrent >= hotBurnAPCost)
            {
                hotBurnButton.interactable = true;
            }
            if (player.sectorCurrent.currentStatus == Sector.Status.coolBurn || player.sectorCurrent.currentStatus == Sector.Status.hotBurn || player.sectorCurrent.currentStatus == Sector.Status.incinerated)
            {
                hotBurnButton.interactable = false;
            }
        }
        if (seasonName == "Autumn" || seasonName == "Winter")
        {
            hotBurnButtonGameObject.SetActive(false);

        }
    }

    void checkPlanningAvailable()
    {
        if (actionPointsCurrent < planningAPCost)
        {
            planningButton.interactable = false;
        }
        if (actionPointsCurrent >= planningAPCost)
        {
            planningButton.interactable = true;
        }
    }

    void checkExtinguishAvailable()
    {
        if (actionPointsCurrent < extinguishAPCost || player.sectorCurrent.wildfire == false)
        {
            extinguishButton.interactable = false;
        }
        if (actionPointsCurrent >= extinguishAPCost && player.sectorCurrent.wildfire == true)
        {
            extinguishButton.interactable = true;
        }
    }

    void checkAwarenessAvailable()
    {
        if (actionPointsCurrent < awarenessAPCost)
        {
            awarenessButton.interactable = false;
        }
        if (actionPointsCurrent >= awarenessAPCost)
        {
            awarenessButton.interactable = true;
        }
    }

    void checkCommunityCentreAvailable()
    {
        if (centre.playerInRange == true)
        {
            communityCentreButtonGameObject.SetActive(true);
        }
        if (centre.playerInRange == false)
        {
            communityCentreButtonGameObject.SetActive(false);
            closeCommunityCentre();
        }
    }

    public void openCommunityCentre()
    {
        communityCentreMenu.SetActive(true);
        return;
    }

    public void closeCommunityCentre()
    {
        communityCentreMenu.SetActive(false);
        return;
    }

    public void raiseAwareness()
    {
        awarenessLevel += 1;
        actionPointsCurrent -= awarenessAPCost;
    }

    void updateCommunityCentreText()
    {
        awarenessLevelText.text = "Level: " + awarenessLevel.ToString();
    }


    void scoreUpdate()
    {
        scoreText.text = "Score: " + score.ToString();
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
