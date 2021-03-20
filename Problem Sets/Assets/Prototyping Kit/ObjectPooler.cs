using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler:MonoBehaviour
{
    private List<GameObject> myPool;
    private int activeObjects;
    public GameObject prefab;
    public int startingNumber;
    

    public void Start() //or Awake??
    {
        activeObjects = 0;
        myPool = new List<GameObject>();
        gameObject.transform.position = Vector3.zero;
        for (int i = 0; i < startingNumber; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            myPool.Add(obj);
        }
    }

    public GameObject Instantiate( Vector3 position, Quaternion rotation,Transform parent)
    {
        GameObject obj;
        if (activeObjects == myPool.Count)
        {
            obj = Instantiate(prefab, position, rotation, parent);
            myPool.Add(obj);
        }
        else
        {
            obj = myPool.Find((o => { return !o.activeSelf; }));
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;
        }
        activeObjects++;
        return obj;
    }    
    
    public GameObject Instantiate( Vector3 position, Quaternion rotation)
    {
        GameObject obj;
        if (activeObjects == myPool.Count)
        {
            obj = Instantiate(prefab, position, rotation, gameObject.transform);
            myPool.Add(obj);
        }
        else
        {
            obj = myPool.Find((o => { return !o.activeSelf; }));
            obj.SetActive(true);
            obj.transform.position = position;
            obj.transform.rotation = rotation;
        }
        activeObjects++;
        return obj;
    }    
    
    public GameObject Instantiate( Vector3 position)
    {
        GameObject obj;
        if (activeObjects == myPool.Count)
        {
            obj = Instantiate(prefab, position,Quaternion.identity,gameObject.transform);
            myPool.Add(obj);
        }
        else
        {
            obj = myPool.Find((o => { return !o.activeSelf; }));
            obj.SetActive(true);
            obj.transform.position = position;
        }
        activeObjects++;
        return obj;
    }

    public void Destroy(GameObject obj)
    {
        obj.SetActive(false);
        activeObjects--;
    }

    public void OnDestroy()
    {
        foreach (var obj in myPool)
        {
            Destroy(obj);
        }
    }
}
