using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator _animator;   

    public void Transition()
    {

        _animator.SetBool("Move", true);
        Invoke(nameof(Load), 1.1f);

    }


    public void Load()
    {

        SceneManager.LoadScene("OperationScene");
    
    }
}
