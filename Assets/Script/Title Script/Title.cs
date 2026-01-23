using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    private Animator _Animator;

    private AudioSource _audioSource;

    [SerializeField] AudioClip _clickSe;
    // Start is called before the first frame update
    void Start()
    {
        _Animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _audioSource.PlayOneShot(_clickSe);
            _Animator.SetBool("Move", true);
            Invoke(nameof(SceneTr), 0.8f);
        }
    }

    void SceneTr()
    {
        SceneManager.LoadScene("Level_SelectionScene");
    }
}
