using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;

    // Inspector
    [SerializeField] TreeSpawner _treeSpawner;
    [SerializeField] int initCount;
    [SerializeField] int[] speedUpIndex;
    [SerializeField] bool[] onSpeedUp;
    
    // Property
    public int killCount
    {
        get { return _killCount; }
        set { _killCount = value; }
    }

    public Queue<Tree> treeGroup;

    // Value
    private int _killCount;

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
