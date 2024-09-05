using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{        

    [SerializeField] private float SpeedPlayer;
    [SerializeField] private float GetScoreInterval;

    [Space(10)]
    public bool isDeath;
    public bool isStartingGame;
    public GameManager GameManager;

    private Transform Player;
    

    void Start()
    {
        Player = transform;

        StartCoroutine(SetScore());
    }

    
    void Update()
    {
        if (!isDeath && isStartingGame)
        {
            Player.Translate(Vector3.up * SpeedPlayer * Time.deltaTime);

            
        }
    }

    private IEnumerator SetScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(GetScoreInterval);
            if (!isDeath && isStartingGame)
            {
                GameManager.Score += 1;
            }
        }
    }
}
