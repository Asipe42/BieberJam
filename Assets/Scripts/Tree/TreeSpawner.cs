using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform treeGroup;
    [SerializeField] Tree[] treePrefab;

    [Header("Branch")]
    [SerializeField, Range(0, 1)] float branchChance = 0.3f;
    [SerializeField] Branch[] branchPrefabs;

    // Value
    Vector2 _treeSize;

    // Event
    private void Awake()
    {
        _treeSize = treePrefab[0].gameObject.GetComponent<BoxCollider2D>().size;
    }

    [ContextMenu("Spawn Tree")]
    public void SpawnTree()
    {
        int ranIndex = Random.Range(0, treePrefab.Length);


        var trees = TreeManager.instance.treeGroup.ToArray();
        Tree lastTree = null;
        if (trees.Length > 0)
        {
            lastTree = trees[trees.Length - 1];
        }

        var spawnPos = new Vector2(
            (lastTree) ? lastTree.transform.position.x: spawnTransform.position.x,
            (lastTree) ? lastTree.transform.position.y + _treeSize.y : spawnTransform.position.y);
        var spawnedTree = Instantiate(treePrefab[ranIndex], spawnPos, Quaternion.identity);


        // branch
        if (branchChance > Random.Range(0f, 1f))
        {
            int branchRandIndex = Random.Range(0, branchPrefabs.Length);
            var branchPrefab = branchPrefabs[branchRandIndex];

            var spawnedBranch = Instantiate(branchPrefab, spawnedTree.transform);
            spawnedTree.branch = spawnedBranch;
        }

        if (lastTree)
        {
            spawnedTree.gameObject.GetComponent<Rigidbody2D>().velocity = lastTree.gameObject.GetComponent<Rigidbody2D>().velocity;
        }

        TreeManager.instance.treeGroup.Enqueue(spawnedTree);
        spawnedTree.transform.SetParent(treeGroup);
    }
}
