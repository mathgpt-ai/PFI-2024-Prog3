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
    


    void Start()
    {
        canevas=GetComponentInChildren<Canvas>();
        healthBar=GetComponentInChildren<Slider>();
        healthBar.maxValue=maxHealth;
        healthBar.value=maxHealth;
    }

    void FollowPlayer()
    {
        //using A* shit blah blah
        //deplacement blah blah

    }
    void DealDamage()
    {
        
        player.HealthChange(dps);
    }
    void Update()
    {
        
    }
}
