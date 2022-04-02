using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletBehaviour : MonoBehaviour
{
    private float timer = 0.0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3.0f)
        {
            this.gameObject.SetActive(false);
            timer = 0.0f;
        }
    }
}
