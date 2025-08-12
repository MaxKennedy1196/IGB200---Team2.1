using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<Sector> sectorList = new List<Sector>();//List of all sectors in the game

    public float fuelIncreaseRateMin;
    public float fuelIncreaseRateMax;

    public float growthIncreaseRateMin;
    public float growthIncreaseRateMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beginNextSeason();
    }

    public void beginNextSeason()
    {
        foreach (Sector sector in sectorList)
        {
            sector.nextSeason();
        }
    }

    
}
