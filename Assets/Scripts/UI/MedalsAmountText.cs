using TMPro;
using UnityEngine;

public class MedalsAmountText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        SetMedals(GameManager.Instance.MedalsCount);

        GameManager.Instance.OnMedalsAmountChanged += SetMedals;
    }

    void SetMedals(int amount)
    {
        _text.text = amount.ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnMedalsAmountChanged -= SetMedals;
        }
    }
}