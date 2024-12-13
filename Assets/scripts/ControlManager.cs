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

    public PauseMenu pauseMenu;




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
            { "jump", KeyCode.Space }
    };

    public bool listening = false;
    [HideInInspector] public string nameof = "";

    public Button[] buttons;

    private void Awake()
    {
        enabled = true;
        pauseMenu = FindObjectOfType<PauseMenu>();
        RefreshNames();

    }
    public void ChangeDictionary(string name)
    {
        listening = true;
        nameof = name;
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                if (key == KeyCode.Escape)
                {

                    pauseMenu.Pausez();
                    listening = false;
                    return;
                }
                bool keyInUse = controls.Any(pair => pair.Value == key);
                if (keyInUse)
                {
                    print(ERROR_SAME_KEY);
                    listening = false;
                    return;
                }
                controls[name] = key;
                RefreshNames();
                return;

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
        listening = false;
    }



    private void Update()
    {
        if (listening)
        {
            ChangeDictionary(nameof);

        }

    }
    void GestionPause()
    {
        print("Pause");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            print("penis");
            pauseMenu.Pausez();
            enabled = false;
        }
    }

}
