using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    [Header("Action Point Variables")]
    public int ActionPointsMax;
    public int ActionPointsCurrent;

    [Header("Season Info")]
    int season = 1;//Current season Number 
    //1 = Autumn
    //2 = Winter
    //3 = Spring
    //4 = Summer

    int seasonsTotal = 1;//Total Seasons Ellapsed
    public string seasonName = "";// Displays the name of the current season Updated by updateSeasonName(), 

    public TextMeshProUGUI seasonText;

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

    public void beginNextSeason()
    {
        season += 1;
        seasonsTotal += 1;

        if (season >= 5)
        {
            season = 1;
        }

        updateSeasonName();

        foreach (Sector sector in sectorList)
        {
            sector.nextSeason();
        }


    }

    private void updateSeasonName()
    {
        if (season == 1)
        {
            seasonName = "Autumn";
        }
        if (season == 2)
        {
            seasonName = "Winter";
        }
        if (season == 3)
        {
            seasonName = "Spring";
        }
        if (season == 4)
        {
            seasonName = "Summer";
        }

        seasonText.text = "Season: " + seasonName;
    }

    
}
