using UnityEngine;
using UnityEngine.UI;
public class ToggleClickSound : MonoBehaviour
{

    void Awake()
    {
        GetComponent<Toggle>().onValueChanged.AddListener(ClickButton);
    }

    void ClickButton(bool _)
    {
        AudioController.Instance.Sounds.PlayOneShot("click");
    }
}