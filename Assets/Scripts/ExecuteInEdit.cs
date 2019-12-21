using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ExecuteInEdit : MonoBehaviour
{

    public Board board;
    public GameObject defaultObjectPrefab;
    public GameObject defaultObjectSoulPrefab;
    private int width;

    private int height;
    // Update is called once per frame
    void Update()
    {

        if (board == null)
        {
            Debug.Log("Empty board detected!");
        }
        
        if (board.width == width && board.height == height)
        {
            return;
        }

        width = board.width;
        height = board.height;
        
        board.startingObjects = new Board.StartingObject[width*height];

        int counter = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                
                board.startingObjects[counter] = new Board.StartingObject();
                board.startingObjects[counter].Init(j,i,defaultObjectPrefab,defaultObjectSoulPrefab);
                counter++;
            }
        }

    }
}
