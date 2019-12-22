using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCanvas : MonoBehaviour
{

    public GameObject optionsMenuPanel;
    // Start is called before the first frame update
    public void StartGameButton()
    {
        SceneManager.LoadScene("Level1");
    }
    public void SelectLevelButton()
    {
        SceneManager.LoadScene("SelectLevel");
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
}
