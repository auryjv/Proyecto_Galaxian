using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
  private float velocidadX = 8;
  [SerializeField] Transform PrefabDisparo;
  private float velocidadDisparo = 4;
  
  void Start()
    {
     
    }

   
    void Update()
    {
    float horizontal = Input.GetAxis("Horizontal");
    

      transform.Translate(horizontal * velocidadX * Time.deltaTime, 0, 0);
      if (Input.GetButtonDown("Fire1"))
      {
        Transform disparo = Instantiate(PrefabDisparo, transform.position, Quaternion.identity);
        disparo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidadDisparo, 0);
      }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "DisparoEnemigo")
      {
        Destroy(other.gameObject);
        gameObject.GetComponent<Animator>().SetTrigger("Destroy");
      }
    }

   
}
