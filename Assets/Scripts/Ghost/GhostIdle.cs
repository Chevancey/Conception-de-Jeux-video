using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class GhostIdle : GhostBehavior
{
    private void OnEnable()
    {
        ghostController.movement.setMotionless();
    }

    private void OnDisable()
    {
        ghostController.leaving.enabled = true; ;
    }

}
