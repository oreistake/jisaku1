using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(SceneTr),5.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SceneTr()
    {
        SceneManager.LoadScene("GameScene");
    }

}
