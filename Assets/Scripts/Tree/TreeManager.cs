using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;

    // Inspector
    [SerializeField] TreeSpawner _treeSpawner;
    [SerializeField] int initCount; 

    public Queue<Tree> treeGroup;

    private void Awake()
    {
        instance = this;

        treeGroup = new Queue<Tree>();

        for(var i = 0; i < initCount; i++)
        _treeSpawner.SpawnTree();
    }
}
