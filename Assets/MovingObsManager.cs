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
public class MovingObsManager : Singleton<MovingObsManager>
{
    public GameObject movingObsPrefab;
    public List<MovingObsParams> movingObsParams;

   public List<MovingObs> movingObs;
    public void Start()
    {
        movingObs = new List<MovingObs>();
        
        foreach (MovingObsParams prms in movingObsParams)
        {
            GameObject go = Instantiate(movingObsPrefab);
            MovingObs goScritp = go.GetComponent<MovingObs>();
            
            movingObs.Add(goScritp);
            
            goScritp.startPos = prms.startPos;
            goScritp.endPos = prms.endPos;
            goScritp.speed = prms.speed;
            goScritp.delay = prms.delay;

            goScritp.Move();
        }
    }

    public void StopAllCorts()
    {
        foreach (var obs in movingObs)
        {
            obs.StopAllCorts();
        }
    }
}
