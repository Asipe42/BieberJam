using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    void Update()
    {
        InputKey();       
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Right");
        }
    }
}
