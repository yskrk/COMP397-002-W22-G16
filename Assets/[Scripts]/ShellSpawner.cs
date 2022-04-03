using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shell;

    public void SpawnShell()
    {
        shell = ShotgunShellPooler._instance.GetPooledObject();

        if (shell != null)
        {
            float y = Random.Range(-5.0f, 5.0f);
            float z = Random.Range(-10.0f, 10.0f);

            // Ajust position and rotation to under shotgun
            shell.transform.position = this.transform.position;
            shell.transform.rotation = this.transform.rotation;
            
            // Rotate the shell for proper falling
            shell.transform.Rotate(0.0f, y, z);
            shell.SetActive(true);
        }
    }
}
