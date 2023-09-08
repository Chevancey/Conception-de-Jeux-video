using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GhostIdle : GhostBehavior
{
    private void OnDisable()
    {
        ghostController.Body.enabled = true;
        ghostController.scatter.Enable();
    }

}
