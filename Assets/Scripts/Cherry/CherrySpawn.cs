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
        if (spawnable && !GameManager.Instance.pacman.canShoot)
        {
            spawnable = false;
            Wait();
            
        }
    }

    private void CherrySpawning()
    {
        int rand = Random.Range(1, childTransforms.Length);

        Instantiate(cherry, childTransforms[rand]);
    }

    private void Wait() 
    {
        Invoke(nameof(CherrySpawning), timeToSpawn);
    }

    public void SetSpwanable()
    {
        spawnable = true;
    }
}
