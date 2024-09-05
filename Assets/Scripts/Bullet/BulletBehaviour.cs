using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float LifeTime;
    [SerializeField] private float DamageMin, DamageMax;
    [SerializeField] private float direction = 1f;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(AutoDestroy(LifeTime));
    }


    private void FixedUpdate()
    {
        rb.velocity = direction * Speed * Time.deltaTime * transform.up;
    }    

    public float GetDamage()
    {
        return Random.Range(DamageMin, DamageMax);
    }

    private IEnumerator AutoDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
