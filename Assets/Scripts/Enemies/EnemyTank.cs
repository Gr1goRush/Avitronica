using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : BaseEnemyBehaviour
{
    [SerializeField] private GameObject _particlePrefab, particleSpawnPoint;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered || exploded)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Airplane"))
        {
            triggered = true;
            Shoot();
        }
    }

    private void Shoot()
    {
        _animator.SetTrigger("Shoot");
    }

    public void SpawnShootParticle()
    {
        if(exploded)
        {
            return;
        }

       GameObject particle = Instantiate(_particlePrefab);
        particle.transform.position = particleSpawnPoint.transform.position;
        particle.transform.rotation = particleSpawnPoint.transform.rotation;
    }
}
