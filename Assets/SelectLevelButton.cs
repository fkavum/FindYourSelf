using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelButton : MonoBehaviour
{

    public GameObject star1;
    public GameObject star2;
    public GameObject star3;
    public Text lockText;
    public Text levelText;

    private int buttonLevel;
    void Start()
    {

        buttonLevel = Convert.ToInt32(this.gameObject.name); //this.gameObject.name
        levelText.text = this.gameObject.name;
        if(buttonLevel > GameManager.Instance.lastOpenLevel)
        {
            lockText.gameObject.SetActive(true);
            star1.SetActive(false);
            star2.SetActive(false);
            star3.SetActive(false);
        }
        else
        {
            lockText.gameObject.SetActive(false);
            string playerPref = "level" + this.gameObject.name + "Star";

            switch (PlayerPrefs.GetInt(playerPref))
            {
                case 0:
                    star1.SetActive(false);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    break;
                case 1:
                    star1.SetActive(true);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    break;
                case 2:
                    star1.SetActive(true);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    break;
                case 3:
                    star1.SetActive(true);
                    star2.SetActive(false);
                    star3.SetActive(false);
                    break;
            }


        }



    }

   public void GoNextLevel()
    {
        if(buttonLevel > GameManager.Instance.lastOpenLevel)
        {
            return;
        }

        SceneManager.LoadScene("Level" + buttonLevel.ToString());
    }
}
