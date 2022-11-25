using UnityEngine;

public class InputReader : MonoBehaviour
{
    void Update()
    {
        InputKey();       
    }

    void InputKey()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            TreeManager.instance.treeGroup.Peek().Shoot();
        }
    }
}
