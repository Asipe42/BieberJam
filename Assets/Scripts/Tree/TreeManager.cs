using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;

    // Inspector
    [SerializeField] private TreeSpawner _treeSpawner;

    public Queue<Tree> treeGroup;

    private void Awake()
    {
        instance = this;

        treeGroup = new Queue<Tree>();

        for(var i = 0; i < 5; i++)
        _treeSpawner.SpawnTree();
    }
}
