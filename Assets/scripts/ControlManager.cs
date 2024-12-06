using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    
    public Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>
        {
            { "forward", KeyCode.W },
            { "backwards", KeyCode.S },
            { "left", KeyCode.A },
            { "right", KeyCode.D },
            { "reload", KeyCode.R },
            { "shoot", KeyCode.Mouse0 },
            { "aim", KeyCode.Mouse1 },
            { "sprint", KeyCode.LeftControl },
            { "interact", KeyCode.E }
        };

    public bool listening = false;

    public Button[] buttons;

    private void Awake()
    {
        RefreshNames();

    }

    private void RefreshNames()
    {

        foreach (Button button in buttons)
        {
            
            
            var texts = button.GetComponentsInChildren<TextMeshProUGUI>();
            foreach (TextMeshProUGUI text in texts)
            {
                if (text != null && text.name == "Key-Text")
                {
                    StringBuilder sb = new StringBuilder(button.name);
                    sb.Replace("Button", "");
                    text.text = controls[sb.ToString()].ToString();

                }
            }
        }
    }



    private void Update()
    {
        if (listening)
        {


        }

    }
}
