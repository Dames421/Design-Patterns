using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonFactory : MonoBehaviour
{
    [SerializeField] private GameObject skeleton;

    private Vector3 skeletonSpawnMinPosition = new Vector3(-80, 0, -80);
    private Vector3 skeletonSpawnMaxPosition = new Vector3(80, 0, 80);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateSkeleton()
    {
        
        Vector3 randomPosition = new Vector3(
                Random.Range(skeletonSpawnMinPosition.x, skeletonSpawnMaxPosition.x),
                0,
                Random.Range(skeletonSpawnMinPosition.z, skeletonSpawnMaxPosition.z));

        Instantiate(skeleton, randomPosition, skeleton.transform.rotation);
        
    }
}
