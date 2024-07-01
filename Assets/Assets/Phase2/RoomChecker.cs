using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChecker : MonoBehaviour
{
    public bool isNuetrl = false;
    public int scianZostalo = 6;
    public GameObject[] targets;
    public Phase2Checker p2c;
    public void CheckRoom()
    {
        int numberOfNeutr = 0;
        foreach (var target in targets)
        {            
            Cel cel = target.GetComponent<Cel>();
            if (cel.isShot)
            {
                numberOfNeutr++;
            }
            else
            {
                break;
            }
        }
        if (numberOfNeutr == targets.Length)
        {
            isNuetrl = true;
            p2c.ilPoprZneutrPom++;
            p2c.CheckCompletion();
        }
    }
}
