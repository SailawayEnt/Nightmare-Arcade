using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

#region Serializable classes
[System.Serializable]
public class EnemyWaves 
{
    [Tooltip("time for wave generation from the moment the game started")]
    public float timeToStart;

    [Tooltip("Enemy wave's prefab")]
    public GameObject wave;
}

#endregion

public class LevelController : MonoBehaviour {

    //Serializable classes implements
    public EnemyWaves[] enemyWaves; 

    public GameObject powerUp;
    public float timeForNewPowerup;
    public GameObject[] planets;
    public float timeBetweenPlanets;
    public float planetsSpeed;
    List<GameObject> planetsList = new List<GameObject>();
    bool allEnemiesDied = false;
    bool gameEnded = false;

    [SerializeField] GameEvent onTicketReceived;
    [SerializeField] GameEvent onGameWon;
    [SerializeField] ConsumableItem ticketInventory;
    [SerializeField] ScenesData scenesData;
    

    Camera mainCamera;

    [SerializeField] GameObject playerGO;
    void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if(!allEnemiesDied)
        StartCoroutine("FindEnemies");
        
    }

    public IEnumerator LoadFirstWave()
    {
        yield return new WaitForSeconds(0.25f);
        playerGO.gameObject.SetActive(true);
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        for (int i = 0; i < enemyWaves.Length; i++) 
        {
            StartCoroutine(CreateEnemyWave(enemyWaves[i].timeToStart, enemyWaves[i].wave));
        }
        

        StartCoroutine(PowerupBonusCreation());
        StartCoroutine(PlanetsCreation());
        
    }

    public IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(.5f);
        GameObject[] enemyGOs;
        enemyGOs = GameObject.FindGameObjectsWithTag("Enemy");
        

        if (enemyGOs.Length == 0 && !gameEnded)
        {
            allEnemiesDied = true;
            if (ticketInventory.CurrentStack < ticketInventory.MaxStack)
            {
                Debug.Log("game won and ticket given");
               ticketInventory.CurrentStack += 1;
               onTicketReceived?.Invoke();
               scenesData.LoadLevelWithIndex(1);
            }
            onGameWon?.Invoke();
            gameEnded = true;
        }
    }
    
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject Wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.instance != null)
        {
            Instantiate(Wave);
        }
    }

    //endless coroutine generating 'levelUp' bonuses. 
    IEnumerator PowerupBonusCreation() 
    {
        while (true) 
        {
            yield return new WaitForSeconds(timeForNewPowerup);
            Instantiate(
                powerUp,
                //Set the position for the new bonus: for X-axis - random position between the borders of 'Player's' movement; for Y-axis - right above the upper screen border 
                new Vector2(
                    Random.Range(PlayerMoving.instance.borders.minX, PlayerMoving.instance.borders.maxX), 
                    mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the arrey
        for (int i = 0; i < planets.Length; i++)
        {
            planetsList.Add(planets[i]);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            int randomIndex = Random.Range(0, planetsList.Count);
            GameObject newPlanet = Instantiate(planetsList[randomIndex]);
            planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (planetsList.Count == 0)
            {
                for (int i = 0; i < planets.Length; i++)
                {
                    planetsList.Add(planets[i]);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }
}
