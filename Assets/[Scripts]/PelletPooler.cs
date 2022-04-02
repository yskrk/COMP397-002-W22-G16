using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletPooler : MonoBehaviour
{
    // singleton
    [SerializeField] private List<GameObject> pools;
    [SerializeField] private GameObject obj;
    [SerializeField] private int amount = 0;

    // singleton
    public static PelletPooler _instance;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pools = new List<GameObject>();
        GameObject tmp;

        for (int i = 0; i < amount; i++)
        {
            tmp = Instantiate(obj);
            tmp.SetActive(false);
            pools.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!pools[i].activeInHierarchy)
            {
                return pools[i];
            }
        }

        return null;
    }
}
