using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelCanvas : MonoBehaviour
{
    public GameObject optionsMenuPanel;
    // Start is called before the first frame update
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
        SceneManager.LoadScene("SampleScene");
    }
}
