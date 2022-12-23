using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Health health;
    public Rigidbody2D rb;
    public bool needToGo;
    public float speed;
    public Vector2 worldPos;
    public int deadEnemies;
    
    public int amountEnemies;
    public int amountHealth;
    public TextMeshProUGUI text;
    
    public Vector3 center;
    public Vector3 size;
    public GameObject enemyPrefab;
    public GameObject healthPrefab;
    
    void Start()
    {
        deadEnemies = 0;
        text.enabled = true;
        currentHealth = maxHealth;
        health.SetMaxHealth(maxHealth);
        Spawn();
        text.text = "Enemy killed: ";
    }
  
    void Update()
    {
        text.text = "Enemy killed: " + deadEnemies;
        
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            text.text = "YOU LOSE";
        }

        if (currentHealth > 0 && deadEnemies == amountEnemies)
        {
            Destroy(gameObject);
            text.text = "YOU WIN!";
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 mousePos = Input.mousePosition;
            worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            needToGo = true;
        }
        
        if (needToGo)
        {
            rb.MovePosition(Vector2.MoveTowards(transform.position,worldPos,speed * Time.deltaTime));

            if (Vector2.Distance(transform.position, worldPos) < 0.01)
            {
                needToGo = false;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        health.SetHealth(currentHealth);
    }
    public void Heal(int heal)
    {
        currentHealth += heal;
        health.SetHealth(currentHealth);
    }

    public void DeadEnemyCounter()
    {
        deadEnemies += 1;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(25);
            Destroy(other.gameObject);
            DeadEnemyCounter();
        }

        if (other.CompareTag("Health") && currentHealth < 100)
        {
            Heal(25);
            Destroy(other.gameObject);
        }
    }

    void Spawn()
    {
        for (int i = 0; i < amountEnemies; i++)
        {
            Vector2 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                Random.Range(-size.y / 2, size.y / 2));
            Instantiate(enemyPrefab, pos, Quaternion.identity);
        }

        for (int i = 0; i < amountHealth; i++)
        {
            Vector2 pos2 = center + new Vector3(Random.Range(-size.x / 2, size.x / 2),
                Random.Range(-size.y / 2, size.y / 2));
            Instantiate(healthPrefab, pos2, Quaternion.identity);
        }
    }
}
