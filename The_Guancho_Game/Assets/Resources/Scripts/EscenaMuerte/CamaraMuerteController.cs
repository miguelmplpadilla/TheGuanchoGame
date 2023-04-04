using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMuerteController : MonoBehaviour
{
    public float speed = 1;
    
    void Update()
    {
        if (transform.position.y > 13.5f)
        {
            Vector3 movement = new Vector3(0, 0, 1) * Time.deltaTime;
            transform.Translate(movement * speed);
        }
    }
}
