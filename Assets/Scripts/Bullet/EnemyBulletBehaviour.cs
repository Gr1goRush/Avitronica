using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehaviour : BulletBehaviour
{

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Airplane"))
        {
            this.Invoke(() => HitAirplane(col), 0.1f);
        }
    }

    private void HitAirplane(Collider2D collider2D)
    {
        collider2D.gameObject.GetComponent<AirplaneController>().Explode();
        Destroy(gameObject);
    }
}
