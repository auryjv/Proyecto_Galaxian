using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemigo : MonoBehaviour
{
  public Transform PrefabDisparoEnemigo;
  public Transform prefabExplosion;

  public float velocidadDisparoEnemigo ;

  [SerializeField] float velocidad = 2;
  private float distanciaCambio = 0.2f;
  private byte numeroSiguientePosicion = 0;
  private Vector3 siguientePosicion;
  private float incrementoVelocidadPorNivel = 1.0f;
  Vector3 posicionInicial;
  bool alcanzado = false;
  protected int TipoNave;
  protected int Puntos;
  protected Transform[] wayPoints;
  bool modoAtaque = false;

    protected void Start()
    {
      modoAtaque = false;
      posicionInicial = transform.position;
      int nivelActual = FindObjectOfType<GameStatus>().nivelActual;
       
      if (nivelActual > 1) velocidad += nivelActual * incrementoVelocidadPorNivel;

      wayPoints = FindObjectOfType<GameController>().GetWayPoints(transform.position);
      if (wayPoints.Length > 0)
        siguientePosicion = wayPoints[0].position;
    }

  void Update()
  {
    if (modoAtaque)
    {
      transform.position = Vector3.MoveTowards(transform.position, siguientePosicion, velocidad * Time.deltaTime);

      if (Vector3.Distance(transform.position, siguientePosicion) < distanciaCambio)
      {
        numeroSiguientePosicion++;
        if (numeroSiguientePosicion >= wayPoints.Length)
          numeroSiguientePosicion = 0;
        siguientePosicion = wayPoints[numeroSiguientePosicion].position;

      }
    }
   

   }

     void OnTriggerEnter2D(Collider2D other)
    {
      if (other.tag == "Disparo")
      {
      Transform explosion = Instantiate(prefabExplosion, other.transform.position, Quaternion.identity);
      explosion.gameObject.GetComponent<AudioSource>().Play();
      Destroy(explosion.gameObject, 1f);
      // Siempre tengo que destruir el disparo
      Destroy(other.gameObject);
        // Solo muestro animacion y sumo puntos si es el primer disparo el que me alcanza.
        if (!alcanzado)
        {
          alcanzado = true;
          gameObject.GetComponent<Animator>().SetTrigger("Destroy");
          FindObjectOfType<GameController>().AnotarPuntos(modoAtaque, Puntos);
        }
      }
      else if (other.tag == "LimiteInferior")
      {
        FindObjectOfType<GameController>().Renacer(TipoNave, posicionInicial);
        Destroy(gameObject);
      }
    }

     public void InicioAtaque()
    {
      modoAtaque = true;
     
      gameObject.GetComponent<Animator>().SetTrigger("Giro");
      StartCoroutine(Disparar());
    }

     IEnumerator Disparar()
    {
      if (modoAtaque)
      {
        gameObject.GetComponent<AudioSource>().Play();
        float pausa = Random.Range(1.0f, 3.0f);
        yield return new WaitForSeconds(pausa);
        Transform disparoEnemigo = Instantiate(PrefabDisparoEnemigo, transform.position, Quaternion.identity);
        disparoEnemigo.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidadDisparoEnemigo, 0);
        StartCoroutine(Disparar());
      }
    }


  
}
