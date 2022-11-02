using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemigo : MonoBehaviour
{
  public Transform PrefabDisparoEnemigo;
  public float velocidadDisparoEnemigo ;
  [SerializeField] List<Transform> wayPoints;
  [SerializeField] float velocidad = 2;
  private float distanciaCambio = 0.2f;
  private byte numeroSiguientePosicion = 0;
  private Vector3 siguientePosicion;
  private float incrementoVelocidadPorNivel = 0.3f;
  Vector3 posicionInicial;
  protected int tipoNave;

    void Start()
    {
      posicionInicial = transform.position;
      siguientePosicion = wayPoints[0].position;
      int nivelActual = FindObjectOfType<GameStatus>().nivelActual;
      if (nivelActual > 1) velocidad += nivelActual * incrementoVelocidadPorNivel;
    }

  void Update()
  {
    
    /*
     * transform.position = Vector3.MoveTowards(
    transform.position,
    siguientePosicion,
    velocidad * Time.deltaTime);

    if (Vector3.Distance(transform.position, siguientePosicion) < distanciaCambio)
    {
      numeroSiguientePosicion++;
      if (numeroSiguientePosicion >= wayPoints.Count)
        numeroSiguientePosicion = 0;
      siguientePosicion = wayPoints[numeroSiguientePosicion].position;

    }
   */

   }

     void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Disparo")
      {
        FindObjectOfType<GameController>().SendMessage("AnotarPuntos");
        Destroy(other.gameObject);
        gameObject.GetComponent<Animator>().SetTrigger("Destroy");
      }
      else if (other.tag == "limiteInferior")
      {
        FindObjectOfType<GameController>().SendMessage("Renacer");
        Destroy(other.gameObject);
      }
    }

     public void InicioAtaque()
    {
      StartCoroutine(Disparar());
    }

     IEnumerator Disparar()
    {
      float pausa = Random.Range(5.0f, 11.0f);
      yield return new WaitForSeconds(pausa);
      Transform disparoEnemigo = Instantiate(PrefabDisparoEnemigo, transform.position, Quaternion.identity);
      disparoEnemigo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidadDisparoEnemigo, 0);
      StartCoroutine(Disparar());
    }


  
}
