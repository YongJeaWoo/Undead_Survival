using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum E_InfoType
{
    None,
    Exp,
    Level,
    Kill,
    Time,
    Health
}

public class HUD : MonoBehaviour
{
    [SerializeField] private E_InfoType infoType;

    private TextMeshProUGUI text;
    private Slider slider;

    private void Awake()
    {
        GetComponents();
    }

    private void LateUpdate()
    {
        InfoFractionation();
    }

    private void GetComponents()
    {
        text = GetComponent<TextMeshProUGUI>();
        slider = GetComponent<Slider>();
    }

    private void InfoFractionation()
    {
        switch (infoType)
        {
            case E_InfoType.Exp:
                {
                    float curExp = GameManager.Instance.GetExp();
                    float maxExp = GameManager.Instance.GetMaxExp();
                    slider.value = curExp / maxExp;
                }
                break;
            case E_InfoType.Level:
                {
                    int level = GameManager.Instance.GetLevel();
                    text.text = $"Lv. {level:F0}";
                }
                break;
            case E_InfoType.Kill:
                {
                    int kill = GameManager.Instance.GetKill();
                    text.text = $"{kill:D4}";
                }
                break;
            case E_InfoType.Time:
                {
                    float currentTime = GameManager.Instance.GetCurrentTime();
                    float maxTime = GameManager.Instance.GetMaxTime();

                    float remainTime = maxTime - currentTime;

                    int min = Mathf.FloorToInt(remainTime / 60);
                    int seconds = Mathf.FloorToInt(remainTime % 60);

                    text.text = $"{min:D2} : {seconds:D2}";
                }
                break;
            case E_InfoType.Health:
                {
                    float curHp = PlayerManager.Instance.GetCurrentHealth();
                    float maxHp = PlayerManager.Instance.GetMaxHealth();
                    slider.value = curHp / maxHp;
                }
                break;
        }
    }
}
