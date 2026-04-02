using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public List<EnemyLogic> agents = new List<EnemyLogic>();
    public int frameSkipBuffer = 4;

    void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            agents.Add(transform.GetChild(i).GetComponent<EnemyLogic>());
        }
    }

    void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            if(Time.frameCount % 5 != frameSkipBuffer) return;
            agents[i].Tick();
        }
    }
}
