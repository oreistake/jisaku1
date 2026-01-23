using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{

    public Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _clickSe;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void Shaker()
    {
        _audioSource.PlayOneShot(_clickSe);
        _animator.SetTrigger("Shake");
    }

   
   
}
