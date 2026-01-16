using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public Animator _animator;

    public void Shaker()
    {
        _animator.SetTrigger("Shake");
    }

   
   
}
