using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ShareefSoftware
{
    public class ClutterSpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> clutterPrefabs;
        [SerializeField] private float clutterChance = 0.15f;

        private List<Transform> paths;

        private void Start()
        {
            paths = GameObject.FindGameObjectsWithTag("path")
            .Select(p => p.transform)
            .ToList();
            
            SpawnClutter();
        }

        private void SpawnClutter()
        { 
            foreach (var path in paths)
            { 
                if (Random.value > clutterChance) continue;

                GameObject prefab = clutterPrefabs[Random.Range(0, clutterPrefabs.Count)];

                float pathHeight = path.localScale.y;
                float prefabHeight = prefab.transform.localScale.y;

                Vector3 offset = new Vector3(
                    Random.Range(-path.localScale.x / 2, path.localScale.x / 2),
                    pathHeight / 2 + prefabHeight * 4 / 3,
                    Random.Range(-path.localScale.z / 2, path.localScale.z / 2));
                Vector3 position = path.position + offset;
                Quaternion rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

                GameObject clutter = Instantiate(prefab, position, rotation);
                clutter.transform.SetParent(transform);
            }
        }
    }
}
