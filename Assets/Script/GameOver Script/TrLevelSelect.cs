using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrLevelSelect : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    
    public void Onclick()
    {
        _animator.SetBool("Fade", true);
        Invoke(nameof(SceneTitle), 1.1f);
    }
    
    
    void SceneTitle()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
