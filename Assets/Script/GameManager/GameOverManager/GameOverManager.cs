using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_Menu;
    [SerializeField]
    GameObject m_Reload;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void ReloadGame()
    {
        SceneManager.LoadScene("Game_Level_1");
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
