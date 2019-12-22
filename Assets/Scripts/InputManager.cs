using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public GameObject pauseMenuPanel;
    public bool isMoveInputEnabled = true;

    public bool right = false;
    public bool left = false;
    public bool up = false;
    public bool down = false;

    private void Update()
    {
        if (isMoveInputEnabled)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                right = true;
                LevelManager.Instance.board.MovePlayers(Vector2.right);
            }

            if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow))
            {
                left = true;
                LevelManager.Instance.board.MovePlayers(Vector2.left);
            }

            if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow))
            {
                up = true;
                LevelManager.Instance.board.MovePlayers(Vector2.up);
            }

            if (Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
            {
                down = true;
                LevelManager.Instance.board.MovePlayers(Vector2.down);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                LevelManager.Instance.isInGhostMode = !LevelManager.Instance.isInGhostMode;
                if (LevelManager.Instance.isGridMovesForGhost || LevelManager.Instance.isGridMovesForPlayer)
                {
                    LevelManager.Instance.isGridMovesForGhost = LevelManager.Instance.isInGhostMode;
                    LevelManager.Instance.isGhostCanMove =  LevelManager.Instance.isInGhostMode;
                    LevelManager.Instance.isGridMovesForPlayer = !LevelManager.Instance.isInGhostMode;
                    LevelManager.Instance.isPlayerCanMove = !LevelManager.Instance.isInGhostMode;
                    LevelManager.Instance.SetupCameraMechanics();
                }
                LevelManager.Instance.board.ChangeGridMode();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
        PauseGame();
            
        }
    }

    public void PauseGame()
    {
        if (pauseMenuPanel.active)
        {
            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1f;
            isMoveInputEnabled = true;
        }
        else
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0f;
            isMoveInputEnabled = false;
        }
    }

    private void LateUpdate()
    {
        right = false;
        left = false;
        up = false;
        down = false;
    }
}