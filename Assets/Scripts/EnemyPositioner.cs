using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPositioner : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;


    [Space(20)]
    [SerializeField] private Vector2 PosMin;
    [SerializeField] private Vector2 PosMax;


    void Start()
    {
        float posX = Random.Range(PosMin.x, PosMax.x);
        float posY = Random.Range(PosMin.y, PosMax.y);

        Vector2 enemyPos = new Vector2(posX, posY);

        Enemy.transform.localPosition = enemyPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 1.5f);
        Gizmos.DrawSphere(PosMin, 0.1f);
        Gizmos.DrawLine(PosMin, new Vector2(PosMin.x, PosMax.y));

        Gizmos.color = new Color(0, 0, 1, 1.5f);
        Gizmos.DrawSphere(PosMax, 0.1f);
        Gizmos.DrawLine(PosMax, new Vector2(PosMax.x, PosMin.y));
    }
}
