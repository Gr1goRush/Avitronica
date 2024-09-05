using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBehaviour : BaseEnemyBehaviour
{

    [SerializeField] private float Speed;
    [SerializeField] private float Health;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Health <= 0)
        {
            //switch (gameObject.tag)
            //{
            //    case "EnemyBirds":
            //        gameManager.MedalsCount += 50;
            //        break;
            //    case "EnemyPlane":
            //        gameManager.MedalsCount += 150;
            //        break;
            //    case "EnemyTank":
            //        gameManager.MedalsCount += 100;
            //        break;

            //    default:
            //        break;
            //}

            Explode();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = Speed * Time.deltaTime * Vector2.down;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("AirplaneBullet"))
        {
            var bullet = col.GetComponent<BulletBehaviour>();

            Health -= bullet.GetDamage();

            return;
        }

        if(col.CompareTag("Airplane"))
        {
            GameManager.Instance.AirplaneController.Explode();
            Destroy(gameObject);
            return;
        }

        if (col.CompareTag("PlayerJet"))
        {
            col.GetComponent<FighterJetControl>().Health -= 1;
        }
    }
}
