using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor.U2D;
using UnityEngine.UI;
public class LevelUpSelect : MonoBehaviour
{

    //[SerializeField] GameObject[] _Skill;
    [SerializeField] TMP_Text m_TextMeshPro;
    [SerializeField] PlayerMove m_pPlayerMove;
    bool _isPick = false;

    public bool _isAxePick;
    public bool _isPotionPick;

    int m_random;
    //[SerializeField] GameObject[] m_gameobject;
    [SerializeField] Sprite[] m_image;
    [SerializeField] UnityEngine.UI.Image m_showSprite;
    
    string[] Skill = 
    {
        "ポーション",
        "斧"
    };

    //public Image hogeA;
    //public Sprite hogaA;
    //public Sprite hogaB;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //m_TextMeshPro.text = null;
        //m_pPlayerMove = new PlayerMove();
        if (Input.GetKeyDown(KeyCode.Space))
        {

            _isPick = true;
            m_random = Random.Range(0, Skill.Length);
            m_TextMeshPro.text = Skill[m_random];
        }

        if (m_random == 0) m_showSprite.sprite = m_image[0];
        if (m_random == 1) m_showSprite.sprite = m_image[1];



    }

    public void Pick()
    {
        if (m_random == 0)
        {
            if (!_isPick) return;
            Posion();
            Debug.Log("ポーションが選ばれた");
        }

        if (m_random == 1)
        {
            if (!_isPick) return;
            Axe();
            Debug.Log("斧が選ばれた");

        }
    }

    void Axe()
    {

       
        m_pPlayerMove._axePick = true;
        _isPick = false;

    }

    void Posion()
    {
        m_pPlayerMove.HealHp();
        _isPick = false;
    }


}
