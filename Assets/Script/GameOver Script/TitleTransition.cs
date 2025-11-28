using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleTransition : MonoBehaviour
{
    Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
       _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown(KeyCode.R))
        {
            _animator.SetBool("Fade", true);
            Invoke(nameof(SceneLoad), 1.1f);

        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            _animator.SetBool("Fade", true);
            Invoke(nameof(SceneTitle), 1.1f);

        }
    }
    void SceneLoad()
    {
        SceneManager.LoadScene("LoadScene");
    }

    void SceneTitle()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
