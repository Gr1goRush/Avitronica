using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pointsText;

    public void Show(int points)
    {
        pointsText.text = points.ToString();
        gameObject.SetActive(true);
    }

    public void LoadMenu()
    {
        GameManager.Instance.LoadMenu();
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
