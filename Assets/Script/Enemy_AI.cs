using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    #region componentes
    private Rigidbody2D rb;
    public Transform player;
    private Animator anim;
    private SpriteRenderer sr;
    #endregion
    #region variables
    float direction;
    private float attackDistance;
    private float followDistance;
    private float nextAttackTime = 0f;
    [SerializeField]
    private float attackRate = 1f;
    [SerializeField]
    private int attackDamage = 10;
    private bool isDead = false;
    #endregion
    #region stats
    [SerializeField]
    private EnemyStats enemyStats;
    #endregion

    public enum EnemyState
    {
        patrol,
        follow,
        shoot
    }
    private EnemyState state = EnemyState.patrol;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponentInChildren<SpriteRenderer>();
        attackDistance = enemyStats.attackDistance;
        followDistance = enemyStats.followDistance;
        direction = 1f;
    }

    private void Update()
    {
        if (isDead) return;
        UpdateState();
        UpdateBehaviour();
    }

    private void UpdateBehaviour()
    {
        switch (state)
        {
            //Patrullar
            case EnemyState.patrol:
                rb.linearVelocity = new Vector2(direction * enemyStats.speed, rb.linearVelocity.y);

                anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
                break;

            //Seguir
            case EnemyState.follow:

                if (player.position.x > transform.position.x)
                    direction = 1f;
                else
                    direction = -1f;

                rb.linearVelocity = new Vector2(direction * enemyStats.speed, rb.linearVelocity.y);

                anim.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
                break;

            //Atacar
            case EnemyState.shoot:
                if (player.position.x > transform.position.x)
                    direction = 1f;
                else
                    direction = -1f;

                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

                anim.SetFloat("Speed", 0);

                if (Time.time >= nextAttackTime)
                {
                    Debug.Log("¡Intentando atacar al jugador!");
                    anim.SetTrigger("Attack");
                    AttackPlayer();
                    nextAttackTime = Time.time + attackRate;
                }
                break;
        }
        sr.flipX = (direction < 0);
    }

    private void UpdateState()
    {
        if (player == null)
        {
            state = EnemyState.patrol;
            return;
        }

        float distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < enemyStats.attackDistance)
        {
            state = EnemyState.shoot;
            return;
        }

        if (distance < enemyStats.followDistance)
        {
            state = EnemyState.follow;
            return;
        }

        state = EnemyState.patrol;
    }

    private void AttackPlayer()
    {
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
        }

        Debug.Log("¡Atacando al jugador!");
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            direction *= -1f;
        }
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;

        anim.SetBool("Dead", true);    
        rb.linearVelocity = Vector2.zero;  
    }


}
