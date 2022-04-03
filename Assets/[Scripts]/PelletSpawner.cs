using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletSpawner : MonoBehaviour
{
    [SerializeField] private int pelletAmount;
    [SerializeField] private float spreadAngle;
    [SerializeField] private float pelletVelocity;
    [SerializeField] private GameObject pellet;

    public void FirePellet()
    {
        for (int i = 0; i < pelletAmount; i++)
        {
            pellet = PelletPooler._instance.GetPooledObject();
            if (pellet != null)
            {
                // Ajust position and rotation to under shotgun
                pellet.transform.position = this.transform.position;
                pellet.transform.rotation = this.transform.rotation;

                // Make pellet spreading ***Continuing***
                pellet.transform.rotation = Quaternion.RotateTowards(pellet.transform.rotation, pellet.transform.rotation, spreadAngle);
                pellet.GetComponent<Rigidbody>().AddForce(this.transform.position * pelletVelocity);
                pellet.SetActive(true);
            }
        }
    }
}
