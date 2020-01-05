using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject playerPrefab;
    public GameObject ghostPrefab;
    public Vector3 FindMovablePlacePos;

    public Vector2 playerStartingPosition = new Vector2(0f,0f);
    public Vector2 soulStartingPosition = new Vector2(1f,1f);
    
    
    public int width;
    public int height;
    
    private Tile[,] m_allTiles;
    private TileEntity[,] m_allTileObjects;
    private TileEntity[,] m_allTileObjectsSoul;
    
    [HideInInspector]
    public Player m_player;
    [HideInInspector]
    public Ghost m_ghost;

    public StartingObject[] startingObjects;
    private BoardGenerator m_boardGenerator;

    [System.Serializable]
    public class StartingObject
    {
        public GameObject tileEntityPrefab;
        public GameObject tileEntityPrefabSoul;
        public int x;
        public int y;

        public void Init(int x, int y, GameObject tileEntityPrefab = null,GameObject tileEntityPrefabSoul = null)
        {
            this.x = x;
            this.y = y;
            this.tileEntityPrefab = tileEntityPrefab;
            this.tileEntityPrefabSoul = tileEntityPrefabSoul;
        }
    }

    void Start()
    {

        m_boardGenerator = gameObject.GetComponent<BoardGenerator>();
        m_boardGenerator.ParseCsv();
        m_boardGenerator.SetVariables(this);
        m_allTiles = new Tile[width, height];
        m_allTileObjects = new TileEntity[width, height];
        m_allTileObjectsSoul = new TileEntity[width, height];
        // sets up any manually placed Tiles
        SetupTiles();
        SetupTileEntities();
        ChangeGridMode();
        InitPlayer();
        LevelManager.Instance.SetupCameraMechanics();
    }

    private void Update()
    {
        /*
        Tile tile = null;
        if (InputManager.Instance.right)
        {
            tile = MovablePlaceAtWithDirection((int)FindMovablePlacePos.x,(int)FindMovablePlacePos.y, Vector2.right);
            if (tile == null)
            {
                Debug.Log("No possible move.");
            }
            else
            {
                Debug.Log("Movable Position Find at: " +  tile.xIndex + ", " + tile.yIndex);

            }
        }
        if (InputManager.Instance.left)
        {
            tile = MovablePlaceAtWithDirection((int)FindMovablePlacePos.x,(int)FindMovablePlacePos.y, Vector2.left);
            if (tile == null)
            {
                Debug.Log("No possible move.");
            }
            else
            {
                Debug.Log("Movable Position Find at: " +  tile.xIndex + ", " + tile.yIndex);

            }
        }
        if (InputManager.Instance.up)
        {
            tile =  MovablePlaceAtWithDirection((int)FindMovablePlacePos.x,(int)FindMovablePlacePos.y, Vector2.up);
            if (tile == null)
            {
                Debug.Log("No possible move.");
            }
            else
            {
                Debug.Log("Movable Position Find at: " +  tile.xIndex + ", " + tile.yIndex);

            }
        }
        if (InputManager.Instance.down)
        {
            tile = MovablePlaceAtWithDirection((int)FindMovablePlacePos.x,(int)FindMovablePlacePos.y, Vector2.down);
            if (tile == null)
            {
                Debug.Log("No possible move.");
            }
            else
            {
                Debug.Log("Movable Position Find at: " +  tile.xIndex + ", " + tile.yIndex);

            }
        }*/
    }

    private void SetupTileEntities()
    {
        foreach (StartingObject startingObject in startingObjects)
        {
            MakeTileEntity(startingObject.tileEntityPrefab,startingObject.tileEntityPrefabSoul, startingObject.x, startingObject.y);
        }
    }

    private void MakeTileEntity(GameObject prefab,GameObject prefabSoul, int x, int y, int z = 0)
    {
        if (prefab != null && IsWithinBounds(x, y))
        {
            // create a Tile at position (x,y,z) with no rotations; rename the Tile and parent it 

            // to the Board, then initialize the Tile into the m_allTiles array

            GameObject entity = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            entity.name = "Entity (" + x + "," + y + ")";
            m_allTileObjects[x, y] = entity.GetComponent<TileEntity>();
            entity.transform.parent = transform;
            m_allTileObjects[x, y].Init(x, y, this);
            
            GameObject tileSoul = Instantiate(prefabSoul, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            tileSoul.name = "EntitySoul (" + x + "," + y + ")";
            m_allTileObjectsSoul[x, y] = tileSoul.GetComponent<TileEntity>();
            tileSoul.transform.parent = transform;
            m_allTileObjectsSoul[x, y].Init(x, y, this);
            tileSoul.SetActive(false);
        }
    }

    void SetupTiles()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (m_allTiles[i, j] == null)
                {
                    MakeTile(tilePrefab, i, j);
                }
            }
        }
    }

    void InitPlayer()
    {
        GameObject playerObj = Instantiate(playerPrefab, new Vector3((int) playerStartingPosition.x, (int) playerStartingPosition.y, 0), Quaternion.identity) as GameObject;
        playerObj.name = "Player";
        m_player = playerObj.GetComponent<Player>();
        m_player.Init((int) playerStartingPosition.x, (int) playerStartingPosition.y);


        GameObject ghostObj =
            Instantiate(ghostPrefab, new Vector3((int) soulStartingPosition.x, (int) soulStartingPosition.y, 0), Quaternion.identity) as GameObject;
        ghostObj.name = "Ghost";
        m_ghost = ghostObj.GetComponent<Ghost>();
        m_ghost.Init((int) soulStartingPosition.x, (int) soulStartingPosition.y);
    }

    void MakeTile(GameObject prefab, int x, int y, int z = 0)
    {
        // only run the logic on valid GameObject and if we are within the boundaries of the Board
        if (prefab != null && IsWithinBounds(x, y))
        {
            // create a Tile at position (x,y,z) with no rotations; rename the Tile and parent it 

            // to the Board, then initialize the Tile into the m_allTiles array

            GameObject tile = Instantiate(prefab, new Vector3(x, y, z), Quaternion.identity) as GameObject;
            tile.name = "Tile (" + x + "," + y + ")";
            m_allTiles[x, y] = tile.GetComponent<Tile>();
            tile.transform.parent = transform;
            m_allTiles[x, y].Init(x, y, this);
        }
    }

    public void MovePlayers(Vector2 playerDir)
    {
        if (!InputManager.Instance.isMoveInputEnabled || m_ghost.isMoving || m_player.isMoving )
        {
            return;
        }


        if (LevelManager.Instance.isInGhostMode)
        {
            playerDir = playerDir * -1f;
        }
        
        if (LevelManager.Instance.isPlayerCanMove)
        {
            Tile playerTile = MovablePlaceAtWithDirection(m_player.x, m_player.y, playerDir);
            if (playerTile != null)
            {
                m_player.MovePlayer(playerTile.xIndex,playerTile.yIndex,playerDir);
            }
        }
        if (LevelManager.Instance.isGhostCanMove)
        {
            Tile playerTile = MovablePlaceAtWithDirection(m_ghost.x, m_ghost.y, playerDir*-1f);
            if (playerTile != null)
            {
                m_ghost.MovePlayer(playerTile.xIndex,playerTile.yIndex);
            }
        }
    }

    bool IsWithinBounds(int x, int y)
    {
        return (x >= 0 && x < width && y >= 0 && y < height);
    }

    // Return null if there is no place to move.
    Tile MovablePlaceAtWithDirection(int startX, int startY, Vector2 dir)
    {
        if (dir.y > 0)
        {
            // Up direction - X constant Y incremented.
            if (startY + 1 >= height)
            {
                return null;
            }

            for (int i = startY + 1; i < height; i++)
            {
                if (m_allTileObjects[startX, i].entityType == EntityType.Obstacle)
                {
                    if (startY == i - 1)
                    {
                        return null;
                    }

                    return m_allTiles[startX, i - 1];
                }
            }

            return m_allTiles[startX, height-1];
        }
        else if (dir.y < 0)
        {
            // Down Direction - X constant Y decreased
            if (startY <= 0)
            {
                return null;
            }

            for (int i = startY - 1; i >= 0; i--)
            {
                if (m_allTileObjects[startX, i].entityType == EntityType.Obstacle)
                {
                    if (startY == i + 1)
                    {
                        return null;
                    }

                    return m_allTiles[startX, i + 1];
                }
            }

            return m_allTiles[startX, 0];
        }
        else if (dir.x > 0)
        {
            // Right Direction - Y constant X increased
            if (startX + 1 >= height)
            {
                return null;
            }

            for (int i = startX + 1; i < height; i++)
            {
                if (m_allTileObjects[i, startY].entityType == EntityType.Obstacle)
                {
                    if (startX == i - 1)
                    {
                        return null;
                    }

                    return m_allTiles[i - 1, startY];
                }
            }
            Debug.Log(width + ", " + startY);
            return m_allTiles[width -1, startY];
        }
        else if (dir.x < 0)
        {
            // Left Direction - Y constant X decreased
            if (startX <= 0)
            {
                return null;
            }

            for (int i = startX - 1; i >= 0; i--)
            {
                if (m_allTileObjects[i, startY].entityType == EntityType.Obstacle)
                {
                    if (startX == i + 1)
                    {
                        return null;
                    }

                    return m_allTiles[ i + 1,startY];
                }
            }

            return m_allTiles[0, startY];
        }

        return null;
    }

    public void ChangeGridMode()
    {
    
        if (LevelManager.Instance.isInGhostMode)
        {
            SoundManager.Instance.PlayGhostGridBgMusic();
        }
        else
        {
            SoundManager.Instance.PlayPlayerGridBgMusic();
        }
        
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                m_allTileObjects[i,j].gameObject.SetActive(!LevelManager.Instance.isInGhostMode);
                m_allTileObjectsSoul[i,j].gameObject.SetActive(LevelManager.Instance.isInGhostMode);
            }
        }
    }
}