using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MovingObsParams
{
    public Vector3 startPos;
    public Vector3 endPos;
    public float speed;
    public float delay;
}
public class MovingObsManager : MonoBehaviour
{
    public GameObject movingObsPrefab;
    public List<MovingObsParams> movingObsParams;

    public void Start()
    {
        foreach (MovingObsParams prms in movingObsParams)
        {
            GameObject go = Instantiate(movingObsPrefab);
            MovingObs goScritp = go.GetComponent<MovingObs>();

            goScritp.startPos = prms.startPos;
            goScritp.endPos = prms.endPos;
            goScritp.speed = prms.speed;
            goScritp.delay = prms.delay;

            goScritp.Move();
        }
    }

}
