using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Hard_Mode : MonoBehaviour
{
    public Animator _Animator;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {
       
       
        
    }

    public void Hard_Mode_Transition()
    {
         _Animator.SetBool("Move", true);
        Invoke(nameof(SceneTr), 1.1f);
    }


    void SceneTr()
    {
        SceneManager.LoadScene("LoadScene");
    }
    
}
