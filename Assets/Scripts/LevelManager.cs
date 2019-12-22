using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class LevelManager : Singleton<LevelManager>
{

    public int currentLevel = 0;
    public int moveCount = 0;
    public bool isGhostCanMove = true;
    public bool isPlayerCanMove = true;

    public bool isInGhostMode = false;
    public bool isGridMovesForPlayer = false;
    public bool isGridMovesForGhost = false;

    public Board board;

    public bool isGameEnded = false;
    public GameObject winMenu;

    private void Start()
    {
        GameManager.Instance.nextLevel = currentLevel + 1;
        moveCount = 0;
    }

    private void Update()
    {
        if (isGameEnded)
        {
            StartCoroutine(waitForWin());
            if (currentLevel >= GameManager.Instance.lastOpenLevel)
            {
                GameManager.Instance.lastOpenLevel = currentLevel + 1;
            }
        }
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
