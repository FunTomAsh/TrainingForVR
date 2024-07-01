using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isNeutralized = false;
    public bool isChecked = false;
    public bool failedToNeutralize = false;
    public bool isOpen = false;
    public int requiredSetting = 0;
    public GameObject fire;
    public GameObject anime;
    public Phase2Checker p2c;


    public IEnumerator destroy(int secs)
    {
        yield return new WaitForSeconds(secs);
        anime.SetActive(false);
    }
    public void Check()
    {
        if(isNeutralized)
        {
            isChecked = true;
            GetComponent<Renderer>().material.color = Color.white;
            //anime.GetComponent<Animator>().Play("Door");
        }
    }

    public void DoorDestr()
    {
        failedToNeutralize = true;
        fire.SetActive(true);
        StartCoroutine(destroy(3));
    }
    public bool TryNeutralize(int ringSetting)
    {
        if (ringSetting == requiredSetting && !failedToNeutralize)
        {
            if (!isNeutralized)
            {
                isNeutralized = true;
                p2c.ilPoprZneutrDrzwi++;                
                GetComponent<Renderer>().material.color = Color.green;
                requiredSetting = 0;
            }      

/*            else
            {
                isChecked = true;
                GetComponent<Renderer>().material.color = Color.white;
                anime.GetComponent<Animator>().Play("Door");
            }*/
                        
            return true;
        }
        else if(ringSetting > requiredSetting && !failedToNeutralize)
        {
            p2c.erDuzUstP++;
            DoorDestr();            
            return false;
        }
        else if (ringSetting < requiredSetting && !failedToNeutralize)
        {
            p2c.erMalUstP++;
            DoorDestr();
            return false;
        }
        else
        {
            DoorDestr();
            return false;
        }
    }
}
