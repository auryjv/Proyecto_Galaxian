using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparo : MonoBehaviour
{
    Animator explosion;
 
    void Start()
    {
      explosion = gameObject.GetComponent<Animator>();
    }
  
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Limite")
      {
         Destroy(gameObject);
      }
    }
  
}
