using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCrack : MonoBehaviour
{
    //public GameObject[] spawnPosition;
    public GameObject crackPrefab;
    public List<GameObject> spawnPosition = new List<GameObject>();

    private GameObject wheel;
    private int random;
    // Start is called before the first frame update
    void Start()
    {
        wheel = this.transform.gameObject;
        foreach (Transform child in wheel.transform)
        {
            if (child.tag == "SpawnPosition")
                spawnPosition.Add(child.gameObject);
        }

        //random create crack
        if (Random.value >= 0.5f)
        {
            spawnCrack();
        }

    }

    // Update is called once per frame
    void Update()
    {
    }


    //random spawn crack 
    private void spawnCrack()
    {
        random = Random.Range(0, spawnPosition.Count);
        GameObject crack = Instantiate(crackPrefab, spawnPosition[random].transform.position, Quaternion.identity);
        crack.transform.parent = spawnPosition[random].transform;
    }
}
