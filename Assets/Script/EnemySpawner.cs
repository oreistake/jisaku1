using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    // 敵オブジェクト
    [SerializeField] private GameObject[] _enemy;

    // 敵を生成する時間
    [SerializeField] private float[] _spawnTimes;

    [SerializeField]private float _spawnCount;
    private int _spawnNum;

    // Start is called before the first frame update
    void Start()
    {
        _spawnCount = 0.0f;
        _spawnNum = 0;

    }

    // Update is called once per frame
    void Update()
    {
        _Spawn();
    }

    private void _Spawn()
    {
        if (_spawnNum > _enemy.Length - 1) return;

        _spawnCount += Time.deltaTime;

        if (_spawnCount >= _spawnTimes[_spawnNum])
        {
            GameObject enemy = Instantiate(_enemy[_spawnNum]);

           

            _spawnNum++;
            _spawnCount = 0f;
        }
    }

    
}
