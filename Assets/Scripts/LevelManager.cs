using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
   

    public bool isGhostCanMove = true;
    public bool isPlayerCanMove = true;

    public bool isInGhostMode = false;
    public bool isGridMoves = false;

    public Board board;

}
