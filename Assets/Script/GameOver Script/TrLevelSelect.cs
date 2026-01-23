using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrLevelSelect : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
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
        Invoke(nameof(SceneTitle), 1.1f);
    }
    
    
    void SceneTitle()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
