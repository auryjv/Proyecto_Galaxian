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
  /*
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Enemigo")
      {
      
        //Destroy(other.gameObject);
        //gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        //explosion.SetTrigger("Active");
         Destroy(gameObject);
      }
    }
  */
}
