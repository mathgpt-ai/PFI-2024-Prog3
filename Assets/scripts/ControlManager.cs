using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
  public Dictionary<string,KeyCode> controls= new Dictionary<string,KeyCode>();
    public bool listening = false;

    public Button[] buttons;

    [HideInInspector]
    public string controlName = "";
    private void Awake()
    {
        controls.Add("foward", KeyCode.W);
        controls.Add("backwards", KeyCode.S);
        controls.Add("left", KeyCode.A);
        controls.Add("right", KeyCode.D);
        controls.Add("Reload",KeyCode.R);
        controls.Add("shoot", KeyCode.Mouse0);
        controls.Add("aim", KeyCode.Mouse1);
        controls.Add("sprint", KeyCode.LeftControl);
        controls.Add("interact", KeyCode.E);

    }

    public void ChangeControls(string control)
    {
        listening = true;
        controlName=control;
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode))) 
        {
            if (Input.GetKeyDown(key))
            {
                controls[control] = key;
                listening = false;
                foreach(Button button in buttons)
                {
                    if (button.name == control + "Button")
                        button.GetComponentInChildren<Text>().text = key.ToString();
                }
            }
        }
    }
    private void Update()
    {
        if (listening)
        {
            ChangeControls(controlName);
        }
    }
}
