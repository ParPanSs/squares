using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Player deadEnemy;

    private void Start()
    {
        deadEnemy = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            deadEnemy.DeadEnemyCounter();
            Destroy(gameObject);
        }
    }
}
