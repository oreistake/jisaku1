using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTitle : MonoBehaviour
{
   public void Onclick()
    {
        SceneManager.LoadScene("TitleScene");
    }


}
