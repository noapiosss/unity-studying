using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private int currentHealth;
    public HealthBar healthBar;
    private int maxHealth = 100;
    private float shootColdown = 0.05f;
    private float timeSinceLastShoot = 0;
    private bool shootIsReady = true;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;
    public Transform LaunchOffset;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth > 0)
        {
            Watch();
            anim.SetBool("isWalking", Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0);

            if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
            {
                Move();
            }

            if (Input.GetButton("Fire1") && shootIsReady)
            {
                Shoot();
            }

            if (!shootIsReady)
            {
                timeSinceLastShoot += Time.deltaTime;
                shootIsReady = timeSinceLastShoot > shootColdown;
            }            
        }
        else
        {
            anim.SetBool("isDead", true);
        }

        
        
    }

    private void Move()
    {
        Vector3 direction = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void Watch()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void Shoot()
    {
        GameObject bullet = BulletsPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = LaunchOffset.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
        }
        shootIsReady = false;
        timeSinceLastShoot = 0;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            currentHealth -= 20;
        }
    }
}
