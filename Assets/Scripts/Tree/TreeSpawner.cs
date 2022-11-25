using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform treeGroup;
    [SerializeField] Tree[] treePrefab;

    [Header("Branch")]
    [SerializeField, Range(0, 1)] float branchChance = 0.3f;
    [SerializeField] Branch[] branchPrefabs;

    [ContextMenu("Spawn Tree")]
    public void SpawnTree()
    {
        int ranIndex = Random.Range(0, treePrefab.Length);

        var spawnedTree = Instantiate(treePrefab[ranIndex], spawnTransform.position, Quaternion.identity);


        // branch
        if (branchChance > Random.Range(0f, 1f))
        {
            int branchRandIndex = Random.Range(0, branchPrefabs.Length);
            var branchPrefab = branchPrefabs[branchRandIndex];

            var spawnedBranch = Instantiate(branchPrefab, spawnedTree.transform);
            spawnedTree.branch = spawnedBranch;
        }


        TreeManager.instance.treeGroup.Enqueue(spawnedTree);
        spawnedTree.transform.SetParent(treeGroup);
    }
}
