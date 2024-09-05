using System.Collections;
using UnityEngine;

public delegate void TriggerTankAction(EnemyTank enemyTank);
public class EnemyBombDropTrigger : MonoBehaviour
{

    [SerializeField] private string tankEnemyTag;

    public event TriggerTankAction OnTankTriggered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tankEnemyTag))
        {
            EnemyTank tank = collision.GetComponent<EnemyTank>();
            OnTankTriggered?.Invoke(tank);
        }
    }
}