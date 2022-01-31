using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] 
    private GameObject _enemyPrefab;

    public int enemies = 0;
    private int _waves = 1;

    private Vector2 _spawnPosition = Vector2.zero;

    [SerializeField] private TMP_Text _enemiesCounter = null;

    private void Start()
    {
        Time.timeScale = 1;
        enemies = 0;
        _spawnPosition = new Vector2(Random.Range(-6.36f, 6.14f), Random.Range(-3.702f, 3.298f));
    }

    private void Update()
    {
        _enemiesCounter.text = "Enemies: " + enemies;
        
        if (enemies == 0)
        {
            _waves++;
            for (int i = 1; i < _waves; i++)
            {
                GameObject enemy = ObjectPool.SHAREDINSTANCE.GetPooledObject();

                if (enemy != null)
                {
                    enemy.transform.position = new Vector2(Random.Range(-6.36f, 6.14f), Random.Range(-3.702f, 3.298f));
                    enemy.transform.rotation = _enemyPrefab.transform.rotation;
                    enemy.SetActive(true);
                }
                
                Debug.Log("Enemy Spawned");
                _spawnPosition = new Vector2(Random.Range(-6.36f, 6.14f), Random.Range(-3.702f, 3.298f));
                enemies++;
            }
        }
    }
}
