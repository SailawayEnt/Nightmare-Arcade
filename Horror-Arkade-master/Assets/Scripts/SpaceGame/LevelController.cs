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
    readonly List<GameObject> _planetsList = new List<GameObject>();
    bool _allEnemiesDied = false;
    bool _gameEnded = false;

    [SerializeField] GameEvent onTicketReceived;
    [SerializeField] GameEvent onGameWon;
    [SerializeField] ConsumableItem ticketInventory;
    [SerializeField] ScenesData scenesData;
    

    Camera _mainCamera;

    [SerializeField] GameObject playerGO;
    void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        // if(!_allEnemiesDied)
        //     StartCoroutine(FindEnemies());
        
    }

    public IEnumerator LoadFirstWave()
    {
        yield return new WaitForSeconds(0.25f);
        playerGO.gameObject.SetActive(true);
        //for each element in 'enemyWaves' array creating coroutine which generates the wave
        foreach (var wave in enemyWaves)
        {
            StartCoroutine(CreateEnemyWave(wave.timeToStart, wave.wave));
        }
        

        StartCoroutine(PowerupBonusCreation());
        if (_planetsList.Count > 0)
            StartCoroutine(PlanetsCreation());
        
    }

    public IEnumerator FindEnemies()
    {
        yield return new WaitForSeconds(.5f);
        var enemyGOs = GameObject.FindGameObjectsWithTag("Enemy");


        if (enemyGOs.Length != 0 || _gameEnded) yield break;

        _allEnemiesDied = true;
        
        if (ticketInventory.CurrentStack < ticketInventory.MaxStack)
        {
            Debug.Log("game won and ticket given");
            ticketInventory.CurrentStack += 1;
            onTicketReceived?.Invoke();
        }
        scenesData.LoadLevelWithIndex(2);
        onGameWon?.Invoke();
        _gameEnded = true;
    }
    
    //Create a new wave after a delay
    IEnumerator CreateEnemyWave(float delay, GameObject wave) 
    {
        if (delay != 0)
            yield return new WaitForSeconds(delay);
        if (Player.Instance != null)
        {
            Instantiate(wave);
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
                    _mainCamera.ViewportToWorldPoint(Vector2.up).y + powerUp.GetComponent<Renderer>().bounds.size.y / 2), 
                Quaternion.identity
                );
        }
    }

    IEnumerator PlanetsCreation()
    {
        //Create a new list copying the array
        foreach (var planet in planets)
        {
            _planetsList.Add(planet);
        }
        yield return new WaitForSeconds(10);
        while (true)
        {
            ////choose random object from the list, generate and delete it
            var randomIndex = Random.Range(0, _planetsList.Count);
            var newPlanet = Instantiate(_planetsList[randomIndex]);
            _planetsList.RemoveAt(randomIndex);
            //if the list decreased to zero, reinstall it
            if (_planetsList.Count == 0)
            {
                foreach (var planet in planets)
                {
                    _planetsList.Add(planet);
                }
            }
            newPlanet.GetComponent<DirectMoving>().speed = planetsSpeed;

            yield return new WaitForSeconds(timeBetweenPlanets);
        }
    }

    public void GameEnded()
    {
        _gameEnded = true;
    }
}
