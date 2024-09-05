using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirplaneController : ExplodingObject
{
    [SerializeField] private GameManager gameManager;

    protected override void OnExplodeAnimation()
    {
        gameManager.GameOver();
        
        base.OnExplodeAnimation();
    }
}
