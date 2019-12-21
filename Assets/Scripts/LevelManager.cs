using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class LevelManager : Singleton<LevelManager>
{
   

    public bool isGhostCanMove = true;
    public bool isPlayerCanMove = true;

    public bool isInGhostMode = false;
    public bool isGridMovesForPlayer = false;
    public bool isGridMovesForGhost = false;

    public Board board;


    public void SetupCameraMechanics()
    {
        if (isGridMovesForGhost)
        {
            Camera.main.transform.parent = board.m_ghost.gameObject.transform;
        }

        else if (isGridMovesForPlayer)
        {
            Camera.main.transform.parent = board.m_player.gameObject.transform;
        }
    }
}
