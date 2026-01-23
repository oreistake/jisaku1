using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPointDelete : MonoBehaviour
{
    public GameObject gameObuject;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Delete), 2.0f);

    }

    void Delete()
    {
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
