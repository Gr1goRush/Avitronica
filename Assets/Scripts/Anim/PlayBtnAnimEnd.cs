using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBtnAnimEnd : MonoBehaviour
{
    public void TriggerAnimEnd()
    {
        GameManager.Instance.StartGame();
    }
}
