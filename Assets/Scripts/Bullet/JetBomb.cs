using System.Collections;
using UnityEngine;

public class JetBomb : MonoBehaviour
{

    [HideInInspector] public EnemyTank triggeredTank;

    void Start()
    {
        Invoke(nameof(ExplodeTank), 0.5f);
    }

    void ExplodeTank()
    {
        triggeredTank.Explode();
    }
}