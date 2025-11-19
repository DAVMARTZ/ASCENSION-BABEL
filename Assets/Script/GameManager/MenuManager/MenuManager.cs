using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void StartGame()
    {
        SceneManager.LoadScene("Game_Level_1");
    }

    public void OpenOption()
    {
        m_Menu.SetActive(false);
    }

    public void CloseOption()
    {
        m_Menu?.SetActive(true);
    }
}
