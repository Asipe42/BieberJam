using UnityEngine;

public class CloudManager : MonoBehaviour
{
    [SerializeField] Cloud[] clouds;
    [SerializeField] float deadline;
    [SerializeField] float repositionX;

    private void Update()
    {
        CheckCloudPosition();
    }

    void CheckCloudPosition()
    {
        foreach (var cloud in clouds)
        {
            if (cloud.transform.position.x > deadline)
            {
                ResetPositon(cloud);
            }
        }
    }

    void ResetPositon(Cloud target)
    {
        target.transform.position = new Vector2(repositionX, target.transform.position.y);
    }
}
