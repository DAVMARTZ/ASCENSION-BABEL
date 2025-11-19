using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Stats/PlayerStats")]

public class PlayerStats : ScriptableObject
{
    [Header("Vitales")]
    public int vidaMaxima = 100;
    public int defensa = 0;

    [Header("Daño e invulnerabilidad")]
    public int dañoBase = 10;
    public float invulnerabilidadTiempo = 0.5f;

    [Header("Respawn / Física")]
    public Vector2 respawnPosition = Vector2.zero;
    public float knockbackForce = 5f;
}
