using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shell;

    private int shellAmount;

    public void SpawnShell()
    {
        shell = ObjectPooler._instance.GetPooledObject();

        if (shell != null)
        {
            float y = Random.Range(-5.0f, 5.0f);
            float z = Random.Range(-10.0f, 10.0f);

            Vector3 pos = this.transform.position;

            shell.transform.position = pos;
            shell.transform.rotation = this.transform.rotation;
            shell.transform.Rotate(0.0f, y, z);
            shell.SetActive(true);
        }
    }
}
