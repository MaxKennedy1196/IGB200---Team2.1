using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System.Data;
using UnityEngine.SocialPlatforms.Impl;



public class Sector : MonoBehaviour
{
    //Script for describing a sector and all related functions 
    public GameManager Manager;

    [Header("Sector Variables")]

    public int xPos;//x Position of this sector
    public int yPos;//y Position of this sector



    public enum Status
    {
        incinerated,
        hotBurn,
        coolBurn,
        healthy,
        dry,
        veryDry
    }

    public Status currentStatus;

    [Header("Planning Variables")]

    public int plannedTurns;

    [Header("Wildfire Variables")]
    public GameObject wildfireGraphics;
    
    public bool wildfire;

    [Header("Environmental Challenge Variables")]

    public bool challengeEnabled;
    private string currentAction;
    float challengeTimer = 0f;
    float challengeRating = 20f;
    int challengePhase = 1;

    public GameObject challengeBorders;
    public List<GameObject> challengeTargetList = new List<GameObject>();
    public List<GameObject> planningTargetList = new List<GameObject>();
    public List<targetTrigger> challengeTriggerList = new List<targetTrigger>();

    public GameObject tileMapHealthy;
    public GameObject tileMapYellow;
    public GameObject tileMapOrange;
    public GameObject tileMapIncineratedOverlay;
    public GameObject tileMapCoolBurnedOverlay;

    public bool incinerated;
    public bool coolBurned;

    public bool randomInital;

    public bool isTownCentre;

    float[,] currentPattern;

    float[,] pattern1 =  {{-5f  , 0f  },
                          {-2.5f, 0f  },
                          { 0f  , 0f  },
                          { 2.5f, 0f  },
                          { 5f  , 0f  }};
 
    float[,] pattern2 =  {{ 5f  , 0f  },
                          { 2.5f, 0f  },
                          { 0f  , 0f  },
                          {-2.5f, 0f  },
                          {-5f  , 0f  }};
 
    float[,] pattern3 =  {{ 0f  , 5   },
                          { 0f  , 2.5f},
                          { 0f  , 0   },
                          { 0f  ,-2.5f},
                          { 0f  ,-5   }};
 
    float[,] pattern4 =  {{ 0f  ,-5   },
                          { 0f  ,-2.5f},
                          { 0f  , 0   },
                          { 0f  , 2.5f},
                          { 0f  , 5   }}; 
 
    float[,] pattern5 =  {{-5   ,-5   },
                          {-2.5f,-2.5f},
                          { 0   , 0   },
                          { 2.5f, 2.5f},
                          { 5   , 5   }};
 
    float[,] pattern6 =  {{ 5   , 5   },
                          { 2.5f, 2.5f},
                          { 0   , 0   },
                          {-2.5f,-2.5f},
                          {-5   ,-5   }};
 
    float[,] pattern7 =  {{ 5   ,-5   },
                          { 2.5f,-2.5f},
                          { 0   , 0   },
                          {-2.5f, 2.5f},
                          {-5   , 5   }}; 

    float[,] pattern8 =  {{-5   , 5   },
                          {-2.5f, 2.5f},
                          { 0   , 0   },
                          { 2.5f,-2.5f},
                          { 5   ,-5   }};                                         

    float[,] pattern9 =  {{-5f  ,-5f  },
                          {-2.5f, 5f  },
                          { 0f  ,-5f  },
                          { 2.5f, 5f  },
                          { 5f  ,-5f  }};

    float[,] pattern10 = {{-5f  , 5f  },
                          {-2.5f,-5f  },
                          { 0f  , 5f  },
                          { 2.5f,-5f  },
                          { 5f  , 5f  }};

    float[,] pattern11 = {{ 5f  ,-5f  },
                          { 2.5f, 5f  },
                          { 0f  ,-5f  },
                          {-2.5f, 5f  },
                          {-5f  ,-5f  }};
     
    float[,] pattern12 = {{ 5f  , 5f  },
                          { 2.5f,-5f  },
                          { 0f  , 5f  },
                          {-2.5f,-5f  },
                          {-5f  , 5f  }};                      

    float[,] pattern13 = {{ 5f  ,-5f  },
                          {-5f  ,-2.5f},
                          { 5f  , 0f  },
                          {-5f  , 2.5f},
                          { 5f  , 5f  }};

    float[,] pattern14 = {{-5f  ,-5f  },
                          { 5f  ,-2.5f},
                          {-5f  , 0f  },
                          { 5f  , 2.5f},
                          {-5f  , 5f  }};                                       

    float[,] pattern15 = {{ 5f  , 5f  },
                          {-5f  , 2.5f},
                          { 5f  , 0f  },
                          {-5f  ,-2.5f},
                          { 5f  ,-5f  }};

    float[,] pattern16 = {{-5f  , 5f  },
                          { 5f  , 2.5f},
                          {-5f  , 0f  },
                          { 5f  ,-2.5f},
                          {-5f  ,-5f  }};







    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Manager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();//find the GameManager
        Manager.sectorList.Add(gameObject.GetComponent<Sector>());//Add this sector to SectorList in the GameManager 


        sectorInit();


        foreach (GameObject target in challengeTargetList)
        {
            challengeTriggerList.Add(target.GetComponent<targetTrigger>());
        }

        //foreach (targetTrigger trigger in challengeTriggerList)
        //{
        //    trigger.sector = gameObject.GetComponent<Sector>();
        //}

        challengeTriggerList[0].fireSprite.SetActive(false);
        challengeTriggerList[1].fireSprite.SetActive(false);
        challengeTriggerList[2].fireSprite.SetActive(false);
        challengeTriggerList[3].fireSprite.SetActive(false);
        challengeTriggerList[4].fireSprite.SetActive(false);
    

        challengeTriggerList[0].planningFlag.SetActive(false);
        challengeTriggerList[1].planningFlag.SetActive(false);
        challengeTriggerList[2].planningFlag.SetActive(false);
        challengeTriggerList[3].planningFlag.SetActive(false);
        challengeTriggerList[4].planningFlag.SetActive(false);

    }



    void Update()
    {
        if (isTownCentre == true)
        {
            currentStatus = Status.healthy;
            wildfire = false;
        }


        if (wildfire == true)
        {
            wildfireGraphics.SetActive(true);
        }
        if (wildfire == false)
        {
            wildfireGraphics.SetActive(false);
        }

        updateDisplayTiles();

        environmentalChallenge();//handles all environmental challenge logic

        
    }

    private void updateDisplayTiles()
    {

        tileMapOrange.SetActive(false);
        tileMapYellow.SetActive(false);
        tileMapHealthy.SetActive(false);
        tileMapIncineratedOverlay.SetActive(false);
        tileMapCoolBurnedOverlay.SetActive(false);

        if (currentStatus == Status.veryDry)
        {
            tileMapOrange.SetActive(true);
        }
        if (currentStatus == Status.dry)
        {
            tileMapYellow.SetActive(true);
        }
        if (currentStatus == Status.healthy)
        {
            tileMapHealthy.SetActive(true);
        }
        if (currentStatus == Status.incinerated)
        {
            tileMapIncineratedOverlay.SetActive(true);
        }
        if (currentStatus == Status.hotBurn)
        {
            tileMapIncineratedOverlay.SetActive(true);
        }
        if (currentStatus == Status.coolBurn)
        {
            tileMapCoolBurnedOverlay.SetActive(true);
        }
    }

    void sectorInit()
    {
        if (randomInital == true)
        {
            currentStatus = Status.healthy;

            dryRandomise();

            wildfire = false;
            //wildfireGraphics.enabled = false;
        }
            
    }

    void dryRandomise()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.dryChance)
        {
            currentStatus = Status.dry;
        }
    }

    void veryDryRandomise()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.veryDryChance)
        {
            currentStatus = Status.veryDry;
        }
    }

    void healthyRandomiseFromCoolBurn()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromCoolBurn)
        {
            currentStatus = Status.healthy;
        }
    }

    void healthyRandomiseFromHotBurn()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromHotBurn)
        {
            currentStatus = Status.healthy;
        }
    }

    void healthyRandomiseFromIncinerated()
    {
        float randomiser = Random.Range(0f, 100f);

        if (randomiser <= Manager.healthyChanceFromIncinerated)
        {
            currentStatus = Status.healthy;
        }
    }


    public void nextMonth()
    {
        if (currentStatus == Status.hotBurn)
        {
            Manager.scoreIncrease(Manager.pointsHotBurned);
        }
        if (currentStatus == Status.coolBurn)
        {
            Manager.scoreIncrease(Manager.pointsCoolBurned);
        }
        if (currentStatus == Status.healthy)
        {
            Manager.scoreIncrease(Manager.pointsHealthy);
        }
        if (currentStatus == Status.dry)
        {
            Manager.scoreIncrease(Manager.pointsDry);
        }
        if (currentStatus == Status.veryDry)
        {
            Manager.scoreIncrease(Manager.pointsVerDry);
        }

        incinerated = false;
        coolBurned = false;

        plannedTurns -= 1;
        if (plannedTurns <= 0)
        {
            plannedTurns = 0;
        }

        if (wildfire == true)
        {
            completeWildfire();
            return;
        }
        if (wildfire == false)
        {
            updateStatus();


            if (currentStatus == Status.veryDry)
            {
                if (Manager.seasonName == "Spring")
                {
                    float randomiser = Random.Range(0f, 100f);

                    if (randomiser <= Manager.wildfireChanceSpring)
                    {
                        startWildFire();
                    }
                }
                if (Manager.seasonName == "Summer")
                {
                    float randomiser = Random.Range(0f, 100f);

                    if (randomiser <= Manager.wildfireChanceSummer)
                    {
                        startWildFire();
                    }
                }
            }

            return;
        }
        


    }

    void updateStatus()
    {
        if (currentStatus == Status.coolBurn)
        {
            healthyRandomiseFromCoolBurn();
            return;
        }
        if (currentStatus == Status.hotBurn)
        {
            healthyRandomiseFromHotBurn();
            return;
        }
        if (currentStatus == Status.incinerated)
        {
            healthyRandomiseFromIncinerated();
            return;
        }
        if (currentStatus == Status.healthy)
        {
            dryRandomise();
            return;
        }
        if (currentStatus == Status.dry)
        {
            veryDryRandomise();
            return;
        }
    }

    private void startWildFire()
    {
        wildfireGraphics.SetActive(true);
        wildfire = true;

        plannedTurns = 0;

        print("WILDFIRE!!!");

    }

    public void startCoolBurn()
    {
        currentAction = "coolBurn";
        beginEnvironmentalChallenge();
    }

    public void startHotBurn()
    {
        currentAction = "hotBurn";

        beginEnvironmentalChallenge();
    }

    public void startPlanning()
    {
        currentAction = "planning";
        beginEnvironmentalChallenge();
    }

    public void startExtinguish()
    {
        currentAction = "extinguish";
        beginEnvironmentalChallenge();
    }


    private void completeWildfire()
    {
        wildfire = false;
        currentStatus = Status.incinerated;
    }

    private void completeCoolBurn(float challengeScore)
    {
        float finalScore = 0;
        if (challengeScore < 0)
        {
            challengeScore = 0;
        }

        if (plannedTurns == 0)
            {
                finalScore = challengeScore * Manager.scoreMultiplier;
                Manager.scoreIncrease(finalScore);
            }
        if (plannedTurns != 0)
        {
            finalScore = challengeScore * Manager.scoreMultiplier * Manager.planningScoreMuliplier;
            Manager.scoreIncrease(finalScore);
            plannedTurns = 0;
        }

        currentStatus = Status.coolBurn;
        print("Cool Burn Performed: " + finalScore);
    }

    private void completeHotBurn(float challengeScore)
    {
        if (challengeScore < 0)
        {
            challengeScore = 0;
        }

        if (plannedTurns == 0)
        {
            Manager.scoreIncrease(challengeScore * Manager.scoreMultiplier);
        }
        if (plannedTurns != 0)
        {
            Manager.scoreIncrease(challengeScore * Manager.scoreMultiplier * Manager.planningScoreMuliplier);
            plannedTurns = 0;
        }

        currentStatus = Status.hotBurn;
        print("Hot Burn Performed");
    }

    private void completePlanning(float challengeScore)
    {
        if (challengeScore < 0)
        {
            challengeScore = 0;
        }

        plannedTurns = Manager.planningDuration;

        print("Planning Complete");
    }

    private void completeExtinguish(float challengeScore)
    {
        if (challengeScore < 0)
        {
            challengeScore = 0;
        }

        wildfire = false;
        print("Extinguish Performed");
    }



    private void beginEnvironmentalChallenge()
    {
        randomizeChallengePattern();// randomizes current pattern
        challengeEnabled = true;
        challengePhase = 1;
        challengeTimer = 0f;
    }

    private void randomizeChallengePattern()
    {
        int randomizer = Random.Range(1, 17);

        switch (randomizer)
        {
            case 1:
                currentPattern = pattern1;
                break;
            case 2:
                currentPattern = pattern2;
                break;
            case 3:
                currentPattern = pattern3;
                break;
            case 4:
                currentPattern = pattern4;
                break;
            case 5:
                currentPattern = pattern5;
                break;
            case 6:
                currentPattern = pattern6;
                break;
            case 7:
                currentPattern = pattern7;
                break;
            case 8:
                currentPattern = pattern8;
                break;
            case 9:
                currentPattern = pattern9;
                break;
            case 10:
                currentPattern = pattern10;
                break;
            case 11:
                currentPattern = pattern11;
                break;
            case 12:
                currentPattern = pattern12;
                break;
            case 13:
                currentPattern = pattern13;
                break;
            case 14:
                currentPattern = pattern14;
                break;
            case 15:
                currentPattern = pattern15;
                break;
            case 16:
                currentPattern = pattern16;
                break;             
        }
    }

    private void environmentalChallenge()
    {

        if (challengeEnabled == true)
        {
            challengeTimer += Time.deltaTime;
            print(challengeTimer);

            challengeBorders.SetActive(true);

            int iterator = 0;
            foreach (GameObject target in challengeTargetList)
            {
                target.transform.position = transform.position + new Vector3(currentPattern[iterator, 0], currentPattern[iterator, 1]);
                iterator += 1;
                target.SetActive(true);
            }
            
            iterator = 0;
            foreach (GameObject target in planningTargetList)
            {
                target.transform.position = transform.position + new Vector3(currentPattern[iterator, 0], currentPattern[iterator, 1]);
                iterator += 1;
                //target.SetActive(true);
            }

            if (challengePhase == 1)
            {
                challengeTriggerList[0].currentState = targetTrigger.state.activated;
                challengeTriggerList[1].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[2].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[3].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[4].currentState = targetTrigger.state.preActivation;

                if (currentAction == "coolBurn" || currentAction == "hotBurn")
                {
                    challengeTriggerList[0].fireSprite.SetActive(false);
                    challengeTriggerList[1].fireSprite.SetActive(false);
                    challengeTriggerList[2].fireSprite.SetActive(false);
                    challengeTriggerList[3].fireSprite.SetActive(false);
                    challengeTriggerList[4].fireSprite.SetActive(false);
                }

                if (currentAction == "planning")
                {
                    challengeTriggerList[0].planningFlag.SetActive(false);
                    challengeTriggerList[1].planningFlag.SetActive(false);
                    challengeTriggerList[2].planningFlag.SetActive(false);
                    challengeTriggerList[3].planningFlag.SetActive(false);
                    challengeTriggerList[4].planningFlag.SetActive(false);
                }

            }

            if (challengePhase == 2)
            {
                challengeTriggerList[0].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[1].currentState = targetTrigger.state.activated;
                challengeTriggerList[2].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[3].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[4].currentState = targetTrigger.state.preActivation;

                if (currentAction == "coolBurn" || currentAction == "hotBurn")
                {
                    challengeTriggerList[0].fireSprite.SetActive(true);
                    challengeTriggerList[1].fireSprite.SetActive(false);
                    challengeTriggerList[2].fireSprite.SetActive(false);
                    challengeTriggerList[3].fireSprite.SetActive(false);
                    challengeTriggerList[4].fireSprite.SetActive(false);
                }

                if (currentAction == "planning")
                {
                    challengeTriggerList[0].planningFlag.SetActive(true);
                    challengeTriggerList[1].planningFlag.SetActive(false);
                    challengeTriggerList[2].planningFlag.SetActive(false);
                    challengeTriggerList[3].planningFlag.SetActive(false);
                    challengeTriggerList[4].planningFlag.SetActive(false);
                }
            }

            if (challengePhase == 3)
            {
                challengeTriggerList[0].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[1].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[2].currentState = targetTrigger.state.activated;
                challengeTriggerList[3].currentState = targetTrigger.state.preActivation;
                challengeTriggerList[4].currentState = targetTrigger.state.preActivation;

                if (currentAction == "coolBurn" || currentAction == "hotBurn")
                {
                    challengeTriggerList[0].fireSprite.SetActive(true);
                    challengeTriggerList[1].fireSprite.SetActive(true);
                    challengeTriggerList[2].fireSprite.SetActive(false);
                    challengeTriggerList[3].fireSprite.SetActive(false);
                    challengeTriggerList[4].fireSprite.SetActive(false);
                }

                if (currentAction == "planning")
                {
                    challengeTriggerList[0].planningFlag.SetActive(true);
                    challengeTriggerList[1].planningFlag.SetActive(true);
                    challengeTriggerList[2].planningFlag.SetActive(false);
                    challengeTriggerList[3].planningFlag.SetActive(false);
                    challengeTriggerList[4].planningFlag.SetActive(false);
                }
            }

            if (challengePhase == 4)
            {
                challengeTriggerList[0].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[1].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[2].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[3].currentState = targetTrigger.state.activated;
                challengeTriggerList[4].currentState = targetTrigger.state.preActivation;

                if (currentAction == "coolBurn" || currentAction == "hotBurn")
                {
                    challengeTriggerList[0].fireSprite.SetActive(true);
                    challengeTriggerList[1].fireSprite.SetActive(true);
                    challengeTriggerList[2].fireSprite.SetActive(true);
                    challengeTriggerList[3].fireSprite.SetActive(false);
                    challengeTriggerList[4].fireSprite.SetActive(false);
                }

                if (currentAction == "planning")
                {
                    challengeTriggerList[0].planningFlag.SetActive(true);
                    challengeTriggerList[1].planningFlag.SetActive(true);
                    challengeTriggerList[2].planningFlag.SetActive(true);
                    challengeTriggerList[3].planningFlag.SetActive(false);
                    challengeTriggerList[4].planningFlag.SetActive(false);
                }
            }

            if (challengePhase == 5)
            {
                challengeTriggerList[0].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[1].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[2].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[3].currentState = targetTrigger.state.postActivation;
                challengeTriggerList[4].currentState = targetTrigger.state.activated;

                if (currentAction == "coolBurn" || currentAction == "hotBurn")
                {
                    challengeTriggerList[0].fireSprite.SetActive(true);
                    challengeTriggerList[1].fireSprite.SetActive(true);
                    challengeTriggerList[2].fireSprite.SetActive(true);
                    challengeTriggerList[3].fireSprite.SetActive(true);
                    challengeTriggerList[4].fireSprite.SetActive(false);
                }

                if (currentAction == "planning")
                {
                    challengeTriggerList[0].planningFlag.SetActive(true);
                    challengeTriggerList[1].planningFlag.SetActive(true);
                    challengeTriggerList[2].planningFlag.SetActive(true);
                    challengeTriggerList[3].planningFlag.SetActive(true);
                    challengeTriggerList[4].planningFlag.SetActive(false);
                }
            }

            if (currentAction == "planning" || currentAction == "extinguish")
            {
                challengeTriggerList[0].fireSprite.SetActive(false);
                challengeTriggerList[1].fireSprite.SetActive(false);
                challengeTriggerList[2].fireSprite.SetActive(false);
                challengeTriggerList[3].fireSprite.SetActive(false);
                challengeTriggerList[4].fireSprite.SetActive(false);
            }

            if (currentAction == "coolBurn" || currentAction == "hotBurn")
            {
                challengeTriggerList[0].planningFlag.SetActive(false);
                challengeTriggerList[1].planningFlag.SetActive(false);
                challengeTriggerList[2].planningFlag.SetActive(false);
                challengeTriggerList[3].planningFlag.SetActive(false);
                challengeTriggerList[4].planningFlag.SetActive(false);
            }

            if (currentAction == "coolBurn" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeCoolBurn(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "hotBurn" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeHotBurn(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "extinguish" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completeExtinguish(challengeScoreInverted);
                environmentalChallengeReset();
            }

            if (currentAction == "planning" && challengePhase > challengeTriggerList.Count)
            {
                float challengeScore = challengeTimer / challengeRating;
                float challengeScoreInverted = 1 - challengeScore;

                completePlanning(challengeScoreInverted);
                environmentalChallengeReset();

                challengeTriggerList[0].planningFlag.SetActive(true);
                challengeTriggerList[1].planningFlag.SetActive(true);
                challengeTriggerList[2].planningFlag.SetActive(true);
                challengeTriggerList[3].planningFlag.SetActive(true);
                challengeTriggerList[4].planningFlag.SetActive(true);
            }
        }
        if (challengeEnabled == false)
        {
            challengeBorders.SetActive(false);

            
            
        }
    }

    private void environmentalChallengeReset()
    {
        currentAction = "null";
        challengeEnabled = false;
        challengePhase = 1;

        foreach (GameObject target in challengeTargetList)
        {
            target.SetActive(false);
        }

        foreach (targetTrigger trigger in challengeTriggerList)
        {
            trigger.isActive = false;
        }
    }

    public void nextChallengePhase()
    {
        challengePhase += 1;
    }




}
