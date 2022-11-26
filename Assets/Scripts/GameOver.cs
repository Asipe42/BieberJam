using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    private void OnEnable()
    {
        ShowObject();
    }

    public void ShowObject()
    {
        foreach(var obj in objects)
        {
            obj.SetActive(false);
        }

        foreach (var obj in objects)
        {
            obj.SetActive(true);
        }
    }
}