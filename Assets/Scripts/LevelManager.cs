using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{

    public int currentLevel = 0;
    public bool isGhostCanMove = true;
    public bool isPlayerCanMove = true;

    public int oneStarMove;
    public int twoStarMove;
    public int threeStarMove;
    
    private int currentMovesDone;

    public bool isInGhostMode = false;
    public bool isGridMovesForPlayer = false;
    public bool isGridMovesForGhost = false;

    public Board board;

    public bool isGameEnded = false;
    public GameObject winMenu;
    public GameObject loseMenu;
    public LevelBgCanvas LevelBgCanvas;
    public LevelCanvas levelCanvas;
    private void Start()
    {
        GameManager.Instance.nextLevel = currentLevel + 1;
        currentMovesDone = 0;
        LevelBgCanvas.SetMovesDone(currentMovesDone);
        InputManager.Instance.canGoNextLevel = false;
    }


    public void EndGame()
    {
        int stars = CalculateStars();
        levelCanvas.ActivateStars(stars);
        string playerPref = "level" + currentLevel.ToString() + "Star";
        PlayerPrefs.SetInt(playerPref, stars);

        StartCoroutine(waitForWin());
        if (currentLevel >= GameManager.Instance.lastOpenLevel)
        {
            GameManager.Instance.lastOpenLevel = currentLevel + 1;
            PlayerPrefs.SetInt("lastOpenLevel", currentLevel + 1);
        }
    }

    private int CalculateStars()
    {
        if (currentMovesDone <= threeStarMove)
        {
            return 3;
        }

        if (currentMovesDone <= twoStarMove)
        {
            return 2;
        }

        if (currentMovesDone <= oneStarMove)
        {
            return 1;
        }

        return 0;
    }

    public void GoNextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level" + GameManager.Instance.nextLevel.ToString());
    }

    internal void LoseGame()
    {
        InputManager.Instance.isMoveInputEnabled = false;
        loseMenu.SetActive(true);
    }

    public void UpdateMovesDoneCounters()
    {
        currentMovesDone++;
        LevelBgCanvas.SetMovesDone(currentMovesDone);
    }

    IEnumerator waitForWin()
    {
        yield return new WaitForSeconds(2f);
        InputManager.Instance.canGoNextLevel = true;
        winMenu.SetActive(true);

    }

    public void SetupCameraMechanics()
    {
        if (isGridMovesForGhost)
        {
            Camera.main.transform.parent = null;

            Camera.main.transform.parent = board.m_ghost.gameObject.transform;
        }

        else if (isGridMovesForPlayer)
        {
            Camera.main.transform.parent = null;
            Camera.main.transform.parent = board.m_player.gameObject.transform;
        }
    }
}
