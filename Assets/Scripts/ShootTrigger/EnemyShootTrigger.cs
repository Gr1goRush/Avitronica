using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyShootTrigger : MonoBehaviour
{

    [SerializeField] private string[] flyEnemiesTags;

    private readonly List<Collider2D> enemies = new List<Collider2D>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (flyEnemiesTags.Contains(collision.gameObject.tag))
        {
            enemies.Add(collision);
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (flyEnemiesTags.Contains(collision.gameObject.tag))
        {
            enemies.Remove(collision);
            return;
        }
    }

    public bool FlyEnemiesTriggered()
    {
        return enemies.Count > 0;
    }
}