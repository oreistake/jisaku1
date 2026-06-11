using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using UnityEditor.U2D;
using UnityEngine.UI;
public class LevelUpSelect : MonoBehaviour
{

    [SerializeField] TMP_Text m_WeaponName;         // 武器の名前

    [SerializeField] PlayerMove m_pPlayerMove;

    bool _isPick = false;

    int m_random;

    [SerializeField] Sprite[] m_image;

    [SerializeField] UnityEngine.UI.Image m_showSprite;

    void Update()
    {
        if (m_random == 0) m_showSprite.sprite = m_image[0];
        if (m_random == 1) m_showSprite.sprite = m_image[1];
        if (m_random == 2) m_showSprite.sprite = m_image[2];

    }

    public void RandomSelect()
    {
        _isPick = true;
        if (m_pPlayerMove.axeLevel > 4) m_pPlayerMove.axeLevel = 4;
        string[] Skill =
        {
        "ポーション\r\n\r\nプレイヤーのHPが回復する\r\n\r\n",
        "斧Lv."+ (m_pPlayerMove.axeLevel +1)+ "\r\n\r\n上方向にランダムに飛ばし下に落ちる\r\n\r\n",
        "魔法瓶Lv."+ (m_pPlayerMove.posionLevel +1)+ "\r\n\r\n周囲にランダムに降り注ぎ敵を攻撃する\r\n\r\n"
        };
        m_random = Random.Range(0, Skill.Length);
        m_WeaponName.text = Skill[m_random];
    }

    public void Pick()
    {
        if (m_random == 0)
        {
            if (!_isPick) return;
            Posion();
        }

        if (m_random == 1)
        {
            if (!_isPick) return;
            m_pPlayerMove.axeLevel++;
            Axe();
        }

        if(m_random == 2)
        {
            if (!_isPick) return;
            m_pPlayerMove.posionLevel++;
            MagicPosion();
        }

    }

    void Axe()
    {
        m_pPlayerMove.AttackSword();
        _isPick = false;
    }

    void Posion()
    {
        m_pPlayerMove.HealHp();
        _isPick = false;
    }

    void MagicPosion()
    {
        m_pPlayerMove.AttackPosion();
        _isPick = false;
    }

}
