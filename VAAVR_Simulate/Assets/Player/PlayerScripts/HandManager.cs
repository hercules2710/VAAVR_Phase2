using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public HandController leftHand;
    public HandController RightHand;
    public void resetHand()
    {
        leftHand.Start();
        RightHand.Start();
    }
    public void invisibleHands()
    {
        leftHand.hideHand();
        RightHand.hideHand();
    }
    public void visibleHands()
    {
        leftHand.showHand();
        RightHand.showHand();
    }
}
