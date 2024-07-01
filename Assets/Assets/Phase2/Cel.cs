using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cel : MonoBehaviour
{
    public int requiredSetting = 0;
    public bool isCombine = false;
    public bool isShot = false;
    public GameObject[] walls;
    public RoomChecker rc;
    public Phase2Checker p2c;

    IEnumerator changeColor(int secs)
    {
        yield return new WaitForSeconds(secs);
        if (isCombine)
        {
            foreach (var wall in walls)
            {
                wall.GetComponent<Renderer>().material.color = Color.white;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.white;
        }
        
    }

    public bool Shot(int ustPiersc)
    {
        if (!isShot && ustPiersc == 0)
        {
            isShot = true;
            rc.scianZostalo--;
            rc.CheckRoom();
            p2c.CheckCompletion();
            if (isCombine)
            {
                foreach(var wall in walls)
                {
                    wall.GetComponent<Renderer>().material.color = Color.green;
                    StartCoroutine(changeColor(1));
                }
                return true;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.green;
                StartCoroutine(changeColor(1));
                return true;
            }
            
        }
        else if(ustPiersc != 0)
        {
            p2c.erZleUstStrzelPom++;
            if (isCombine)
            {
                foreach (var wall in walls)
                {
                    wall.GetComponent<Renderer>().material.color = Color.red;
                    StartCoroutine(changeColor(1));
                }
                return false;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
                StartCoroutine(changeColor(1));
                return false;
            }
        }
        else
        {
            p2c.erLScTrafWiecNizRaz++;
            if (isCombine)
            {
                foreach (var wall in walls)
                {
                    wall.GetComponent<Renderer>().material.color = Color.red;
                    StartCoroutine(changeColor(1));
                }
                return false;
            }
            else
            {
                GetComponent<Renderer>().material.color = Color.red;
                StartCoroutine(changeColor(1));
                return false;
            }
        }
    }
}
