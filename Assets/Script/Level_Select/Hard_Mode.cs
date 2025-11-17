using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hard_Mode : MonoBehaviour
{
    private Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _Animator.SetBool("Move", true);
            Invoke(nameof(SceneTr), 0.8f);
        }
    }

    void SceneTr()
    {
        SceneManager.LoadScene("GameScene");
    }
    
}
