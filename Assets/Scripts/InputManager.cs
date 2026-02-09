using Google.XR.Cardboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // NOTE:
        // RegisterActionButton() and ReadInput() are called together here
        // only as an example to demonstrate the full input-binding workflow.
        //
        // In a real application, these would typically NOT be executed
        // in the same update loop (e.g., binding in a settings menu and/or on first application start,
        // and input reading during normal gameplay).


        // Listens for user input to (re)bind an action to a specific key
        // and stores that binding persistently between sessions.
        RegisterActionButton();

        // Checks whether the previously registered input was performed
        // and triggers the corresponding action if detected.
        ReadInput();
    }

    private static void ReadInput()
    {
        if (Input.GetKeyDown((KeyCode)PlayerPrefs.GetInt("Action Button")))
        {
            Debug.Log("Action Performed");
            Api.Recenter();
        }
    }

    private static void RegisterActionButton()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log(keyCode);
                    PlayerPrefs.SetInt("Action Button", (int)keyCode);
                }
            }
        }
    }
}
