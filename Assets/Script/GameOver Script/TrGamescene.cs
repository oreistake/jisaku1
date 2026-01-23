using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrGamescene : MonoBehaviour
{
    public Animator _animator;
    private AudioSource _audioSource;
    [SerializeField] AudioClip _clickSe;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Onclick()
    {
        _audioSource.PlayOneShot(_clickSe);
        _animator.SetBool("Fade", true);
        Invoke(nameof(SceneLoad), 1.1f);
    }
    
    
    void SceneLoad()
    {
        SceneManager.LoadScene("LoadScene");
    }
}
