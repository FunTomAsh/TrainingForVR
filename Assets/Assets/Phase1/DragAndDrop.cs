using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAndDrop : MonoBehaviour
{
    public string slot;
    public GameObject equipment;
    public GameObject toHide;
    public GameObject equipped;
    public Transform defPostition;
    private bool isDragging = false;
    private Vector3 hold;
    private Camera cameraFaza1;
    private MaskManager maskManager;
    private StartChecker startChecker;

    void Start()
    {
        cameraFaza1 = Camera.main;
        maskManager = MaskManager.Instance;
        startChecker = FindObjectOfType<StartChecker>();
    }

    void OnMouseDown()
    {
        if (equipped.activeSelf)            
        {
             UnequipItem();
        }
        else
        {
            isDragging = true;
            hold = transform.position - GetMouseWorldPos();
        }        
    }

    void OnMouseUp()
    {
        isDragging = false;
        GetComponent<Renderer>().material.color = Color.black;
        Ray ray = cameraFaza1.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.gameObject.name == slot)
            {
                EquipItem();
                return;
            }
        }
        ReturnToDefault();
    }

    void OnMouseOver()
    {
        if (equipped.activeSelf)
        {
            GetComponent<Renderer>().material.color = Color.red;
            GetComponentInChildren<TextMeshPro>().SetText("Unequip?");
        }
        else
        { 
            GetComponent<Renderer>().material.color = Color.yellow;
        }
        
    }

    void OnMouseExit()
    {
        if (equipped.activeSelf)
        {
            GetComponent<Renderer>().material.color = Color.green;
            GetComponentInChildren<TextMeshPro>().SetText("EQUIPPED");
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
    }

    void Update()
    {
        if (isDragging)
        {            
            transform.position = GetMouseWorldPos() + hold;
        }
    }

    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = cameraFaza1.WorldToScreenPoint(transform.position).z;
        return cameraFaza1.ScreenToWorldPoint(mousePoint);
    }

    public void EquipItem()
    {
        if (CompareTag("Mask"))
        {
            maskManager.EquipMask(this);
        }
        else
        {
            equipment.SetActive(true);
            toHide.SetActive(false);
            equipped.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
            ReturnToDefault();
        }
        startChecker.StartCheck();
    }

    public void UnequipItem()
    {
        if (CompareTag("Mask"))
        {
            if (maskManager != null && maskManager == MaskManager.Instance)
            {
                maskManager.UnequipCurrentMask();
            }
        }
        else
        {
            equipment.SetActive(false);
            toHide.SetActive(true);
            equipped.SetActive(false);
            GetComponent<Renderer>().material.color = Color.black;
            ReturnToDefault();
        }
        startChecker.StartCheck();
    }

    public void ReturnToDefault()
    {
        if (equipped.activeSelf)
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.black;
        }
        transform.position = defPostition.position;
        transform.rotation = defPostition.rotation;
        //startChecker.StartCheck();
    }
}
