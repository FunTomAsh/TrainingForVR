using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFaze2 : MonoBehaviour
{
    public GameObject[] faze2Objects;
    public GameObject[] faze1Objects;
    public GameObject player;

    private void OnMouseUp()
    {
        foreach (var f2 in faze2Objects)
        {
            f2.SetActive(true);
        };
        
        foreach (var f1 in faze1Objects)
        {
            f1.SetActive(false);
        };
        
    }
}
