using UnityEngine;

public class Cloud : MonoBehaviour
{
    float randomSpeed = 0.05f;

    private void Update()
    {
        transform.Translate(Vector2.right * randomSpeed * Time.deltaTime);
    }
}
