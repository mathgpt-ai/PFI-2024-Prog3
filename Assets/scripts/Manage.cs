using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class Manage : MonoBehaviour
{
    [SerializeField] private int space = 2;
    
    [SerializeField] GameObject Dots;
    public List<GameObject> DotsList;   
    void Start()
    {
        CreateDots(10);
    }

   private void CreateDots(int size)
    {
        for(int i=0;i<size;i++)
        {
            for (int j=0;j<size;j++)
            {
                Instantiate(Dots).transform.position=new Vector3((i-size/2)*space,10,(j-size/2)*space);

            }
        }
    }
    void Update()
    {
        if(DotsList != null)
        {
            foreach(GameObject dot in DotsList)
            {
                print(dot.transform.position.ToString());  
            }
        }
        
    }
}

public static class Pathfinding
{
  

}
public interface IGraph
{

}



