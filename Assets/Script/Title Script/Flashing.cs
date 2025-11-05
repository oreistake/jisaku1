using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Flashing : MonoBehaviour
{
    [SerializeField] float blinkInterval = 0.5f;
    private TMP_Text tmpText;
    private bool isBlinking = false;

    void Start()
    {
        tmpText = GetComponent<TMP_Text>();
        StartBlinking();
    }

    public void StartBlinking()
    {
        if (!isBlinking)
            StartCoroutine(BlinkCoroutine());
    }

    public void StopBlinking()
    {
        isBlinking = false;
        tmpText.enabled = true;
    }

    private System.Collections.IEnumerator BlinkCoroutine()
    {
        isBlinking = true;
        while (isBlinking)
        {
            tmpText.enabled = !tmpText.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
