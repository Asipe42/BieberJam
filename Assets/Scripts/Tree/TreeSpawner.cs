using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform treeGroup;
    [SerializeField] Tree[] treePrefab;

    [ContextMenu("Spawn Tree")]
    public void SpawnTree()
    {
        int ranIndex = Random.Range(0, treePrefab.Length);

        var spawnedTree = Instantiate(treePrefab[ranIndex], spawnTransform.position, Quaternion.identity);
        
        TreeManager.instance.treeGroup.Enqueue(spawnedTree);
        spawnedTree.transform.SetParent(treeGroup);
    }
}
