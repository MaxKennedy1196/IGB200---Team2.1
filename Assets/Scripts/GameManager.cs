using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
    Controls controls;
    public List<Sector> sectorList = new List<Sector>();//List of all sectors in the game
    [SerializeField] public GameObject[] Seasons = new GameObject[4];

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

    public TextMeshProUGUI coolBurnButtonText;

    public bool coolBurnInteractableOverride;


    [Header("Hot Burn Variables")]
    public Button hotBurnButton;
    public GameObject hotBurnButtonGameObject;

    public int hotBurnAPCost;

    public TextMeshProUGUI hotBurnButtonText;

    public bool hotBurnInteractableOverride;

    [Header("Planning Variables")]
    public Button planningButton;
    public GameObject planningButtonGameObject;
    public TextMeshProUGUI plannedTurnsText;
    public GameObject plannedTurnsTextGameObject;

    public int planningDuration;

    public int planningAPDiscount;

    public int planningAPCost;

    public TextMeshProUGUI planningButtonText;

    public bool planningInteractableOverride;


    [Header("Extinguish Variables")]
    public Button extinguishButton;
    public GameObject extinguishButtonGameObject;

    public int extinguishAPCost;

    public TextMeshProUGUI extinguishButtonText;

    public bool extinguishInteractableOverride;

    [Header("Awareness Campaign Variables")]

    public int awarenessAPCost;
    public int awarenessAPGain;
    public Button awarenessButton;
    public TextMeshProUGUI awarenessLevelText;
    public bool awarenessRaised;

    public bool awarenessInteractableOverride;

    [Header("UI Score & Token Animation Variables")]

    [SerializeField] GameObject scoreChangePrefab;
    [SerializeField] Transform scoreParent;
    [SerializeField] RectTransform scoreEndPoint;

    [SerializeField] GameObject tokenChangePrefab;
    [SerializeField] Transform tokenParent;
    [SerializeField] RectTransform tokenEndPoint;


    [Header("Action Point Variables")]
    public int actionPointsMax;
    public int actionPointsIncreaseRate;
    public int actionPointsCurrent;

    public TextMeshProUGUI actionPointsRemainingText;
    

    [Header("Scoring Variables")]
    public float score;

    public TextMeshProUGUI scoreText;

    public float planningScoreMuliplier;
    public float scoreMultiplier;

    [Header("Month + Season Variables")]
    
    public string monthName = "";// Displays the name of the current month Updated by updateSeasonMonthNames()
    public string seasonName = "";// Displays the name of the current Season Updated by updateSeasonMonthNames()

    public TextMeshProUGUI monthText;
    public TextMeshProUGUI seasonText;

    public int month;//Current Month Number 
    int monthsTotal = 1; //Total months Ellapsed
    [HideInInspector] public int timeProgressionRate;

    public bool nextMonthInteractableOverride;
    public Button nextMonthButton;

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


    public GameObject playerIconTL;
    public GameObject playerIconTM;
    public GameObject playerIconTR;

    public GameObject playerIconML;
    public GameObject playerIconMM;
    public GameObject playerIconMR;

    public GameObject playerIconBL;
    public GameObject playerIconBM;
    public GameObject playerIconBR;

    public GameObject planningIconTL;
    public GameObject planningIconTM;
    public GameObject planningIconTR;

    public GameObject planningIconML;
    public GameObject planningIconMM;
    public GameObject planningIconMR;

    public GameObject planningIconBL;
    public GameObject planningIconBM;
    public GameObject planningIconBR;

    public bool rangerBookOpen;
    public bool communityCentreOpen;

    [Header("Community Centre Variables")]

    public CommunityCentre centre;

    public GameObject communityCentreMenu;
    public GameObject communityCentreButtonGameObject;

    

    [Header("Tree List")]

    public List<EnvironmentalTreeVisuals> treeList = new List<EnvironmentalTreeVisuals>();//List of all trees in the game

    [Header("UI Elements")]

    // Attention lord Xavier, I am adding this variable to hide UI when needed
    public bool UIActive;

    Color incineratedColor = new Color(0f, 0f, 0f, 1f);
    Color hotBurnedColor = new Color(0f, 0.1f, 0f, 1f);
    Color coolBurnedColor = new Color(0f, 0.3f, 0f, 1f);
    Color healthyColor = new Color(0.2f, 0.8f, 0.2f, 1f);
    Color dryColor = new Color(1f, 1f, 0.2f, 1f);
    Color verDryColor = new Color(0.8f, 0.4f, 0f, 1f);
    Color wildFireColor = new Color(1f, 0f, 0f, 1f);

    public GameObject MinigameInstruction;

    public RectTransform seasonArrow; 

    [Header("Scoring based on Stuatus")]

    public int pointsHotBurned;
    public int pointsCoolBurned;
    public int pointsHealthy;
    public int pointsDry;
    public int pointsVerDry;

    [Header("Hat Unlocks")]
    public bool unlockedAkubra;
    public bool unlockedBillyHat;
    public bool unlockedBinChickenHat;
    public bool unlockedBucketHat;
    public bool unlockedCorkHat;
    public bool unlockedDefault;
    public bool unlockedHelmet;
    public bool unlockedNoHat;

    public Button buttonAkubra;
    public Button buttonBillyHat;
    public Button buttonBinChickenHat;
    public Button buttonBucketHat;
    public Button buttonCorkHat;
    public Button buttonDefault;
    public Button buttonHelmet;
    public Button buttonNoHat;

    public Image checkIconAkubra;
    public Image checkIconBillyHat;
    public Image checkIconBinChickenHat;
    public Image checkIconBucketHat;
    public Image checkIconCorkHat;
    public Image checkIconDefault;
    public Image checkIconHelmet;
    public Image checkIconNoHat;

    public Image lockIconAkubra;
    public Image lockIconBillyHat;
    public Image lockIconBinChickenHat;
    public Image lockIconBucketHat;
    public Image lockIconCorkHat;
    public Image lockIconDefault;
    public Image lockIconHelmet;
    public Image lockIconNoHat;


    [Header("End of Year Message")]
    public GameObject endYearGameObj;

    public Image endYearHat;

    public TextMeshProUGUI unlockDescription;

    public TextMeshProUGUI endOfYearScore;

    public Sprite SpriteAkubra;
    public Sprite SpriteBillyHat;
    public Sprite SpriteBinChickenHat;
    public Sprite SpriteBucketHat;
    public Sprite SpriteCorkHat;
    public Sprite SpriteHelmet;
    public Sprite SpriteNoHat;

    private string unlockTextAkubra = "YOU'VE UNLOCKED AKUBRA";
    private string unlockTextBillyHat = "YOU'VE UNLOCKED BILLY'S HAT";
    private string unlockTextBinChickenHat = "YOU'VE UNLOCKED BIN CHICKEN HAT";
    private string unlockTextBucketHat = "YOU'VE UNLOCKED BUCKET HAT";
    private string unlockTextCorkHat = "YOU'VE UNLOCKED CORK HAT";
    private string unlockTextHelmet = "YOU'VE UNLOCKED ANTI-MAGPIE HELMET";
    private string unlockTextNoHat = "YOU'VE UNLOCKED NO HAT";
    





    void Awake()
    {
        controls = new Controls();

        controls.GamePlay.RangerBook.performed += ctx => rangerBookMenuCheck();
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeProgressionRate = 1;
        updateSeasonMonthNames();

        //actionPointsCurrent = actionPointsMax;
    }

    

    void Update()
    {
        checkNextMonthAvailable();
        checkCoolBurnAvailable();//check if this action can be performed
        checkHotBurnAvailable();//check if this action can be performed
        checkExtinguishAvailable();//check if this action can be performed
        checkPlanningAvailable();
        checkAwarenessAvailable();//check whether the player can purchase an awareness campaign
        checkCommunityCentreAvailable();//check whether the player is in range of the community centre

        updateMiniMapColours();//updates the minimaps fire rating
        updatePlayerMiniMapPosition();
        updatePlanningIconMiniMap();

        updateScoreText();
        updatePlannedTurnsText();
        updateActionButtonText();
        updateMonthArrowPosition();

        updateMinigameInstructions();

        updateLockedHats();
        updateHatActiveUI();

        actionPointsRemainingText.text = "" + actionPointsCurrent;
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
            playerIconTL.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != 1)
        {
            playerIconTL.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == 1)
        {
            playerIconTM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != 1)
        {
            playerIconTM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == 1)
        {
            playerIconTR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != 1)
        {
            playerIconTR.SetActive(false);
        }



        if (player.sectorCurrent.xPos == -1 && player.sectorCurrent.yPos == 0)
        {
            playerIconML.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != 0)
        {
            playerIconML.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == 0)
        {
            playerIconMM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != 0)
        {
            playerIconMM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == 0)
        {
            playerIconMR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != 0)
        {
            playerIconMR.SetActive(false);
        }



        if (player.sectorCurrent.xPos == -1 && player.sectorCurrent.yPos == -1)
        {
            playerIconBL.SetActive(true);
        }
        if (player.sectorCurrent.xPos != -1 || player.sectorCurrent.yPos != -1)
        {
            playerIconBL.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 0 && player.sectorCurrent.yPos == -1)
        {
            playerIconBM.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 0 || player.sectorCurrent.yPos != -1)
        {
            playerIconBM.SetActive(false);
        }

        if (player.sectorCurrent.xPos == 1 && player.sectorCurrent.yPos == -1)
        {
            playerIconBR.SetActive(true);
        }
        if (player.sectorCurrent.xPos != 1 || player.sectorCurrent.yPos != -1)
        {
            playerIconBR.SetActive(false);
        }

    }

    private void updatePlanningIconMiniMap()
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

        if (sectorTL.plannedTurns != 0)
        {
            planningIconTL.SetActive(true);
        }
        if (sectorTL.plannedTurns == 0)
        {
            planningIconTL.SetActive(false);
        }

        if (sectorTM.plannedTurns != 0)
        {
            planningIconTM.SetActive(true);
        }
        if (sectorTM.plannedTurns == 0)
        {
            planningIconTM.SetActive(false);
        }

        if (sectorTR.plannedTurns != 0)
        {
            planningIconTR.SetActive(true);
        }
        if (sectorTR.plannedTurns == 0)
        {
            planningIconTR.SetActive(false);
        }



        if (sectorML.plannedTurns != 0)
        {
            planningIconML.SetActive(true);
        }
        if (sectorML.plannedTurns == 0)
        {
            planningIconML.SetActive(false);
        }

        if (sectorMM.plannedTurns != 0)
        {
            planningIconMM.SetActive(true);
        }
        if (sectorMM.plannedTurns == 0)
        {
            planningIconMM.SetActive(false);
        }

        if (sectorMR.plannedTurns != 0)
        {
            planningIconMR.SetActive(true);
        }
        if (sectorMR.plannedTurns == 0)
        {
            planningIconMR.SetActive(false);
        }
        


        if (sectorBL.plannedTurns != 0)
        {
            planningIconBL.SetActive(true);
        }
        if (sectorBL.plannedTurns == 0)
        {
            planningIconBL.SetActive(false);
        }

        if (sectorBM.plannedTurns != 0)
        {
            planningIconBM.SetActive(true);
        }
        if (sectorBM.plannedTurns == 0)
        {
            planningIconBM.SetActive(false);
        }

        if (sectorBR.plannedTurns != 0)
        {
            planningIconBR.SetActive(true);
        }
        if (sectorBR.plannedTurns == 0)
        {
            planningIconBR.SetActive(false);
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
        if(month == 2)
        {
            endOfYearMessage();
        }

        foreach (EnvironmentalTreeVisuals tree in treeList)
        {
            tree.nextTurn();
        }


        actionPointsCurrent += actionPointsIncreaseRate;

        if (awarenessRaised)
        {
            actionPointsCurrent += awarenessAPGain;
            awarenessRaised = false;
        }
            

        if (actionPointsCurrent >= actionPointsMax)
        {
            actionPointsCurrent = actionPointsMax;
        }

        month += timeProgressionRate;
        monthsTotal += 1;

        if (month >= 13)
        {
            month = 1;
        }


        // note to fix currently plays the month before


        float lastmonth = month;
/*
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
        }*/

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

    public void rangerBookMenuCheck()
    {
        if (rangerBookOpen == false)
        {
            RangerBook.SetActive(true);
            player.lockPlayer();
            rangerBookOpen = true;
            return;
        }
        if (rangerBookOpen == true)
        {
            RangerBook.SetActive(false);
            player.unlockPlayer();
            rangerBookOpen = false;
            return;
        }
        
    }

    public void CommunityCentreCheck()
    {
        if (communityCentreOpen == false)
        {
            communityCentreMenu.SetActive(true);
            player.lockPlayer();
            communityCentreOpen = true;
            return;
        }
        if (communityCentreOpen == true)
        {
            communityCentreMenu.SetActive(false);
            player.unlockPlayer();
            communityCentreOpen = false;
            return;
        }
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

            if (player.sectorCurrent.plannedTurns == 0)
            {
                if (actionPointsCurrent < coolBurnAPCost)
                {
                    coolBurnButton.interactable = false;
                }
                if (actionPointsCurrent >= coolBurnAPCost)
                {
                    coolBurnButton.interactable = true;
                }
            }

            if (player.sectorCurrent.plannedTurns != 0)
            {
                if (actionPointsCurrent < coolBurnAPCost - planningAPDiscount)
                {
                    coolBurnButton.interactable = false;
                }
                if (actionPointsCurrent >= coolBurnAPCost - planningAPDiscount)
                {
                    coolBurnButton.interactable = true;
                }
            }

        }

        if (player.sectorCurrent.plannedTurns == planningDuration || player.sectorCurrent.challengeEnabled == true)
        {
            coolBurnButton.interactable = false;
        }

        if (coolBurnInteractableOverride == false)
        {
            coolBurnButton.interactable = false;
        }

        if (player.sectorCurrent.currentStatus == Sector.Status.coolBurn ||
            player.sectorCurrent.currentStatus == Sector.Status.hotBurn ||
            player.sectorCurrent.currentStatus == Sector.Status.incinerated ||
            player.sectorCurrent.currentStatus == Sector.Status.healthy)
        {
            coolBurnButtonGameObject.SetActive(false);
        }

        if(player.sectorCurrent.wildfire == true)
        {
            coolBurnButtonGameObject.SetActive(false);
        }

    }

    void checkHotBurnAvailable()
    {
        if (seasonName == "Spring" || seasonName == "Summer")
        {
            hotBurnButtonGameObject.SetActive(true);

            if (player.sectorCurrent.plannedTurns == 0)
            {
                if (actionPointsCurrent < hotBurnAPCost)
                {
                    hotBurnButton.interactable = false;
                }
                if (actionPointsCurrent >= hotBurnAPCost)
                {
                    hotBurnButton.interactable = true;
                }
            }

            if (player.sectorCurrent.plannedTurns != 0)
            {
                if (actionPointsCurrent < hotBurnAPCost - planningAPDiscount)
                {
                    hotBurnButton.interactable = false;
                }
                if (actionPointsCurrent >= hotBurnAPCost - planningAPDiscount)
                {
                    hotBurnButton.interactable = true;
                }
            }

        }
        if (seasonName == "Autumn" || seasonName == "Winter")
        {
            hotBurnButtonGameObject.SetActive(false);

        }

        if (player.sectorCurrent.plannedTurns == planningDuration || player.sectorCurrent.challengeEnabled == true)
        {
            hotBurnButton.interactable = false;
        }

        if (hotBurnInteractableOverride == false)
        {
            hotBurnButton.interactable = false;
        }

        if (player.sectorCurrent.currentStatus == Sector.Status.coolBurn ||
            player.sectorCurrent.currentStatus == Sector.Status.hotBurn ||
            player.sectorCurrent.currentStatus == Sector.Status.incinerated ||
            player.sectorCurrent.currentStatus == Sector.Status.healthy)
        {
            hotBurnButtonGameObject.SetActive(false);
        }

        if(player.sectorCurrent.wildfire == true)
        {
            hotBurnButtonGameObject.SetActive(false);
        }

    }

    void checkPlanningAvailable()
    {
        if (player.sectorCurrent.wildfire == true)
        {
            planningButtonGameObject.SetActive(false);
        }
        if(player.sectorCurrent.wildfire == false)
        {
            planningButtonGameObject.SetActive(true);
        }

        if(player.sectorCurrent.currentStatus == Sector.Status.healthy ||
        player.sectorCurrent.currentStatus == Sector.Status.coolBurn ||
        player.sectorCurrent.currentStatus == Sector.Status.hotBurn ||
        player.sectorCurrent.currentStatus == Sector.Status.incinerated)
        {
            planningButtonGameObject.SetActive(false);
        }

        if (actionPointsCurrent < planningAPCost)
        {
            planningButton.interactable = false;
        }
        if (actionPointsCurrent >= planningAPCost)
        {
            planningButton.interactable = true;
        }

        

        if (player.sectorCurrent.plannedTurns != 0 ||
            player.sectorCurrent.challengeEnabled == true)
        {
            planningButton.interactable = false;
        }

        if (planningInteractableOverride == false)
        {
            planningButton.interactable = false;
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

        if (extinguishInteractableOverride == false)
        {
            extinguishButton.interactable = false;
        }

        if (player.sectorCurrent.wildfire == true)
        {
            extinguishButtonGameObject.SetActive(true);
        }
        if (player.sectorCurrent.wildfire == false)
        {
            extinguishButtonGameObject.SetActive(false);
        }

        if(player.sectorCurrent.challengeEnabled == true)
        {
            extinguishButton.interactable = false;
        }
    }

    void checkAwarenessAvailable()
    {

        if (actionPointsCurrent >= awarenessAPCost)
        {
            awarenessButton.interactable = true;
        }
        if (actionPointsCurrent < awarenessAPCost || awarenessRaised == true)
        {
            awarenessButton.interactable = false;
        }

        if (awarenessInteractableOverride == false)
        {
            awarenessButton.interactable = false;
        }
    }

    void checkNextMonthAvailable()
    {
        if (nextMonthInteractableOverride == false)
        {
            nextMonthButton.interactable = false;
        }

        if (nextMonthInteractableOverride == true)
        {
            nextMonthButton.interactable = true;
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
        awarenessRaised = true;
        actionPointsCurrent -= awarenessAPCost;
    }

    void updateScoreText()
    {
        int scoreInt = Mathf.RoundToInt(score);
        scoreText.text = "Score: " + scoreInt.ToString();
    }

    void updatePlannedTurnsText()
    {
        if (player.sectorCurrent.plannedTurns != 0)
        {
            plannedTurnsTextGameObject.SetActive(true);
            plannedTurnsText.text = "Burn Planned. " + player.sectorCurrent.plannedTurns.ToString() + " months left to utilise planning." ;
        }
        if (player.sectorCurrent.plannedTurns == 0)
        {
            plannedTurnsTextGameObject.SetActive(false);
        }
            
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

    private void updateActionButtonText()
    {
        if (player.sectorCurrent.plannedTurns == 0)
        {
            coolBurnButtonText.text = "COOL BURN \n COST: " + coolBurnAPCost;
            hotBurnButtonText.text = "HOT BURN \n COST: " + hotBurnAPCost;
        }
        if (player.sectorCurrent.plannedTurns != 0)
        {
            int totalCostCool = coolBurnAPCost - planningAPDiscount;
            coolBurnButtonText.text = "COOL BURN \n COST: " + totalCostCool;

            int totalCostHot = hotBurnAPCost - planningAPDiscount;
            hotBurnButtonText.text = "HOT BURN \n COST: " + totalCostHot;
        }

        planningButtonText.text = "PLAN BURN \n COST: " + planningAPCost;

        extinguishButtonText.text = "EXTINGUISH FIRE \n COST: " + extinguishAPCost;

    }

    private void updateMinigameInstructions()
    {
        if (player.sectorCurrent.challengeEnabled == true)
        {
            MinigameInstruction.SetActive(true);
        }
        if (player.sectorCurrent.challengeEnabled == false)
        {
            MinigameInstruction.SetActive(false);
        }
    }

    private void updateMonthArrowPosition()
    {
        seasonArrow.rotation = Quaternion.Euler(0f, 0f, -15f);   
        switch (month)
        {
            case 1:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -315f); 
                break;
            case 2:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -345f); 
                break;
            case 3:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -15f); 
                break;
            case 4:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -45f); 
                break;
            case 5:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -75f); 
                break;
            case 6:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -105f); 
                break;
            case 7:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -135f); 
                break;
            case 8:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -165f); 
                break;
            case 9:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -195f); 
                break;
            case 10:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -225f); 
                break;
            case 11:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -255f); 
                break;
            case 12:
                seasonArrow.rotation = Quaternion.Euler(0f, 0f, -285f);
                break;
        }
    }

    public void scoreIncrease(float input, Vector2 offset = default)
    {
        ShowScoreChange(Mathf.RoundToInt(input), offset);
        score += input;
        
    }

    private void ShowScoreChange(int change, Vector2 offset = default)
    {
        var inst = Instantiate(scoreChangePrefab, Vector3.zero, Quaternion.identity);
        inst.transform.SetParent(scoreParent, false);

        RectTransform rect = inst.GetComponent<RectTransform>();

        TMP_Text text = inst.GetComponent<TMP_Text>();

        text.text = (change > 0 ? "+" : "") + change.ToString();

        rect.anchoredPosition = scoreEndPoint.anchoredPosition + offset;

        LeanTween.moveY(rect, scoreEndPoint.anchoredPosition.y + 20f, 1.5f).setOnComplete(() => {
            Destroy(inst);
            });

        LeanTween.alphaText(rect, 0.2f, 1.5f);

        Debug.Log("Score Change Shown");
    }

    void updateLockedHats()
    {
        if (unlockedAkubra == false)
        {
            lockIconAkubra.enabled = true;
            buttonAkubra.interactable = false;
        }
        if (unlockedAkubra == true)
        {
            lockIconAkubra.enabled = false;
            buttonAkubra.interactable = true;
        }
        
        if (unlockedBillyHat == false)
        {
            lockIconBillyHat.enabled = true;
            buttonBillyHat.interactable = false;
        }
        if (unlockedBillyHat == true)
        {
            lockIconBillyHat.enabled = false;
            buttonBillyHat.interactable = true;
        }

        if (unlockedBinChickenHat == false)
        {
            lockIconBinChickenHat.enabled = true;
            buttonBinChickenHat.interactable = false;
        }
        if (unlockedBinChickenHat == true)
        {
            lockIconBinChickenHat.enabled = false;
            buttonBinChickenHat.interactable = true;
        }

        if (unlockedBucketHat == false)
        {
            lockIconBucketHat.enabled = true;
            buttonBucketHat.interactable = false;
        }
        if (unlockedBucketHat == true)
        {
            lockIconBucketHat.enabled = false;
            buttonBucketHat.interactable = true;
        }

        if (unlockedCorkHat == false)
        {
            lockIconCorkHat.enabled = true;
            buttonCorkHat.interactable = false;
        }
        if (unlockedCorkHat == true)
        {
            lockIconCorkHat.enabled = false;
            buttonCorkHat.interactable = true;
        }

        if (unlockedDefault == false)
        {
            lockIconDefault.enabled = true;
            buttonDefault.interactable = false;
        }
        if (unlockedDefault == true)
        {
            lockIconDefault.enabled = false;
            buttonDefault.interactable = true;
        }

        if (unlockedHelmet == false)
        {
            lockIconHelmet.enabled = true;
            buttonHelmet.interactable = false;
        }
        if (unlockedHelmet == true)
        {
            lockIconHelmet.enabled = false;
            buttonHelmet.interactable = true;
        }


        if (unlockedNoHat == false)
        {
            lockIconNoHat.enabled = true;
            buttonNoHat.interactable = false;
        }
        if (unlockedNoHat == true)
        {
            lockIconNoHat.enabled = false;
            buttonNoHat.interactable = true;
        }


    }

    private void updateHatActiveUI()
    {
        checkIconAkubra.enabled = false;
        checkIconBillyHat.enabled = false;
        checkIconBinChickenHat.enabled = false;
        checkIconBucketHat.enabled = false;
        checkIconCorkHat.enabled = false;
        checkIconDefault.enabled = false;
        checkIconHelmet.enabled = false;
        checkIconNoHat.enabled = false;

        if (player.currentHat == Player.Hat.Akubra)
        {
            checkIconAkubra.enabled = true;
        }

        if (player.currentHat == Player.Hat.BillyHat)
        {
            checkIconBillyHat.enabled = true;
        }

        if (player.currentHat == Player.Hat.BinChickenHat)
        {
            checkIconBinChickenHat.enabled = true;
        }

        if (player.currentHat == Player.Hat.BucketHat)
        {
            checkIconBucketHat.enabled = true;
        }

        if (player.currentHat == Player.Hat.CorkHat)
        {
            checkIconCorkHat.enabled = true;
        }

        if (player.currentHat == Player.Hat.Default)
        {
            checkIconDefault.enabled = true;
        }

        if (player.currentHat == Player.Hat.Helmet)
        {
            checkIconHelmet.enabled = true;
        }
        
        if(player.currentHat == Player.Hat.NoHat)
        {
            checkIconNoHat.enabled = true;
        }
    }

    public void setHatAkubra()
    {
        player.currentHat = Player.Hat.Akubra;
    }

    public void setHatBillyHat()
    {
        player.currentHat = Player.Hat.BillyHat;
    }

    public void setHatBinChickenHat()
    {
        player.currentHat = Player.Hat.BinChickenHat;
    }

    public void setHatBucketHat()
    {
        player.currentHat = Player.Hat.BucketHat;
    }

    public void setHatCorkHat()
    {
        player.currentHat = Player.Hat.CorkHat;
    }

    public void setHatDefault()
    {
        player.currentHat = Player.Hat.Default;
    }

    public void setHatHelmet()
    {
        player.currentHat = Player.Hat.Helmet;
    }

    public void setHatNoHat()
    {
        player.currentHat = Player.Hat.NoHat;
    }

    private void endOfYearMessage()
    {
        endYearGameObj.SetActive(true);

        randomiseHats();

        endOfYearScore.text = "SCORE: " + score;

        player.lockPlayer();


    }

    void randomiseHats()
    {
        int randomiser = Random.Range(1, 9);

        switch (randomiser)
        {
            case 1:
                if (unlockedAkubra == true)
                {
                    randomiseHats();
                }
                
                unlockedAkubra = true;
                endYearHat.sprite = SpriteAkubra;
                unlockDescription.text = unlockTextAkubra;
                break;
            case 2:
                if (unlockedBillyHat == true)
                {
                    randomiseHats();
                }

                unlockedBillyHat = true;
                endYearHat.sprite = SpriteBillyHat;
                unlockDescription.text = unlockTextBillyHat;
                break;
            case 3:
                if (unlockedBinChickenHat == true)
                {
                    randomiseHats();
                }
                
                unlockedBinChickenHat = true;
                endYearHat.sprite = SpriteBinChickenHat;
                unlockDescription.text = unlockTextBinChickenHat;
                break;
            case 4:
                if (unlockedBucketHat == true)
                {
                    randomiseHats();
                }
                
                unlockedBucketHat = true;
                endYearHat.sprite = SpriteBucketHat;
                unlockDescription.text = unlockTextBucketHat;
                break;
            case 5:
                if (unlockedCorkHat == true)
                {
                    randomiseHats();
                }
                
                unlockedCorkHat = true;
                endYearHat.sprite = SpriteCorkHat;
                unlockDescription.text = unlockTextCorkHat;
                break;
            case 6:
                if (unlockedHelmet == true)
                {
                    randomiseHats();
                }
                
                unlockedHelmet = true;
                endYearHat.sprite = SpriteHelmet;
                unlockDescription.text = unlockTextHelmet;
                break;
            case 7:
                if (unlockedNoHat == true)
                {
                    randomiseHats();
                }
                
                unlockedNoHat = true;
                endYearHat.sprite = SpriteNoHat;
                unlockDescription.text = unlockTextNoHat;
                break;
            
        }
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
