using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    bool isPaused=false;
    [SerializeField] private Canvas Game;//0
    [SerializeField] private Canvas Pause;//1
    [SerializeField] private Canvas Settings;//2


    void Update()
    {
        
       if(Input.GetKeyUp(KeyCode.Escape))
            isPaused = !isPaused;
       
    }
}
