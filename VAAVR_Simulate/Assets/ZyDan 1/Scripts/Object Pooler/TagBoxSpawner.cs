using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagBoxSpawner : MonoBehaviour
{
    ObjectPooler objectPooling;
    //public Transform spawnPos;

    private void Start()
    {
        objectPooling = ObjectPooler.Instance;
    }

    // Start is called before the first frame update
    public void Spawn()
    {
        objectPooling.SpawnFromPool("TagBox");
    }
}
