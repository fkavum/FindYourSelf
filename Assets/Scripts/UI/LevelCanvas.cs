using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCanvas : MonoBehaviour
{
    public GameObject optionsMenuPanel;

    public GameObject pauseMenuPanel;

    public GameObject[] stars;
    
    // Start is called before the first frame update
    public void NextLevelButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + GameManager.Instance.nextLevel.ToString());
    }

    public void ActivateStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }

    public void OptionsOpenButton()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void OptionsCloseButton()
    {
        optionsMenuPanel.SetActive(false);
    }

    public void ResumeGameButton()
    {
        pauseMenuPanel.SetActive(false);
        InputManager.Instance.isMoveInputEnabled = true;
        Time.timeScale = 1f;
    }

    public void GoSelectLevelButton()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene("SelectLevel");
    }

    public void GoMainMenuButton()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlayMenuMusic();
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevelButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}