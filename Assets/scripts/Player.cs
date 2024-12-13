using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //references
    [SerializeField]private ControlManager controlManager;
    [SerializeField] private PauseMenu pauseMenu;




    //movement
    Camera cam;
    CharacterController controller;
    [SerializeField] const float SPRINT_MULTI = 1.5f;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpForce = 10;
    private bool canMove = true;

    private bool aTerre = true;



    //shooting
    [SerializeField] GameObject barrel;// a initialiser pour trouver de ou on spawn les bullets
    float shootDelay = 0f;
    [SerializeField]private int bulletsRN = 20;//bullets he can shoot before reload    A ENLEVERR LE SERIALISE FIELD LORSQUE LE RELOAD FAAIT ET LE METTRE SUR BULLETS TOTAL
    private int bulletsTotal;//bullets total including reload bullets
    [SerializeField]GameObject bullet;

    [SerializeField] float shootingRate = 10;
    
    



    //animation
    public Animator animator;
    private const string RELOAD_ANIMATION_STING = "reload";


    //UI
    [SerializeField] Canvas PauseCanny;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI TMPhealth;
    [SerializeField] TextMeshProUGUI TMPbulletsLeft;//bullets before reload
    [SerializeField] TextMeshProUGUI TMPbullets;//all the bullets
    private int health = 100;
    [SerializeField] int maxHealth = 100;


    //pause menu
    bool isPaused = false;



    private void Start()
    {
        
       
        animator.SetBool(RELOAD_ANIMATION_STING, false);

        cam = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        controlManager = FindAnyObjectByType<ControlManager>();
        pauseMenu = FindAnyObjectByType<PauseMenu>();
        Cursor.lockState = CursorLockMode.Locked;
        healthBar.maxValue = maxHealth;
        healthBar.value = maxHealth;
        TMPhealth.text = maxHealth.ToString();

        pauseMenu.Resume();







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
        Shoot();
        GestionPause();
    }
    private void GunManaging()
    {
        AimDownSight();
        Shoot();
        Reload();

    }
    private void AimDownSight()
    {
        //a faire
    }

    private void Shoot()
    {



        if (Input.GetMouseButton(0) && shootDelay <=0&&bulletsRN>0)
        {

            GameObject tempBullet = ObjectPool.instance.GetPooledObject(bullet);
            

            if (tempBullet != null)//pour etre sur que le pool est ok//
            {
                tempBullet.transform.position = barrel.transform.position;
                tempBullet.SetActive(true);

                print("shoot");
                bulletsRN--;
                TMPbulletsLeft.SetText(bulletsRN.ToString());
                


            }
            shootDelay=1/shootingRate;


        }
        else
            shootDelay-=Time.deltaTime;
        

    }

    


    private void Reload()
    {
        animator.SetBool(RELOAD_ANIMATION_STING, true);

        animator.SetBool(RELOAD_ANIMATION_STING, false);
    }
    private void Moving()
    {



    }






    public void DisableMovement()
    {
        canMove = false;
    }

    public void EnableMovement()
    {
        canMove = true;
    }

}
