using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EntityType
{
    Road,
    Obstacle
}
public class TileEntity : MonoBehaviour
{
    public EntityType entityType = EntityType.Road;
    
    // x and y position in the array
    public int xIndex;
    public int yIndex;

    // reference to our Board
    Board m_board;
    
    public void Init(int x, int y, Board board)
    {
        xIndex = x;
        yIndex = y;
        m_board = board;
    }
}
