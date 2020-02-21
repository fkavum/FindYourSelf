using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelBgCanvas : MonoBehaviour
{

    public Text levelInfoText;
    public Text movesDoneText;
    
    void Start()
    {
        levelInfoText.text = SceneManager.GetActiveScene().name;
    }

    public void SetMovesDone(int movesDone)
    {
        movesDoneText.text = movesDone.ToString();
    }

}
