using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;
    public Rigidbody rb;
    public int hp = 3;
    public TMP_Text hpText;
    public float moveSpeed = 5f;
    public float mouseSensitivity = 12f;
    private float yRotation = 0f;

    [Header("Measurement Device")]
    public GameObject urzPom;
    public Camera measurementCamera;
    public int zeskanowaneUst = 0;
    public TMP_Text urzPomText;    

    [Header("Weapon")]
    public GameObject bron;
    public Transform kierBron;
    public RectTransform crosshair;
    public GameObject muzzleSmokePrefab;
    public GameObject muzzleFirePrefab;
    public int ustPiersc = 0;
    public TMP_Text ustPoz;    

    [Header("Scripts")]
    public Phase2Checker p2c;

    [Header("Other")]
    public float doorOpenDistance = 1f;
    public GameObject nacText;
    public GameObject hitEffectPrefab;
    public bool transported = false;


    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        /*        rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;*/
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    void Update()
    {
        //Movement();
        //Rotation();
        UpdateCrosshairPosition();

        if (Input.GetMouseButtonDown(0))
        {
            ShotGun(Camera.main);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            /*zeskanowaneUst = 0;
            urzPomText.SetText(zeskanowaneUst.ToString());*/
            ScanFromDevice(measurementCamera);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            TryOpenDoor();
        }


        UpdInterf();
        UpdatePiersc();
    }

    void FixedUpdate()
    {
        if (!transported)
        {
            player.transform.eulerAngles = new Vector3(0, 0, 0);
            player.transform.position = new Vector3(15f, 0.3f, 15f);
            //Debug.Log("Player OnEnable position: " + player.transform.position);
            transported = true;
        }
        
        Movement();
        Rotation();
    }

    void UpdInterf()
    {
        if (hp > 0 && !p2c.isSucces)
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, doorOpenDistance))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (hit.collider.CompareTag("Door") && !door.isOpen && !door.failedToNeutralize)
                {
                    nacText.SetActive(true);
                }
            }
            else
            {
                nacText.SetActive(false);
            }
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        player.transform.Translate(Vector3.forward * Time.deltaTime * z * moveSpeed);
        player.transform.Translate(Vector3.right * Time.deltaTime * x * moveSpeed);

        /*rb.position += z * transform.forward * Time.deltaTime * moveSpeed;
        rb.position += x * transform.right * Time.deltaTime * moveSpeed;*/
    }

    void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Debug.Log("mouseX: " + mouseX + ", mouseY: " + mouseY + ", mouseSensitivity: " + mouseSensitivity);

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);

        player.transform.Rotate(Vector3.up * mouseX);
        Camera.main.transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }

    void UpdateCrosshairPosition()
    {
        Ray ray = new Ray(kierBron.position, kierBron.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector2 viewportPoint = Camera.main.WorldToViewportPoint(hit.point);
            crosshair.anchorMin = viewportPoint;
            crosshair.anchorMax = viewportPoint;
        }
    }

    void ScanFromDevice(Camera cam)
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    zeskanowaneUst = door.requiredSetting;
                    door.Check();
                    urzPomText.SetText(zeskanowaneUst.ToString());
                }
            }
            else if (hit.collider.CompareTag("Cel"))
            {
                zeskanowaneUst = 0;
                urzPomText.SetText(zeskanowaneUst.ToString());
            }
        }        
    }


    void ShotGun(Camera cam)
    {
        //w przyp centr ekranu
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Instantiate(muzzleFirePrefab, kierBron);
        Instantiate(muzzleSmokePrefab, kierBron);
        Ray ray = new Ray(kierBron.position, kierBron.forward);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    if (!door.isNeutralized)
                    {
                        bool success = door.TryNeutralize(ustPiersc);
                        if (!success)
                        {
                            LooseHp();
                        }
                    }
                }
            }
            else if (hit.collider.CompareTag("Cel"))
            {
                Instantiate(hitEffectPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                Cel cel = hit.collider.GetComponent<Cel>();
                if (cel != null)
                {
                    bool success = cel.Shot(ustPiersc);
                    if (!success)
                    {
                        LooseHp();
                    }
                }
            }
        }
        
    }

    void TryOpenDoor()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, doorOpenDistance))
        {
            if (hit.collider.CompareTag("Door"))
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null)
                {
                    if (!door.isNeutralized)
                    {
                        door.DoorDestr();
                        LooseHp();
                    }
                    else if (door.isNeutralized && !door.isChecked)
                    {
                        p2c.erBrakPomiaru++;
                        door.DoorDestr();
                        LooseHp();
                    }
                    else
                    {
                        if (!door.isOpen)
                        door.anime.GetComponent<Animator>().Play("Door");
                        door.isOpen = true;
                    }
                }
            }
        }
    }

    void LooseHp()
    {
        hp--;
        hpText.SetText(hp + " x");
        if (hp <= 0)
        {
            p2c.TheEnd();
        }
    }

    void UpdatePiersc()
    {
        //pirewsza metoda
/*        if (Input.GetKeyDown(KeyCode.Alpha1)) { SetPiersc(1); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { SetPiersc(2); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { SetPiersc(3); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { SetPiersc(4); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { SetPiersc(5); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { SetPiersc(6); }
        if (Input.GetKeyDown(KeyCode.Alpha7)) { SetPiersc(7); }
        if (Input.GetKeyDown(KeyCode.Alpha8)) { SetPiersc(8); }
        if (Input.GetKeyDown(KeyCode.Alpha9)) { SetPiersc(9); }
        if (Input.GetKeyDown(KeyCode.Alpha0)) { SetPiersc(0); }
*/
        // Druga metoda
             
        if (Input.GetKeyDown(KeyCode.E))
        {
            ustPiersc = Mathf.Clamp(ustPiersc + 1, 0, 9);
            ustPoz.SetText(ustPiersc.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ustPiersc = Mathf.Clamp(ustPiersc - 1, 0, 9);
            ustPoz.SetText(ustPiersc.ToString());
        }
    }

    void SetPiersc(int setting)
    {
        ustPiersc = setting;
        ustPoz.SetText(setting.ToString());
    }

}
