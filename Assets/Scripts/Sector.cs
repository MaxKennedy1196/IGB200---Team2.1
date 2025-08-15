using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Sector : MonoBehaviour
{
    //Script for describing a sector and all related functions 
    public GameManager Manager;

    public int xPos;//x Position of this sector
    public int yPos;//y Position of this sector

    public float fuelLevel;//Current Fuel Level
    public float growthLevel;//Current Growth Level

    public TextMeshProUGUI fuelText;
    public TextMeshProUGUI growthText;

    public Image fireImage;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        Manager.sectorList.Add(gameObject.GetComponent<Sector>());//Add this sector to SectorList in the GameManager 

        fuelLevel += Random.Range(Manager.fuelSpawnMin, Manager.fuelSpawnMax);
        growthLevel += Random.Range(Manager.growthSpawnMin, Manager.growthSpawnMax);

        sectorInit();
    }

    void sectorInit()
    {
        fuelText.text = "Fuel:" + fuelLevel;
        growthText.text = "Growth:" + growthLevel;

        fireImage.enabled = false;
    }

    public void nextSeason()
    {
        fireImage.enabled = false;

        fuelLevel += Random.Range(Manager.fuelIncreaseRateMin, Manager.fuelIncreaseRateMax);
        growthLevel += Random.Range(Manager.growthIncreaseRateMin, Manager.growthIncreaseRateMax);

        if (fuelLevel >= 100f)
        {
            fuelLevel = 100f;
        }

        if (growthLevel >= 250f)
        {
            growthLevel = 250f;
        }

        fuelText.text = "Fuel:" + fuelLevel;
        growthText.text = "Growth:" + growthLevel;

        if (Manager.seasonName == "Spring")
        {
            float wildfireChance = Random.Range(60f, 100f);

            if (wildfireChance <= fuelLevel)
            {
                startWildFire();
            }
        }
        if (Manager.seasonName == "Summer")
        {
            float wildfireChance = Random.Range(40f, 100f);

            if (wildfireChance <= fuelLevel)
            {
                startWildFire();
            }
        }

    }

    void startWildFire()
    {
        fireImage.enabled = true;
        fuelLevel = 0f;
        growthLevel = 0f;

        print("WILDFIRE!!!");

    }


}
