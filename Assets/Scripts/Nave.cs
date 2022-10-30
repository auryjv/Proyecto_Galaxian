using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
  private float velocidadX = 8;
  private float velocidadY = 8;
 
  void Start()
    {
        
    }

   
    void Update()
    {
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    transform.Translate(horizontal * velocidadX * Time.deltaTime, vertical * velocidadY * Time.deltaTime, 0);
  }
}
