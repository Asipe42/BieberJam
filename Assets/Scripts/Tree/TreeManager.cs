using System.Collections.Generic;
using UnityEngine;

public class TreeManager : MonoBehaviour
{
    public static TreeManager instance;

    public Queue<Tree> treeGroup;

    private void Awake()
    {
        instance = this;

        treeGroup = new Queue<Tree>();
    }
}
