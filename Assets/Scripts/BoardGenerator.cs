using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public TextAsset csvLevel;

    public GameObject O1Normal;
    public GameObject O1Soul;
    
    public GameObject R1Normal;
    public GameObject R1Soul;

// Will be filled when parsing is done.
    private List<Board.StartingObject> startingObjects;

    private bool isGhostCanMove = false;
    private bool isPlayerCanMove = false;

    private bool isInGhostMode = false;
    private bool isGridMovesForPlayer = false;
    private bool isGridMovesForGhost = false;

    private int oneStarMove;
    private int twoStarMove;
    private int threeStarMove;
    
    private int boardWidth;
    private int boardHeight;

    private Vector2 playerStartingPos;
    private Vector2 soulStartingPos;

    public void ParseCsv()
    {
        startingObjects = new List<Board.StartingObject>();
        string[] csvString = csvLevel.text.Split("\n"[0]);
        for (int i = 0; i < csvString.Length ; i++)
        {
            csvString[i] = csvString[i].TrimEnd('\r');
        }
        ParseHeader1(csvString[0]);
        ParseHeader2(csvString[1]);
        ParseGrid(csvString);
    }

    public void SetVariables(Board m_board)
    {
        m_board.width = boardWidth;
        m_board.height = boardHeight;

        m_board.startingObjects = startingObjects.ToArray();
        m_board.playerStartingPosition = playerStartingPos;
        m_board.soulStartingPosition = soulStartingPos;

        LevelManager.Instance.isInGhostMode = isInGhostMode;
        LevelManager.Instance.isGhostCanMove = isGhostCanMove;
        LevelManager.Instance.isPlayerCanMove = isPlayerCanMove;
        LevelManager.Instance.isGridMovesForPlayer = isGridMovesForPlayer;
        LevelManager.Instance.isGridMovesForGhost = isGridMovesForGhost;
    }

    private void ParseHeader1(string s)
    {
        string[] header = s.Split(";"[0]);

        if (header[0] == "1")
        {
            isPlayerCanMove = true;
        }
        if (header[1] == "1")
        {
            isGhostCanMove = true;
        }
        if (header[2] == "1")
        {
            isInGhostMode = true;
        }
        if (header[3] == "1")
        {
            if (isInGhostMode)
            {
                isGridMovesForGhost = true;
            }
            else
            {
                isGridMovesForPlayer = true;
            }
        }

        oneStarMove = Convert.ToInt32(header[4]);
        twoStarMove = Convert.ToInt32(header[5]);
        threeStarMove = Convert.ToInt32(header[6]);

    }
    
    private void ParseHeader2(string s)
    {
        string[] header = s.Split(";"[0]);
        
        string[] playerPos = header[0].Split("-"[0]);
        playerStartingPos = new Vector2(Convert.ToInt32(playerPos[0]),Convert.ToInt32(playerPos[1]));
        string[] soulPos = header[1].Split("-"[0]);
        soulStartingPos = new Vector2(Convert.ToInt32(soulPos[0]),Convert.ToInt32(soulPos[1]));
        string[] boarBounds = header[2].Split("-"[0]);

        boardWidth = Convert.ToInt32(boarBounds[0]);
        boardHeight = Convert.ToInt32(boarBounds[1]);

    }
    private void ParseGrid(string[] csvString)
    {

        int heightIndex = boardHeight - 1;
        
        for (int i = 2; i < csvString.Length; i++)
        {
            string[] columns = csvString[i].Split(";"[0]);
            for (int j = 0; j < columns.Length; j++)
            {
                
                Board.StartingObject temObj = new Board.StartingObject();
                temObj.Init(j,heightIndex,XrefForNormalTileObj(columns[j]),XrefForSoulTileObj(columns[j]));
                
                startingObjects.Add(temObj);
            }

            heightIndex--;
        }
    }

    GameObject XrefForNormalTileObj(string s)
    {

        if (s == "O1")
        {
            return O1Normal;
        }

        if (s == "R1")
        {
            return R1Normal;
        }

        return null;
    }

    GameObject XrefForSoulTileObj(string s)
    {
        if (s == "O1")
        {
            return O1Soul;
        }

        if (s == "R1")
        {
            return R1Soul;
        }


        return null;
    }
}
