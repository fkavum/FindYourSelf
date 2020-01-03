using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelCanvas : MonoBehaviour
{
    public GameObject optionsMenuPanel;

    public Button[] levelButtons;
    // Start is called before the first frame update

    public void Start()
    {
        int levelIndex = 1;
        foreach (Button levelButton in levelButtons)
        {
            if (levelIndex <= GameManager.Instance.lastOpenLevel)
            {
                levelButton.interactable = true;
            }
            else
            {
                levelButton.interactable = false;
            }

            levelIndex++;
        }
    }

    public void GoBackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    public void OptionsOpenButton()
    {
        optionsMenuPanel.SetActive(true);
    }
    
    public void OptionsCloseButton()
    {
        optionsMenuPanel.SetActive(false);
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }

    public void SelectLevel1Button()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SelectLevel2Button()
    {
        SceneManager.LoadScene("Level2");
    }
    public void SelectLevel3Button()
    {
        SceneManager.LoadScene("Level3");
    }
    public void SelectLevel4Button()
    {
        SceneManager.LoadScene("Level4");
    }
    public void SelectLevel5Button()
    {
        SceneManager.LoadScene("Level5");
    }
    public void SelectLevel6Button()
    {
        SceneManager.LoadScene("Level6");
    }
    public void SelectLevel7Button()
    {
        SceneManager.LoadScene("Level7");
    }
    public void SelectLevel8Button()
    {
        SceneManager.LoadScene("Level8");
    }
    public void SelectLevel9Button()
    {
        SceneManager.LoadScene("Level9");
    }
    public void SelectLevel10Button()
    {
        SceneManager.LoadScene("Level10");
    }
    public void SelectLevel11Button()
    {
        SceneManager.LoadScene("Level11");
    }
    public void SelectLevel12Button()
    {
        SceneManager.LoadScene("Level12");
    }
    public void SelectLevel13Button()
    {
        SceneManager.LoadScene("Level13");
    }
    public void SelectLevel14Button()
    {
        SceneManager.LoadScene("Level14");
    }
}
