using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    private MaskManager maskManager;
    public GameObject startBut;
    public GameObject colBottom;
    public GameObject[] Equipment;

    void Start()
    {        
        maskManager = MaskManager.Instance;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartCheck()
    {
        bool allActive = true;
        foreach (var equip in Equipment)
        {
            if (!(equip.activeSelf))
            {
                allActive = false;
                break;
            }
        }
        bool maskActive = (maskManager != null) && (maskManager.currentEquippedMask != null);

        //Debug.Log($" Equip: {allActive}, Mask: {maskActive}");

        if (allActive && maskActive)
        {
            colBottom.SetActive(false);
            startBut.SetActive(true);

        }
        else
        {            
            startBut.SetActive(false);
            colBottom.SetActive(true);
        }
    }
}
