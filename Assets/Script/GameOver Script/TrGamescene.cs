using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrGamescene : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update

    public void Onclick()
    {
        _animator.SetBool("Fade", true);
        Invoke(nameof(SceneLoad), 1.1f);
    }
    
    
    void SceneLoad()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
