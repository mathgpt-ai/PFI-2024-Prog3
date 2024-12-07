using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    private const string ERROR_SAME_KEY = "Error the key you are trying to use is not ";
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
    [HideInInspector] public string nameof = "";

    public Button[] buttons;

    private void Awake()
    {
        RefreshNames();

    }
    public void ChangeDictionary(string name)
    {
        listening = true;
        nameof= name;
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if(key==KeyCode.Escape)
                {
                    //faut sortir du menu
                }
                foreach(KeyValuePair<string,KeyCode> pair in controls)
                {
                    if (key != pair.Value)
                    {
                        controls[name] = key; 
                        RefreshNames();
                    }
                        
                    else
                        print(ERROR_SAME_KEY);
                    


                }
                
            }
        }
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
        listening=false;
    }



    private void Update()
    {
        if (listening)
        {
            ChangeDictionary(nameof);

        }

    }
}
