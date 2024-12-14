using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //references
    [SerializeField] private ControlManager controlManager;
    [SerializeField] private PauseMenu pauseMenu;

    //random
    Vector3 camRotation = new Vector3(0, 0, 0);


    //movement
    Camera cam;
    CharacterController controller;
    [SerializeField] const float SPRINT_MULTI = 1.5f;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpForce = 10;
   
    [SerializeField] float cameraSpeed = 1;//sensitivity i think

    [SerializeField] GameObject body;

    



    //shooting
    [SerializeField] GameObject barrel;// a initialiser pour trouver de ou on spawn les bullets
    float shootDelay = 0f;
    private int bulletsRN = 20;//bullets he can shoot before reload  
    [SerializeField] private int bulletsTotal = 100;//bullets total in reloads
    [SerializeField] GameObject bullet;
    [SerializeField] int magSize = 20;

    [SerializeField] float shootingRate = 10;


    //aiming
    private float waitTimeForAds = 4;
    private float waitTimeForAdsOriginal;


    //reloading
    private bool reloading = false;
    private float timeLeft2Reload = 4;
    private float timeToReloadOriginal = 4;




    //animation
    public Animator animator;
    private const string RELOAD_ANIMATION_STING = "reload";


    //UI
    [SerializeField] Canvas PauseCanny;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI TMPhealth;
    [SerializeField] TextMeshProUGUI TMPbulletsRN;//bullets before reload
    [SerializeField] TextMeshProUGUI TMPbulletsTotal;//all the bullets
    private int health = 100;
    [SerializeField] int maxHealth = 100;


    //moving

    [SerializeField] float gravity = 10;
    Vector3 jump = new Vector3(0, 0, 0);




    private void Start()
    {

        animator.SetBool("adsing", false);
        animator.SetBool(RELOAD_ANIMATION_STING, false);

        cam = GetComponentInChildren<Camera>();
        controller = GetComponentInChildren<CharacterController>();
        controlManager = FindAnyObjectByType<ControlManager>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        Cursor.lockState = CursorLockMode.Locked;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        TMPhealth.text = maxHealth.ToString();
        TMPbulletsRN.text = bulletsRN.ToString();
        TMPbulletsTotal.text = bulletsTotal.ToString();

        waitTimeForAds = waitTimeForAdsOriginal;
        pauseMenu.Resume();
        bulletsRN = magSize;






    }

    public void HealthChange(int change)
    {
        health += change;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthBar.value = health;
        TMPhealth.text = health.ToString();
    }

    private void GestionPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {


            pauseMenu.Pausez();


        }

    }



    // Update is called once per frame
    void Update()
    {
        CameraRotation();
        GunManaging();
        GestionPause();
        Moving();
    }
    private void GunManaging()
    {
        AimDownSight();
        Shoot();
        Reload();

    }
    private void AimDownSight()
    {
        if (Input.GetMouseButton(1) && !reloading)
        {
            animator.SetBool("adsing", true);
            waitTimeForAds = waitTimeForAdsOriginal;



        }
        else
        {
            animator.SetBool("adsing", false);
            waitTimeForAds -= Time.deltaTime;
        }

    }

    private void Shoot()
    {



        if (Input.GetMouseButton(0) && shootDelay <= 0 && bulletsRN > 0 && !reloading)
        {

            GameObject tempBullet = ObjectPool.instance.GetPooledObject(bullet);


            if (tempBullet != null)//pour etre sur que le pool est ok//
            {
                tempBullet.transform.position = barrel.transform.position;



                tempBullet.transform.forward = barrel.transform.forward;




                tempBullet.SetActive(true);


                bulletsRN--;
                TMPbulletsRN.SetText(bulletsRN.ToString());



            }
            shootDelay = 1 / shootingRate;


        }
        else
            shootDelay -= Time.deltaTime;


    }




    private void Reload()
    {
        if (Input.GetKeyDown(controlManager.controls["reload"]) && bulletsTotal > 0)
        {
            animator.SetBool("reload", true);
            reloading = true;
            timeLeft2Reload = timeToReloadOriginal;


        }
        if (reloading)
        {
            timeLeft2Reload -= Time.deltaTime;

            if (timeLeft2Reload <= 0.01)
            {
                animator.SetBool("reload", false);
                reloading = false;
                if (bulletsTotal >= magSize)
                {
                    bulletsTotal -= magSize - bulletsRN;
                    bulletsRN = magSize;
                }
                else
                {
                    bulletsRN = bulletsTotal;
                    bulletsTotal = 0;
                }
            }
            TMPbulletsRN.text = bulletsRN.ToString();
            TMPbulletsTotal.text = bulletsTotal.ToString();
        }


    }
    private void Moving()
    {

        Vector3 cameraFoward = cam.transform.forward;
        Vector3 cameraRight = cam.transform.right;

        cameraFoward.y = 0;
        cameraRight.y = 0;

        cameraFoward.Normalize();
        cameraRight.Normalize();
        float moveX = 0;
        float moveZ = 0;
        if (Input.GetKey(controlManager.controls["forward"]))
            moveZ = 1;
        if (Input.GetKey(controlManager.controls["backwards"]))
            moveZ = -1;
        if (Input.GetKey(controlManager.controls["left"]))
            moveX = -1;
        if (Input.GetKey(controlManager.controls["right"]))
            moveX = 1;

        Vector3 move = (cameraFoward * moveZ + cameraRight * moveX).normalized;
        if (Input.GetKey(controlManager.controls["sprint"]))
            controller.Move(move * speed * Time.deltaTime*SPRINT_MULTI);
        else
            controller.Move(move * speed * Time.deltaTime);


        //ca fonctionne pas il detecte qu'il est jamais grounded
        if (!controller.isGrounded)
        {
            jump -= transform.up * Time.deltaTime * gravity;

            print("no");
            
        }
        else
        {
            
            if (Input.GetKey(controlManager.controls["jump"]))
            {
               
                jump = transform.up * jumpForce;
                jump.y=Mathf.Max(-1,jump.y);
            }
        }


    }

    void CameraRotation()
    {

        camRotation += new Vector3(0, Input.GetAxis("Look_Horizontal") * Time.deltaTime * cameraSpeed, 0);

        camRotation.x = Mathf.Clamp(camRotation.x, -70, 70);//le joueur ne regarde pasa ses pieds

        cam.transform.rotation = Quaternion.Euler(camRotation.x, camRotation.y, 0);
        body.transform.rotation = Quaternion.Euler(0, camRotation.y + body.transform.rotation.y, 0);
    }


}
