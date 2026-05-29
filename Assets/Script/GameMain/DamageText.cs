using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{

    [SerializeField] private float DeleteTime = 1.5f;
    [SerializeField] private float MoveRange = 50.0f;
    [SerializeField] private float EndAlpha = 0.2f;

    private float TimeCnt;
    private TMP_Text NowText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TimeCnt = 0.0f;
        Destroy(this.gameObject, DeleteTime);
        NowText = this.gameObject.GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeCnt += Time.deltaTime;
        this.gameObject.transform.localScale += new Vector3(0,MoveRange / DeleteTime * Time.deltaTime,0);
        float _alpha = 1.0f - (1.0f - EndAlpha) + (TimeCnt * DeleteTime);
        if(_alpha <= 0.0f) _alpha = 0.0f;
        NowText.color = new Color(NowText.color.r, NowText.color.g, NowText.color.b, _alpha);

    }
}
