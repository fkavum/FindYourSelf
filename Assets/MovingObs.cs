using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObs : MonoBehaviour
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;

    public float delay;

    public void Move()
    {
        transform.position = startPos;
        StartCoroutine(MovePlayerCoroutine());
    }

    IEnumerator MovePlayerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            Vector3 endPosition = endPos;
            bool reachedDestination = false;
            while (!reachedDestination)
            {
                if (Vector3.Distance(transform.position, endPosition) < 0.05f)
                {
                    reachedDestination = true;
                    break;
                }
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
                yield return null;
            }

            transform.position = endPos;
            yield return null;
            yield return new WaitForSeconds(delay);
            endPosition = startPos;
            reachedDestination = false;
            while (!reachedDestination)
            {
                if (Vector3.Distance(transform.position, endPosition) < 0.05f)
                {
                    reachedDestination = true;
                    break;
                }
                float step = speed * Time.deltaTime; // calculate distance to move
                transform.position = Vector3.MoveTowards(transform.position, endPosition, step);
                yield return null;
            }

            transform.position = startPos;
            yield return null;
        }
       
    }

}
