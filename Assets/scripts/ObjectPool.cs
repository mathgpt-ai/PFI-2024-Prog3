using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   private List<GameObject> pool = new List<GameObject>();

    [SerializeField] GameObject[] ObjectToPool;
    [SerializeField] int[] quantityPerObject;

    public static ObjectPool instance;
    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    private void Start()
    {
        for(int i=0;i<Mathf.Min(ObjectToPool.Length, quantityPerObject.Length); i++)//Mathmin au cas ou mal instancier 
        {
            for(int j = 0; j < quantityPerObject[i]; j++)
            {
                GameObject obj=Instantiate(ObjectToPool[i]);
                obj.name = ObjectToPool[i].name;
                obj.SetActive(false);
                pool.Add(obj);
            }
        }
    }

   //chercher un object dans le poolqui pas actifs
    public GameObject GetPooledObject(GameObject typeObj)
    {
        for(int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy && pool[i].name == typeObj.name)
                return pool[i];
        }


        return null;//au cas ou ya rien de dispo
    }
}
