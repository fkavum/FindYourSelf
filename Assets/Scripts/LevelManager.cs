using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

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
    public LevelBgCanvas LevelBgCanvas;
    public LevelCanvas levelCanvas;
    private void Start()
    {
        GameManager.Instance.nextLevel = currentLevel + 1;
        currentMovesDone = 0;
        LevelBgCanvas.SetMovesDone(currentMovesDone);
    }

    private void Update()
    {
        if (isGameEnded)
        {
            int stars = CalculateStars();
            levelCanvas.ActivateStars(stars);
            StartCoroutine(waitForWin());
            if (currentLevel >= GameManager.Instance.lastOpenLevel)
            {
                GameManager.Instance.lastOpenLevel = currentLevel + 1;
            }
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

    public void UpdateMovesDoneCounters()
    {
        currentMovesDone++;
        LevelBgCanvas.SetMovesDone(currentMovesDone);
    }

    IEnumerator waitForWin()
    {
        yield return new WaitForSeconds(2.5f);
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
