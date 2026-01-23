using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionTitle : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] AudioClip _clickSe;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void Onclick()
    {
        _audioSource.PlayOneShot(_clickSe);
        Invoke(nameof(Trscene), 0.2f);
    }

    void Trscene()
    {
        SceneManager.LoadScene("TitleScene");
    }


}
