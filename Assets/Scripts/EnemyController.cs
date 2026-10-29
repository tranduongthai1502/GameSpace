using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    private readonly Vector2[] formationOffsets =
    {
        new Vector2(-4.8f, 1.5f),
        new Vector2(-3.2f, 1.5f),
        new Vector2(-1.6f, 1.5f),
        new Vector2(0.0f, 1.5f),
        new Vector2(1.6f, 1.5f),
        new Vector2(3.2f, 1.5f),
        new Vector2(4.8f, 1.5f),
        new Vector2(-3.2f, 0.0f),
        new Vector2(-1.6f, 0.0f),
        new Vector2(0.0f, 0.0f),
        new Vector2(1.6f, 0.0f),
        new Vector2(3.2f, 0.0f),
        new Vector2(-2.4f, -1.5f),
        new Vector2(-0.8f, -1.5f),
        new Vector2(0.8f, -1.5f),
        new Vector2(2.4f, -1.5f)
    };

    void Start()
    {
        SpawnFormation(enemyPrefab);
    }

    public void SpawnFormation(GameObject prefab)
    {
        if (prefab == null)
            return;

        Vector2 targetCenter = new Vector2(0f, 3.5f);

        // Enemy bắt đầu từ phía trên màn hình
        Vector2 startPos = new Vector2(0f, 8f);

        // SPAWN ĐỘI HÌNH
        SpawnTeamWithMovement(
            prefab,
            startPos,
            targetCenter,
            formationOffsets
        );
    }

    // SPAWN TEAM
    void SpawnTeamWithMovement(
        GameObject prefab,
        Vector2 startPos,
        Vector2 targetCenter,
        Vector2[] offsets)
    {
        for (int i = 0; i < offsets.Length; i++)
        {
            Vector2 targetPos = targetCenter + offsets[i];

            GameObject enemy = Instantiate(
                prefab,
                startPos,
                Quaternion.identity
            );

            EnemyMover mover = enemy.AddComponent<EnemyMover>();

            mover.Init(
                startPos,
                targetPos,
                3f
            );
        }
    }
}

// ENEMY MOVER
public class EnemyMover : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 targetPos;
    private float speed;

    private bool isMoving = false;

    private Vector2 idlePosition;
    private float idleTime;
    private float floatSpeed;
    private float floatAmount;

    public void Init(
        Vector2 start,
        Vector2 target,
        float moveSpeed)
    {
        startPos = start;
        targetPos = target;
        speed = moveSpeed;
        transform.position = startPos;
        isMoving = true;

        idleTime = Random.Range(0f, 10f);
        floatSpeed = Random.Range(3f, 5f);
        floatAmount = Random.Range(0.08f, 0.15f);
    }

    void Update()
    {
        // GIAI ĐOẠN 1: ENEMY BAY VÀO ĐỘI HÌNH
        if (isMoving)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );

            // KIỂM TRA ĐÃ ĐẾN VỊ TRÍ CHƯA
            if (Vector2.Distance(transform.position, targetPos) < 0.01f)
            {
                transform.position = targetPos;
                idlePosition = targetPos;

                // KÍCH HOẠT BẮN ĐẠN NGAY KHI VÀO ĐỘI HÌNH
                EnemyShooting shooting = GetComponent<EnemyShooting>();
                if (shooting != null)
                {
                    shooting.EnableShooting(true);
                }

                isMoving = false;
            }

            return;
        }

        // GIAI ĐOẠN 2: ENEMY DAO ĐỘNG NHẸ
        idleTime += Time.deltaTime;
        float yOffset = Mathf.Sin(idleTime * floatSpeed) * floatAmount;

        transform.position = new Vector2(
            idlePosition.x,
            idlePosition.y + yOffset
        );
    }
}
