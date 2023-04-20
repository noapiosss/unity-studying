using UnityEngine;

public class Enemies : MonoBehaviour
{
    public Enemy Creature;
    private float spawnColdown = 1f;
    private float timeSinceLastSpawn = 0;
    private int maxEnemiesCount = 20;
    
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn > spawnColdown)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        Vector3 relativePosition = new Vector3(Random.value-0.5f, Random.value-0.5f, 0) * 40;

        GameObject enemy = EnemiesPool.SharedInstance.GetPooledObject();
        if (enemy != null)
        {
            enemy.transform.position = GameObject.Find("Player").transform.position + relativePosition;
            enemy.transform.rotation = transform.rotation;
            enemy.SetActive(true);
        }
        
        timeSinceLastSpawn = 0;
    }
}
