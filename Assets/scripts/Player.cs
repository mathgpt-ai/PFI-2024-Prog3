using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
     Camera cam;
    CharacterController controller;
    [SerializeField] const float SPRINT_MULTI = 1.5f;
    [SerializeField] float speed = 1;
   

    GameObject barrel;// a initialiser pour trouver de ou on spawn les bullets
    float shootDelay = 0.1f;


    //Ui
  [SerializeField]  Slider healthBar;
    [SerializeField] TextMeshProUGUI TMPhealth;
   [SerializeField] TextMeshProUGUI TMPbulletsLeft;
    [SerializeField] TextMeshProUGUI TMPbullets;
    private int bullets = 20;
    private int health = 100;
    [SerializeField] int maxHealth = 100;

    
    [SerializeField] float jumpForce = 10;
    private ControlManager controlManager;
    private PauseMenu pauseMenu;
    private bool aTerre = true;
    CharacterController cc;

    private void Start()
    {

        
        cam=GetComponentInChildren<Camera>();
        cc = GetComponent<CharacterController>();
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

    private void Moving()
    {

        if (Input.GetKeyDown(controlManager.controls["foward"]))
        {
            print("lol");
        }

    }
}
