using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField]
    GameObject m_Menu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void ReloadGame()
    {
        SceneManager.LoadScene("Menu");
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
