using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkingSceneManager : MonoBehaviour
{
    public GameObject handManagerPrefab;
    public static ParkingSceneManager instance;

    private GameObject handManagerKeeper;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            handManagerKeeper = Instantiate(handManagerPrefab, gameObject.transform.position, Quaternion.identity);
            DontDestroyOnLoad(handManagerKeeper);
            DontDestroyOnLoad(gameObject);
            resetHandManager();
        }
    }
    public void resetHandManager()
    {
        handManagerKeeper.GetComponent<HandManager>().resetHand();
    }
    public void hideHandControll()
    {
        handManagerKeeper.GetComponent<HandManager>().invisibleHands();
    }
    public void showHandControll()
    {
        handManagerKeeper.GetComponent<HandManager>().visibleHands();
    }
}
