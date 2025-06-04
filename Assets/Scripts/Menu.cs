using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private ButtonSound buttonSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButton();
        }
    }

    public void MenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void PauseButton()
    {
        if (PausePanel.activeSelf)
        {
            Time.timeScale = 1f;
            PausePanel.SetActive(false);
            buttonSound.PlaySound();
        }
        else
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            buttonSound.PlaySound();
        }
    }

    public void StartButton()
    {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }

    public void Tutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}