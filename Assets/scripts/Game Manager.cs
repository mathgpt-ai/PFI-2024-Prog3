using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas Game;//0
    [SerializeField] private Canvas Pause;//1
    [SerializeField] private Canvas Settings;//2

    


    void Start()
    {
        /*ShowCanevas(0)*/;//on met pour que de base ce sois le game UI
    }


    // Update is called once per frame
    void Update()
    {

    }
   

    public  void ShowCanevas(int canevas)
    {
        if(canevas == 0)
        {
            Game.enabled = true;
            Pause.enabled = false;
            Settings.enabled = false;
        }
        else if(canevas == 1)
        {
            Game.enabled = false;
            Pause.enabled = true;
            Settings.enabled = false;
        }
        else
        {
            Game.enabled=false;
            Pause.enabled=false;
            Settings.enabled=true;
        }
    }

}
