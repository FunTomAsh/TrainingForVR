using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Szafa : MonoBehaviour
{
    public GameObject[] obiekty;
    private static Szafa activeSzafa;

    public void DisplayItems()
    {
        if (activeSzafa != null && activeSzafa != this && !Input.GetMouseButton(0))
        {
            activeSzafa.HideItems();
        }

        foreach (GameObject obiekt in obiekty)
        {
            obiekt.SetActive(true);
        }

        activeSzafa = this;
    }

    public void HideItems()
    {
        foreach (GameObject obiekt in obiekty)
        {
            obiekt.SetActive(false);
        }
    }
}
