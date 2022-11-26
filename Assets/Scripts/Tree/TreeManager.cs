using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;

    // Inspector
    [SerializeField] TreeSpawner _treeSpawner;
    [SerializeField] int initCount;
    [SerializeField] int[] speedUpIndex;
    [SerializeField] bool[] onSpeedUp;
    
    public int killCount;

    public Queue<Tree> treeGroup;

    public void ResetSppedUP()
    {
        for (int i = 0; i < onSpeedUp.Length; i++)
        {
            onSpeedUp[i] = false;
        }
    }

    private void Awake()
    {
        instance = this;

        treeGroup = new Queue<Tree>();
    }
    private void Start()
    {
        for (var i = 0; i < initCount; i++)
            _treeSpawner.SpawnTree();
    }

    private void Update()
    {
        CheckKillCount();
    }

    void CheckKillCount()
    {
        for (int i = 0; i < onSpeedUp.Length; i++)
        {
            if (onSpeedUp[i])
                return;

            if (killCount > speedUpIndex[i])
            {
                onSpeedUp[i] = true;
                HP.instance.SpeedUp();
            }
        }
    }
}
