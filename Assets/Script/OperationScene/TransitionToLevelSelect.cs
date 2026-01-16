using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToLevelSelect : MonoBehaviour
{


   public Animator _animator;

    public void Onclick()
    {
        _animator.SetBool("Move", true);
        Invoke(nameof(Transition), 0.8f);
    }
    public void Transition()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
