using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
  public Dictionary<string,KeyCode> controls= new Dictionary<string,KeyCode>();

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
}
