using UnityEngine;

public class Sector : MonoBehaviour
{
    //Script for describing a sector and all related functions 
    public GameManager Manager;

    public int xPos;//x Position of this sector
    public int yPos;//y Position of this sector

    public float fuelLevel;//Current Fuel Level
    public float growthLevel;//Current Growth Level


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        Manager.sectorList.Add(gameObject.GetComponent<Sector>());//Add this sector to SectorList in the GameManager 
    }

    public void nextSeason()
    {
        print("nextSeason");
        fuelLevel += Random.Range(Manager.fuelIncreaseRateMin, Manager.fuelIncreaseRateMax);
        growthLevel += Random.Range(Manager.growthIncreaseRateMin, Manager.growthIncreaseRateMax);

        print("fuelLevel:" + fuelLevel + " growthLevel:" + growthLevel);
    }
    

}
