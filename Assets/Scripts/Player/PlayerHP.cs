using SMP_LIG;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    //this need a complet refacto with event
    [SerializeField]
    private float m_playerMaxHP = 100;
    [SerializeField]
    private float m_playerHP;
    [SerializeField]
    private TextMeshProUGUI m_hpUI;
    [SerializeField]
    private Slider m_lifeSlider;
    private bool m_isDead;
    public bool IsDead {  get { return m_isDead; } }

    public static PlayerHP Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_playerHP = m_playerMaxHP;
    }

    public static void DamagePlayer(float damage) => Instance?.TakeDamage(damage);
    public static void HealPlayer(float healing) => Instance?.Heal(healing);

    public void TakeDamage(float damage)
    {
        if (m_isDead) return;
        m_playerHP = Mathf.Clamp(m_playerHP - damage, 0, m_playerMaxHP);
        UpdateHPUI();
        if (m_playerHP > 0) return;
        PlayerDeath();
    }
    

    public void Heal(float heal)
    {
        if (m_isDead) return;
        m_playerHP = Mathf.Clamp(m_playerHP + heal, 0, m_playerMaxHP);
        UpdateHPUI();
    }

    private void UpdateHPUI()
    {
        m_lifeSlider.value = m_playerHP;
    }
    private void PlayerDeath()
    {
        m_isDead = true;
        ScoreManagerArcade.SaveScore.Invoke(ScoreSystem.Instance.Score);
    }
}
