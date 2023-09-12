using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WindowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject tableWindow;
    [SerializeField]
    private GameObject promptWindow;
    // Start is called before the first frame update
    void Start()
    {
        promptWindow.SetActive(true);
        tableWindow.SetActive(false);
    }

    public void OnPromptSet() {
        promptWindow.SetActive(false);
        tableWindow.SetActive(true);
    }
}
