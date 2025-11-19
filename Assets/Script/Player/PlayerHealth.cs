using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private int vidaActual;
    [SerializeField]
    private PlayerStats stats;
    private PlayerMovement playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vidaActual = stats.vidaMaxima;
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int daño)
    {
        vidaActual -= daño;
        if (vidaActual <= 0)
        {
            Debug.Log("Moriste");
            SceneManager.LoadScene("GameOver");
        }
    }

}
