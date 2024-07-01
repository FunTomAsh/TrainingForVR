using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class Phase2Checker : MonoBehaviour
{ 
    public float seconds = 0.00f;
    public int minutes = 0;
    public int hoursLol = 0;
    public int ilPoprZneutrDrzwi = 0;
    public int ilPoprZneutrPom = 0;
    public int erBrakPomiaru = 0;
    public int erDuzUstP = 0;
    public int erMalUstP = 0;
    public int erZleUstStrzelPom = 0;
    public int erPomScZaDrz = 0;
    public int erLScTrafWiecNizRaz = 0;
    public bool isSucces = false;
    public GameObject[] rooms;
    public GameObject[] toHide;
    public GameObject[] toShow;
    public TMP_Text wynik;
    public TMP_Text czas;
    public TMP_Text ilPoprZneutrDrzwiText;
    public TMP_Text ilPoprZneutrPomText;
    public TMP_Text erBrakPomiaruText;
    public TMP_Text erDuzUstPText;
    public TMP_Text erMalUstPText;
    public TMP_Text erZleUstStrzelPomText;
    public TMP_Text erPomScZaDrzText;
    public TMP_Text erLScTrafWiecNizRazText;

    private void Awake()
    {
        CheckCompletion();
    }
    void UpdTimer()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60.0f)
        {
            minutes++;
            seconds = 0;
            if (minutes >= 60)
            {
                hoursLol++;
                minutes = 0;
            }
        }
    }

    void Update()
    {
        UpdTimer();
    }

    public void CheckCompletion()
    {
        erPomScZaDrz = 0;
        foreach (var rum in rooms)
        {
            erPomScZaDrz += rum.GetComponent<RoomChecker>().scianZostalo;
        }
        if (ilPoprZneutrPom == rooms.Length)
        {
            isSucces = true;
            TheEnd();
        }
    }

    public void TheEnd()
    {
        Debug.Log("Game Over");
        StopAllCoroutines();
        foreach (var obj in toHide)
        {
            obj.SetActive(false);
        }
        foreach (var obj in toShow)
        {
            obj.SetActive(true);
        }

        if (isSucces)
        {
            wynik.SetText("Wynik: <color=green>ZWYCIESTWO</color>");

        }
        else
        {
            wynik.SetText("Wynik: <color=red>PORAZKA</color>");
        }

        czas.SetText("Czas: " + hoursLol + ":" + minutes + ":" + (int) seconds);
        ilPoprZneutrDrzwiText.SetText("Liczba poprawnie zneutralizowanych drzwi: " + ilPoprZneutrDrzwi);
        ilPoprZneutrPomText.SetText("Liczba poprawnie zneutralizowanych pomieszczen: " + ilPoprZneutrPom);
        erBrakPomiaruText.SetText("Brak wykonania pomiaru: " + erBrakPomiaru);
        erDuzUstPText.SetText("Za duze ustawienie pierscienia podczas neutralizacji drzwi: " + erDuzUstP);
        erMalUstPText.SetText("Za male ustawienie pierscienia podczas neutralizacji drzwi: " + erMalUstP);
        erZleUstStrzelPomText.SetText("Zle ustawienie pierscienia podczas strzelania w pomieszczeniu za drzwiami: " + erZleUstStrzelPom);
        erPomScZaDrzText.SetText("Pominiete sciany w pomieszczeniu za drzwiami: " + erPomScZaDrz);
        erLScTrafWiecNizRazText.SetText("Liczba scian w pomieszczeniu za drzwiami trafionych wiecej niz raz: " + erLScTrafWiecNizRaz);
    }
}
