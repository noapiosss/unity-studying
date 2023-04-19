using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float maxHealth = 10f;
    private float currentHealth;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private new Collider2D collider2D;
    private Animator anim;
    private float deadDelay = 0;
    public GameObject deadEnemy;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth > 0)
        {
            MoveOnPlayer();
            RotateOnPlayer();
        }
        else
        {
            deadDelay += Time.deltaTime;
            anim.SetBool("isDead", true);
            
            if (deadDelay > 1)
            {
                gameObject.SetActive(false);
                Instantiate(deadEnemy, transform.position, transform.rotation);

                currentHealth = maxHealth;
                anim.SetBool("isDead", false);
                deadDelay = 0;
            } 
        }
    }

    private void MoveOnPlayer()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 direction = Vector3.Normalize(playerPosition - transform.position);
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void RotateOnPlayer()
    {
        Vector3 playerPosition = GameObject.Find("Player").transform.position;
        Vector3 direction = Vector3.Normalize(playerPosition - transform.position);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && currentHealth > 0)
        {
            currentHealth -= 2;
        }
    }

}
