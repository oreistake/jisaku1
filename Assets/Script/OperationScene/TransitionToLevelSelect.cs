using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionToLevelSelect : MonoBehaviour
{


   public Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _ClickSe;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Onclick()
    {
        _audioSource.PlayOneShot(_ClickSe);
        _animator.SetBool("Move", true);
        Invoke(nameof(Transition), 0.8f);
    }
    public void Transition()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
