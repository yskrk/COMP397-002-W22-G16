/*
 * ShellSpawner.cs
 * Joshua Eagles - 301078033
 * Yusuke Kuroki - 301137023
 * Last Modified: 2022-04-03
 * 
 * Handles the logic for the shell spawner
 * 
 * Revision History:
 * 2022-04-03 - Initial Creation
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{
	[SerializeField] private GameObject shell;

	public void SpawnShell()
	{
		shell = ObjectPooler._instance.GetInactivePooledObject();

		if (shell != null)
		{
			float y = Random.Range(-5.0f, 5.0f);
			float z = Random.Range(-10.0f, 10.0f);

			Vector3 pos = this.transform.position;

			shell.transform.position = pos;
			shell.transform.rotation = this.transform.rotation;
			shell.transform.Rotate(y, 0.0f, z);
			shell.SetActive(true);
		}
	}
}
