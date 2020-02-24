using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ghost : MonoBehaviour
{
    public int x;
    public int y;
    public float moveSpeed = 1f;

    public bool isMoving = false;

    private void Update()
    {
       // HandlePlayerInput();
    }

    private void HandlePlayerInput()
    {
        if (!InputManager.Instance.isMoveInputEnabled || !LevelManager.Instance.isGhostCanMove)
        {
            return;
        }
        
        if (InputManager.Instance.right)
        {
            MovePlayer(x-1,y);
        }
    }

    public void MovePlayer(int x,int y)
    {
        if(isMoving)
        {
            return;
        }
        
        this.x = x;
        this.y = y;
        StartCoroutine(MovePlayerCoroutine());
    }

    IEnumerator MovePlayerCoroutine()
    {
        isMoving = true;
        Vector3  endPosition= new Vector3(x,y,0);
        bool reachedDestination = false;
        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, endPosition) < 0.05f)
            {
                reachedDestination = true;
                break;
            }
            float step =  moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            yield return null;
        }

        transform.position = new Vector3(x, y, 0);
        isMoving = false;
        yield return null;
    }

    public void Init(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
    
        if (other.gameObject.tag == "MovingObs")
        {
            Destroy(gameObject);
            LevelManager.Instance.LoseGame();
        }
    }
}
