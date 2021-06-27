using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject enemyPrefab;

    Timer spawnTimer;
    GameObject player;//Player's game object
    float playerDistance;//The distance from the player
    // Start is called before the first frame update
    void Start()
    {
        ResetValues();
    }
    void ResetValues()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.SetTimer(0);
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        playerDistance = transform.position.x - player.transform.position.x;
        if (spawnTimer.time == 0 &Mathf.Abs(playerDistance)<15)
        {
            SpawnEnemy();
            spawnTimer.SetTimer(5f);
        }
    }
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position+new Vector3(-2,0), enemyPrefab.transform.rotation);
    }
}
