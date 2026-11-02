using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameManager gameManager;

    public Zombie[] zombiePrefabs;
    public ZombieData[] zombieDatas;
    public Transform[] spawnPoints;

    public float spawnInterval = 3f;
    public int maxZombies = 10;
    

    private readonly List<Zombie> zombies = new List<Zombie>();
    private readonly List<Zombie> zombiePool = new List<Zombie>();

    private readonly Dictionary<Zombie, int> zombieTypes =
        new Dictionary<Zombie, int>();

    private float spawnTimer = 0f;
    private bool isGameOver = false;

    private void Start()
    {
        if (gameManager == null ||
            zombiePrefabs == null ||
            zombieDatas == null ||
            spawnPoints == null ||
            zombiePrefabs.Length == 0 ||
            zombieDatas.Length != zombiePrefabs.Length ||
            spawnPoints.Length == 0)
        {
            enabled = false;
            return;
        }
        CreateZombie();
    }

    private void Update()
    {
        if (isGameOver)
        {
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval && zombies.Count < maxZombies)
        {
            CreateZombie();
            spawnTimer = 0f;
        }
    }

    private void CreateZombie()
    {
        int typeIndex = Random.Range(0, zombiePrefabs.Length);

        Transform point =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        Zombie zombie = zombiePool.Find(
            z => zombieTypes[z] == typeIndex
        );

        if (zombie == null)
        {
            zombie = Instantiate(
                zombiePrefabs[typeIndex],
                point.position,
                point.rotation
            );

            zombie.gameObject.SetActive(false);

            zombieTypes.Add(zombie, typeIndex);

            Zombie createdZombie = zombie;

            zombie.OnDead.AddListener(
                () => OnZombieDead(createdZombie)
            );

            zombie.OnDead.AddListener(
                () => StartCoroutine(CoReturnToPoolZombie(createdZombie))
            );
        }
        else
        {
            zombiePool.Remove(zombie);
        }

        zombie.zombieData = zombieDatas[typeIndex];
        zombie.Setup(zombie.zombieData);

        zombie.transform.SetPositionAndRotation(
            point.position,
            point.rotation
        );

        zombie.gameObject.SetActive(true);
        zombies.Add(zombie);
    }

    private void OnZombieDead(Zombie zombie)
    {
        if (zombies.Remove(zombie))
        {
            if (!isGameOver)
            {
                gameManager.AddScore(50);
            }
        }
    }

    private IEnumerator CoReturnToPoolZombie(Zombie zombie)
    {
        // 좀비가 완전히 가라앉을 때까지 대기
        yield return new WaitUntil(() => zombie.IsSinkComplete);
        

        zombie.gameObject.SetActive(false);
        zombiePool.Add(zombie);
    }

    public void onEndGame()
    {
        isGameOver = true;

        for (int i = zombies.Count - 1; i >= 0; --i)
        {
            if (!zombies[i].IsDead)
            {
                zombies[i].Die();
            }
        }
    }
}