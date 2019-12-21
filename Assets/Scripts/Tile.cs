using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    
    // x and y position in the array
    public int xIndex;
    public int yIndex;

    // reference to our Board
    Board m_board;
    
    // the Sprite for this Tile
    SpriteRenderer m_spriteRenderer;
    
    void Awake () 
    {
        // initialize our SpriteRenderer
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    public void Init(int x, int y, Board board)
    {
        xIndex = x;
        yIndex = y;
        m_board = board;
    }
}
