using UnityEngine;
using System.Collections;

public static class Events
{
    public static System.Action ResetApp = delegate { };
    public static System.Action<bool> ShowJoystick = delegate { };
    public static System.Action OnJoystickPressed = delegate { };

}
   
