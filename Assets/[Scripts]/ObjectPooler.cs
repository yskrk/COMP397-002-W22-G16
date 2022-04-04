/*
 * ObjectPooler.cs
 * Joshua Eagles - 301078033
 * Yusuke Kuroki - 301137023
 * Last Modified: 2022-04-03
 * 
 * Creates the pooled objects to be used for the ejected shells
 * 
 * Revision History:
 * 2022-04-03 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	// singleton
	[SerializeField] private List<GameObject> pooledObjects;
	[SerializeField] private GameObject shell;
	[SerializeField] private int amount = 8;

	// singleton
	public static ObjectPooler _instance;
	private void Awake()
	{
		_instance = this;
	}

	// Start is called before the first frame update
	void Start()
	{
		pooledObjects = new List<GameObject>();
		GameObject gameObject;

		for (int i = 0; i < amount; i++)
		{
			gameObject = Instantiate(shell);
			gameObject.SetActive(false);
			pooledObjects.Add(gameObject);
		}
	}

	public GameObject GetInactivePooledObject()
	{
		for (int i = 0; i < amount; i++)
		{
			if (!pooledObjects[i].activeInHierarchy)
			{
				return pooledObjects[i];
			}
		}

		return null;
	}
}
