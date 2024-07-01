using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskManager : MonoBehaviour
{
    public static MaskManager Instance;
    public DragAndDrop currentEquippedMask;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EquipMask(DragAndDrop mask)
    {
        if (currentEquippedMask != null)
        {
            currentEquippedMask.UnequipItem();
        }

        currentEquippedMask = mask;
        mask.equipment.SetActive(true);
        mask.toHide.SetActive(false);
        mask.equipped.SetActive(true);
        mask.GetComponent<Renderer>().material.color = Color.green;
        mask.ReturnToDefault();
    }

    public void UnequipCurrentMask()
    {
        if (currentEquippedMask != null)
        {
            currentEquippedMask.equipment.SetActive(false);
            currentEquippedMask.toHide.SetActive(true);
            currentEquippedMask.equipped.SetActive(false);
            currentEquippedMask.GetComponent<Renderer>().material.color = Color.black;
            currentEquippedMask.ReturnToDefault();
            currentEquippedMask = null;
        }
    }
}
