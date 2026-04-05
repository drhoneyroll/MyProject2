using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyLogic> agents = new List<EnemyLogic>();
    public int farmeSkipBufferFar = 9;
    public int frameSkipBufferMid = 4;
    public float farDistance;
    public float midDistance;
    private float sqrFar;
    private float sqrMid;


    void Awake()
    {
        /*
        for(int i = 0; i < transform.childCount; i++)
        {
            agents.Add(transform.GetChild(i).GetComponent<EnemyLogic>());
        }
        */
    }

    void Start()
    {
        sqrFar = farDistance * farDistance;
        sqrMid = midDistance * midDistance;
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {

            if(agents[i] != enabled)
            {
                agents.Remove(agents[i]);
                continue;
            }

            Vector3 distance =  agents[i].attackPostion.position - agents[i].transform.position;

            if(distance.sqrMagnitude >= sqrFar)
            {
                if(Time.frameCount % 120 != farmeSkipBufferFar) continue;
                agents[i].Tick();
                continue;
            } 
            else if(distance.sqrMagnitude >= sqrMid && distance.sqrMagnitude <= sqrFar)
            {
                if(Time.frameCount % 30 != frameSkipBufferMid) continue;
                agents[i].Tick();
                continue;
            }
            else
            {
                if(Time.frameCount % 3 != frameSkipBufferMid) continue;
                agents[i].Tick();
                continue;
            }
        }
    }
}
