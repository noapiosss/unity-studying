using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 20f;
    private float maxLifeTime = 1f;
    private float lifeTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += transform.up * speed * Time.deltaTime;
        lifeTime += Time.deltaTime;

        if( lifeTime > maxLifeTime )
        {
            gameObject.SetActive(false);
            lifeTime = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.SetActive(false);
    }
}
