using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject PausePanel;
    public ButtonSound buttonSound;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            buttonSound.PlaySound();
        }
    }
    public void MenuButton() { SceneManager.LoadScene(0); }
    public void Exit() { Application.Quit(); }
    public void PauseButton() { Time.timeScale = 0f; }
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

}
