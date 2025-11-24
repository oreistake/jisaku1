using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private int _aliveEnemyCount = 0;
    private int _spawnedEnemyTotal = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // EnemySpawner ‚©‚çŒÄ‚Î‚ê‚é
    public void RegisterSpawn()
    {
        _aliveEnemyCount++;
        _spawnedEnemyTotal++;
    }

    // Enemy‚ª€–S‚µ‚½‚Æ‚«‚É Enemy ‚ªŒÄ‚Ô
    public void RegisterDeath()
    {
        _aliveEnemyCount--;

        // ‚·‚×‚Ä spawn Ï‚İ ‚©‚Â ¶‘¶ 0
        if (_aliveEnemyCount <= 0 && _spawnedEnemyTotal > 0)
        {
            SceneManager.LoadScene("GameClearScene");
        }
    }
}
