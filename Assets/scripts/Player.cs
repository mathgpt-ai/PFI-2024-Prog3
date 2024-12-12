using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //references
    private ControlManager controlManager;
    private PauseMenu pauseMenu;



    //movement
    Camera cam;
    CharacterController controller;
    [SerializeField] const float SPRINT_MULTI = 1.5f;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpForce = 10;
    
    private bool aTerre = true;
   


    //shooting
    GameObject barrel;// a initialiser pour trouver de ou on spawn les bullets
    float shootDelay = 0.1f;
    private int bulletsRN = 20;
    private int bulletsTotal;

    //animation
    public Animator animator;
    private const string RELOAD_ANIMATION_STING = "reload";


    //UI
  [SerializeField]  Slider healthBar;
    [SerializeField] TextMeshProUGUI TMPhealth;
   [SerializeField] TextMeshProUGUI TMPbulletsLeft;
    [SerializeField] TextMeshProUGUI TMPbullets;
    private int health = 100;
    [SerializeField] int maxHealth = 100;

    
    private void Start()
    {
        animator.SetBool(RELOAD_ANIMATION_STING,false);
        
        cam=GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();
        controlManager=FindAnyObjectByType<ControlManager>();   
        pauseMenu=FindAnyObjectByType<PauseMenu>();
        Cursor.lockState = CursorLockMode.Locked;
        healthBar.maxValue=maxHealth;
        healthBar.value=maxHealth;
        TMPhealth.text=maxHealth.ToString();
        

        

        
    }

    public void HealthChange(int change)
    {
        health += change;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        healthBar.value= health;
        TMPhealth.text = health.ToString();
    }

    

    // Update is called once per frame
    void Update()
    {
        //Moving();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(controlManager.controls["shoot"]))
        {

        }
    }

    private void Reload()
    {
        animator.SetBool(RELOAD_ANIMATION_STING, true);

        animator.SetBool(RELOAD_ANIMATION_STING, false);
    }
    private void Moving()
    {

        

    }
}
