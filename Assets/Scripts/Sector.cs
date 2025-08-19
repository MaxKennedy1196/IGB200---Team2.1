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

    void Update()
    {
        fuelText.text = "Fuel:" + fuelLevel;
        growthText.text = "Growth:" + growthLevel;

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
    }

    void sectorInit()
    {
        fuelText.text = "Fuel:" + fuelLevel;
        growthText.text = "Growth:" + growthLevel;

        fireImage.enabled = false;
    }

    public void nextMonth()
    {
        fireImage.enabled = false;

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

    }

    private void startWildFire()
    {
        fireImage.enabled = true;
        fuelLevel = 0f;
        growthLevel = 0f;

        print("WILDFIRE!!!");

    }

    public void startCoolBurn()
    {
        fuelLevel -= Random.Range(Manager.coolBurnFuelDecreaseMin, Manager.coolBurnFuelDecreaseMax);
        growthLevel -= Random.Range(Manager.coolBurnGrowthDecreaseMin, Manager.coolBurnGrowthDecreaseMax);

        print("Cool Burn Performed");
    }

    


}
