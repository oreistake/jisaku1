using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Hard_Mode : MonoBehaviour
{
    public Animator _Animator;

    private AudioSource _audioSource;

    [SerializeField] AudioClip _clickSe;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void Update()
    {
       
       
        
    }

    public void Hard_Mode_Transition()
    {
        _audioSource.PlayOneShot(_clickSe);
        Invoke(nameof(Anime), 0.3f);
        Invoke(nameof(SceneTr), 1.4f);
    }

    void Anime()
    {
         _Animator.SetBool("Move", true);
    }
    void SceneTr()
    {
        SceneManager.LoadScene("LoadScene");
    }
    
}
