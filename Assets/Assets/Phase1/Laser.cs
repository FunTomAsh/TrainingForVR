using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public Camera cameraFaza1;
    public LineRenderer laser;
    private Szafa activeSzafa;

    void Update()
    {
        Ray ray = cameraFaza1.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        laser.SetPosition(0, transform.position);

        if (Physics.Raycast(ray, out hit))
        {
            laser.SetPosition(1, hit.point);
            Szafa szafa = hit.collider.GetComponent<Szafa>();
            if (szafa != null)
            {
                szafa.DisplayItems();
                activeSzafa = szafa;
            }
        }
        else
        {
            laser.SetPosition(1, ray.GetPoint(100));
        }
    }
}
