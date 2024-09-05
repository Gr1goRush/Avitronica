using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterJetControl : ExplodingObject
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private float ForceAmount, moveMultiplier = 0.6f, minMoveSpeed = 1f;

    [Space(10)]
    [Header("Shooting")]
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private JetBomb BombPrefab;
    [SerializeField] private List<Transform> GunsList;
    [SerializeField] private float ShootingInterval;
    [SerializeField] private GameObject BulletRoot, BombSocket;
    [SerializeField] private List<Transform> ParticalPos;
    [SerializeField] private GameObject ParticleShootPrefab;
    [SerializeField] private EnemyShootTrigger enemyShootTrigger;
    [SerializeField] private EnemyBombDropTrigger enemyBombDropTrigger;

    [Space(10)]
    [Header("Health")]
    public int Health;

    [SerializeField] private Sprite HelmetSprite, HelmetBrokenSprite;
    [SerializeField] private Image[] HealthBar;

    private Vector3 startLocalPosition;
    private float lastMoveSpeed;
    private Vector3 targetPosition;

    private int indexNextGun;


    void Start()
    {
        startLocalPosition = transform.localPosition;
        targetPosition = startLocalPosition;

        StartCoroutine(OnShooting(ShootingInterval));
        enemyBombDropTrigger.OnTankTriggered += OnTankTriggered;

        GameUIController.Instance.MovePointer.OnWorldOffsetChanged += MovePointer_OnWorldPositionChanged;
    }

    private void MovePointer_OnWorldPositionChanged(Vector3 offsetFromCenter, float speed)
    {
        if (!PlayerController.isDeath && PlayerController.isStartingGame)
        {
            lastMoveSpeed = speed / Time.deltaTime;
            targetPosition = startLocalPosition + offsetFromCenter;
        }
    }

    void Update()
    {
        for (int i = 0; i < HealthBar.Length; i++)
        {
            if (i + 1 <= Health)
            {
                HealthBar[i].sprite = HelmetSprite;
            }
            else
            {
                HealthBar[i].sprite = HelmetBrokenSprite;
            }

        }

        if (Health <= 0)
        {
            Explode();
        }
    }

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, Mathf.Max(lastMoveSpeed * moveMultiplier, minMoveSpeed) * Time.fixedDeltaTime);    
    }

    private IEnumerator OnShooting(float waitTime)
    {
        while (!PlayerController.isDeath)
        {
            yield return new WaitForSeconds(waitTime);

            if (PlayerController.isStartingGame)
            {
                UpdateShooting();
            }
        }
    }

    private void UpdateShooting()
    {
        if (!enemyShootTrigger.FlyEnemiesTriggered())
        {
            return;
        }

        Transform gun;
        int newGunIndex;

        newGunIndex = indexNextGun < 0 ? GunsList.Count - Mathf.Abs(indexNextGun % GunsList.Count) : indexNextGun % GunsList.Count;

        gun = GunsList[newGunIndex];

        indexNextGun += 1;

        GameObject bullet = Instantiate(BulletPrefab);
        bullet.transform.SetParent(BulletRoot.transform);
        bullet.transform.position = gun.position;


        GameObject particle = Instantiate(ParticleShootPrefab);
        particle.transform.SetParent(transform);
        particle.transform.position = ParticalPos[newGunIndex].position;

        AudioController.Instance.Sounds.PlayOneShot("shoot");
    }

    private void OnTankTriggered(EnemyTank enemyTank)
    {
        JetBomb bomb = Instantiate(BombPrefab);
        bomb.triggeredTank = enemyTank;
        bomb.transform.SetParent(BulletRoot.transform);
        bomb.transform.position = BombSocket.transform.position;
    }

    protected override void OnExplodeAnimation()
    {
        gameManager.GameOver();
        base.OnExplodeAnimation();
    }

    private void OnDestroy()
    {
        GameUIController.Instance.MovePointer.OnWorldOffsetChanged -= MovePointer_OnWorldPositionChanged;
    }
}
