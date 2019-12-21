using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public bool isMoveInputEnabled = true;

    public bool right = false;
    public bool left = false;
    public bool up = false;
    public bool down = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            right = true;
            LevelManager.Instance.board.MovePlayers(Vector2.right);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            left = true;
            LevelManager.Instance.board.MovePlayers(Vector2.left);

        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            up = true;
            LevelManager.Instance.board.MovePlayers(Vector2.up);

        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            down = true;
            LevelManager.Instance.board.MovePlayers(Vector2.down);

        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            LevelManager.Instance.isInGhostMode = !LevelManager.Instance.isInGhostMode;
            LevelManager.Instance.board.ChangeGridMode();
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
