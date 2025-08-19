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

    [Header("Action Point Variables")]
    public int ActionPointsMax;
    public int ActionPointsCurrent;

    [Header("Season Info")]
    
    int month = 3;//Current month Number 
    //1 = Autumn
    //2 = Winter
    //3 = Spring
    //4 = Summer

    int  monthsTotal = 1;//Total Seasons Ellapsed
    public string monthName = "";
    public string seasonName = "";// Displays the name of the current month Updated by updateSeasonName(), 

    public TextMeshProUGUI seasonText;
    public TextMeshProUGUI monthText;

    [Header("Scoring Variables")]

    public int score;
    public int scoreHigh;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreHighText;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        updateSeasonName();
    }

    void Update()
    {
        scoreUpdate();

        if (ActionPointsCurrent < coolBurnAPCost)
        {
            coolBurnButton.interactable = false;
        }
        if (ActionPointsCurrent > coolBurnAPCost)
        {
            coolBurnButton.interactable = true;
        }
        if (seasonName == "Spring" || seasonName == "Summer")
        {
            coolBurnButton.interactable = false;
        }

    }

    public void beginNextMonth()
    {
        ActionPointsCurrent = ActionPointsMax;

        month += 1;
        monthsTotal += 1;

        if (month >= 13)
        {
            month = 1;
        }

        updateSeasonName();

        foreach (Sector sector in sectorList)
        {
            sector.nextMonth();
        }

    }

    public void beginCoolBurn()
    {
        player.sectorCurrent.startCoolBurn();
        ActionPointsCurrent -= coolBurnAPCost;
        
        
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
        scoreHighText.text = "Score: " + scoreHigh.ToString();
    }



    private void updateSeasonName()
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
