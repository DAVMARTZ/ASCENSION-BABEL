using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Stats/EnemyStats")]

public class EnemyStats : ScriptableObject
{
    [Header("Combate")]
    public int daño = 10;
    public float attackDistance = 2.5f;
    public float attackCooldown = 0.6f;

    [Header("Detección")]
    public float followDistance = 6f;

    [Header("Movimiento")]
    public float speed = 2f;

    [Header("Vida")]
    public int vidaMaxima = 50;
}
