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

    public bool wildfire;




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
        fuelText.text = "Fuel:" + Mathf.RoundToInt(fuelLevel);
        growthText.text = "Growth:" + Mathf.RoundToInt(growthLevel);

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

        if (wildfire == true)
        {
            fireImage.enabled = true;
        }
        if (wildfire == false)
        {
            fireImage.enabled = false;
        }
    }

    void sectorInit()
    {
        fuelText.text = "Fuel:" + Mathf.RoundToInt(fuelLevel);
        growthText.text = "Growth:" + Mathf.RoundToInt(growthLevel);

        wildfire = false;
        fireImage.enabled = false;
    }

    public void nextMonth()
    {
        if (wildfire == true)
        {
            fuelLevel = 0f;
            growthLevel = 0f;
            wildfire = false;
            return;
        }
        if (wildfire == false)
        {
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
            return;
        }

    }

    private void startWildFire()
    {
        fireImage.enabled = true;
        wildfire = true;

        print("WILDFIRE!!!");

    }

    public void startCoolBurn(float score)
    {
        fuelLevel -= Random.Range(Manager.coolBurnFuelDecreaseMin, Manager.coolBurnFuelDecreaseMax);
        growthLevel -= Random.Range(Manager.coolBurnGrowthDecreaseMin, Manager.coolBurnGrowthDecreaseMax);

        print("Cool Burn Performed");
    }

    public void startHotBurn(float score)
    {
        fuelLevel -= Random.Range(Manager.hotBurnFuelDecreaseMin, Manager.hotBurnFuelDecreaseMax);
        growthLevel -= Random.Range(Manager.hotBurnGrowthDecreaseMin, Manager.hotBurnGrowthDecreaseMax);

        print("Hot Burn Performed");
    }
    
    public void startExtinguish(float score)
    {
        fuelLevel -= Random.Range(Manager.extinguishFuelDecreaseMin, Manager.extinguishFuelDecreaseMax);
        growthLevel -= Random.Range(Manager.extinguishGrowthDecreaseMin, Manager.extinguishGrowthDecreaseMax);

        wildfire = false;

        print("Extinguish Performed");
    }

    


}
