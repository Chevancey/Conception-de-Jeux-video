using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CherrySpawn : MonoBehaviour
{
    Transform[] childTransforms = null;

    [SerializeField]
    private GameObject cherry;
    
    bool spawnable = true;

    [SerializeField]
    float timeToSpawn;

    private void Awake()
    {
        childTransforms = GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (spawnable)
        {
            Wait();
            spawnable = false;
        }
    }

    private void CherrySpawning()
    {
        int rand = Random.Range(0, childTransforms.Length);

        Instantiate(cherry, childTransforms[rand]);
        spawnable = true;
    }

    private void Wait() 
    {
        Invoke(nameof(CherrySpawn), timeToSpawn);
    }
}
