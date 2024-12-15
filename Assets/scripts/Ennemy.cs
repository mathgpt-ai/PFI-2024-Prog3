using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ennemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int dps = 5;//each second he deals x damage not continious attack
    [SerializeField] float speed = 5;
    Canvas canevas;
    Slider healthBar;
    [SerializeField]GameObject Player;
    Player player;
    private bool isInRange = false;
    private Manage manage;
    [SerializeField] GameObject Manager;
    private List<GameObject> pathList;
    Rigidbody rb;

    private int TargetInt = 0;


    void Start()
    {
        rb=GetComponent<Rigidbody>();
        manage= Manager.GetComponent<Manage>();
        canevas=GetComponentInChildren<Canvas>();
        healthBar=GetComponentInChildren<Slider>();
        healthBar.maxValue=maxHealth;
        healthBar.value=maxHealth;
        
    }
    

    void FollowPlayer()
    {
        pathList = Pathfinding.GetPath(manage.path, gameObject, Player);

        GameObject currentTarget = pathList[TargetInt];
        Vector3 targetPosition = currentTarget.transform.position;
        Vector3 direction = (targetPosition - transform.position).normalized;
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }

    

   
    void DealDamage()
    {
        
        player.HealthChange(dps);
    }
    void Update()
    {
        
    }
}
