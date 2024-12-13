using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private Canvas Game;//0
    [SerializeField] private Canvas Pause;//1
    [SerializeField] private Canvas Settings;//2
   [SerializeField] Player Player;

   
    
    public void Pausez()
   {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0.0f;
        ShowCanevas(1);
        //Player.enabled = false;

   }
    private void Start()
    {
        
    }
    public void SettingPause()
    {
        ShowCanevas(2);
    }
    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1.0f;
        ShowCanevas(0);
        //Player.enabled = true;
        
    }
    private void ShowCanevas(int canevas)
    {
        if (canevas == 0)
        {
            Game.enabled = true;
            Pause.enabled = false;
            Settings.enabled = false;
        }
        else if (canevas == 1)
        {
            Game.enabled = false;
            Pause.enabled = true;
            Settings.enabled = false;
        }
        else
        {
            Game.enabled = false;
            Pause.enabled = false;
            Settings.enabled = true;
        }
    }

}
