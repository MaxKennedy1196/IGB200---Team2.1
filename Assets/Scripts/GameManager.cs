using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Player player;
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
    int  monthsTotal = 1; //Total months Ellapsed


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateSeasonMonthNames();
    }

    void Update()
    {
        //print(hotBurnAPCost);

        scoreUpdate();

        checkCoolBurnAvailable();
        checkHotBurnAvailable();
        checkExtinguishAvailable();

        actionPointsRemainingText.text = "Action Points Remaining: " + actionPointsCurrent;

    }

    public void beginNextMonth()
    {
        actionPointsCurrent = actionPointsMax;

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
        player.sectorCurrent.startCoolBurn();
        actionPointsCurrent -= coolBurnAPCost;
    }

    public void beginHotBurn()
    {
        player.sectorCurrent.startHotBurn();
        actionPointsCurrent -= hotBurnAPCost;
    }

    public void beginExtinguish()
    {
        player.sectorCurrent.startExtinguish();
        actionPointsCurrent -= extinguishAPCost;
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

    
}
