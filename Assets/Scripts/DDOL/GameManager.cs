using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public int lastOpenLevel = 0;
    public int nextLevel = 0;

     void Start()
    {
        
        if (PlayerPrefs.HasKey("lastOpenLevel"))
        {
            lastOpenLevel = PlayerPrefs.GetInt("lastOpenLevel");
        }
        else
        {
            PlayerPrefs.SetInt("lastOpenLevel", 1);
            lastOpenLevel = 1;
        }
    }

}
