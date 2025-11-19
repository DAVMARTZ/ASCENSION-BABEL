using UnityEngine;

public class EnemyPlayerDetector : MonoBehaviour
{
    [SerializeField]
    private Enemy_AI enemy;

    private void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.player = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemy.player = null;
        }
    }
}
