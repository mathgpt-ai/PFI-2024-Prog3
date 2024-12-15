using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ennemy : MonoBehaviour
{
    [SerializeField] int maxHealth = 10;
    [SerializeField] int dps = 5;//each second he deals x damage not continious attack
    Canvas canevas;
    Slider healthBar;
    [SerializeField]GameObject Player;
    Player player;
    private bool isInRange = false;
    private Manage manage;
    [SerializeField] GameObject Manager;
    private List<GameObject> pathList;


    void Start()
    {
        manage= Manager.GetComponent<Manage>();
        canevas=GetComponentInChildren<Canvas>();
        healthBar=GetComponentInChildren<Slider>();
        healthBar.maxValue=maxHealth;
        healthBar.value=maxHealth;
        
    }
    

    void FollowPlayer()
    {
        pathList = Pathfinding.GetPath(manage.path, gameObject, Player);


    }

    void MoveToPlayer()
    {

    }

   
    void DealDamage()
    {
        
        player.HealthChange(dps);
    }
    void Update()
    {
        
    }
}
