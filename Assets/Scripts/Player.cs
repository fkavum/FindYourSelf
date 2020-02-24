using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int x;
    public int y;
    public float moveSpeed = 1f;
    public GameObject awakenPlayerPrefab;

    public bool isMoving = false;
    private Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ghost")
        {
            Camera.main.transform.parent = null;
            Destroy(other.gameObject);
            Instantiate(awakenPlayerPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            LevelManager.Instance.isGameEnded = true;
            LevelManager.Instance.EndGame();
            SoundManager.Instance.PlayAwakenGridBgMusic();
        }

        if (other.gameObject.tag == "MovingObs")
        {
            Destroy(gameObject);
            LevelManager.Instance.LoseGame();
        }
    }

    public void MovePlayer(int x, int y, Vector2 dir)
    {
        if (isMoving)
        {
            return;
        }

        AnimateWalk(dir);
        this.x = x;
        this.y = y;
        StartCoroutine(MovePlayerCoroutine());
    }

    void AnimateWalk(Vector2 dir)
    {
        if (m_animator == null) return;
        if (dir.y > 0)
        {
            m_animator.SetBool("idle", false);
            m_animator.SetBool("walkUp", true);
        }
        else if (dir.y < 0)
        {
            m_animator.SetBool("idle", false);
            m_animator.SetBool("walkDown", true);

        }
        else if (dir.x > 0)
        {
            m_animator.SetBool("idle", false);
            m_animator.SetBool("walkRight", true);

        }
        else if (dir.x < 0)
        {
            m_animator.SetBool("idle", false);
            m_animator.SetBool("walkLeft", true);

        }
    }

    void AnimateIdle()
    {
        if (m_animator == null) return;
        m_animator.SetBool("walkLeft", false);
        m_animator.SetBool("walkRight", false);
        m_animator.SetBool("walkDown", false);
        m_animator.SetBool("walkUp", false);
        m_animator.SetBool("idle", true);
    }

    IEnumerator MovePlayerCoroutine()
    {
        isMoving = true;
        Vector3 endPosition = new Vector3(x, y, 0);
        bool reachedDestination = false;
        while (!reachedDestination)
        {
            if (Vector3.Distance(transform.position, endPosition) < 0.05f)
            {
                reachedDestination = true;
                break;
            }

            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
            yield return null;
        }

        transform.position = new Vector3(x, y, 0);
        AnimateIdle();
        isMoving = false;
        yield return null;
    }

    public void Init(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}