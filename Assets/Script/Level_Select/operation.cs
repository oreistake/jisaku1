using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public Animator _animator;   

    private AudioSource _audioSource;
    [SerializeField] AudioClip _clickSe;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Transition()
    {

        _audioSource.PlayOneShot(_clickSe);
        _animator.SetBool("Move", true);
        Invoke(nameof(Load), 1.1f);

    }
    public void Load()
    {

        SceneManager.LoadScene("OperationScene");
    
    }
}
