using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCotterPin : MonoBehaviour
{
    public float installDistance;
    public LayerMask cotterPinPlacementLayer;
    public void InstallCotterPin()
    {
        Collider[] collider = Physics.OverlapSphere(this.transform.position, installDistance,cotterPinPlacementLayer);
        if (collider.Length < 1) return;

        var cotterPinPlacement = collider[0].gameObject;

        Debug.Log("add");

        var cotterPinScript = cotterPinPlacement.GetComponent<CotterPin>();
        cotterPinScript.AddCotterPin();
        if (cotterPinScript.isAdded)
        {
            this.gameObject.SetActive(false);
        }
    }
}
