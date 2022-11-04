using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{   [SerializeField] GameObject[] contadorVidas;
    [SerializeField] GameObject[] contadorNiveles;
    [SerializeField] Transform[] wayPoints1;
    [SerializeField] Transform[] wayPoints2;
    [SerializeField] GameObject bloque;
    private int  puntos;
    private int  vidas;
    private int nivelActual;
    private int ultimoNivel;
    private int enemigosRestantes;
    [SerializeField] TextMeshProUGUI marcadorText;
    public Transform PrefabEnemigoRojo;
    public Transform PrefabEnemigoVerde;
    public Transform PrefabEnemigoLila;
    public Transform PrefabEnemigoAmarillo;
    public Transform PrefabNave;
    public GameObject gameOver;
    private float ataqueDelay;
  private int navesAtacando = 0;
    private int maxNavesAtacando = 5;

  void Start()
    {
    nivelActual = FindObjectOfType<GameStatus>().nivelActual;
    puntos = FindObjectOfType<GameStatus>().puntos;
    vidas = FindObjectOfType<GameStatus>().vidas;
    gameOver.SetActive(false);
    ataqueDelay = Random.Range(3, 5);
    navesAtacando = 0;

    SumarPuntosMarcador(puntos);
    // Ocultamos todas las vidas
    for (int i = 0; i < contadorVidas.Length; i++)
      contadorVidas[i].gameObject.SetActive(false);
    // Ocultamos todos los niveles
    for (int i = 0; i < contadorNiveles.Length; i++)
      contadorNiveles[i].gameObject.SetActive(false);

    // Mostramos las vidas que nos quedan
    for (int i = 0; i < vidas; i++)
      contadorVidas[i].gameObject.SetActive(true);
    // Mostramos tantas banderas como nivel
    for (int i = 0; i < nivelActual; i++)
      contadorNiveles[i].gameObject.SetActive(true);

    enemigosRestantes = FindObjectsOfType<Enemigo>().Length;
   
    Debug.Log("Enemigos Rstantes: " + enemigosRestantes + " Nivel: " + nivelActual + " Vidas: " + vidas);    
  }

  void Update()
  {
    ataqueDelay -= Time.deltaTime;
    if (ataqueDelay < 0)
    {
      if (navesAtacando < maxNavesAtacando)
      {
        GetRandomEnemy().InicioAtaque();
        ataqueDelay = Random.Range(3, 5);
        navesAtacando++;
      }
      else
      {
        ataqueDelay = Random.Range(0, 1);
      }
      
    }
  }

  public Enemigo GetRandomEnemy()
  {
    var listaEnemigos = FindObjectsOfType<Enemigo>();
    int n = Random.Range(0, listaEnemigos.Length);
    return listaEnemigos[n].gameObject.GetComponent<Enemigo>();
  }

  public void SumarPuntosMarcador(int puntos)
  {
   marcadorText.text = "" + puntos;
  }

  public void AnotarPuntos(bool naveAtacando)
  {
    //GetComponent<AudioSource>().Play();
    
    enemigosRestantes--;

    if (naveAtacando)
    {
      navesAtacando--;
      puntos += 30;
    } else
    {
      puntos += 10;
    }
    FindObjectOfType<GameStatus>().puntos = puntos;
    SumarPuntosMarcador(puntos);

    Debug.Log("Quedan Enemigos; " + enemigosRestantes);
    if (enemigosRestantes == 0)
    {
      CambiarNivel();
    }
  }

  public void RestarVida()
  {
    vidas--;
    contadorVidas[vidas].SetActive(false);
    FindObjectOfType<GameStatus>().vidas = vidas;
    FindObjectOfType<Nave>().GetComponent<Animator>().SetTrigger("Destroy");
    
    if (vidas > 0)
    {
      Debug.Log("Reviviendo Nave");
      StartCoroutine(Revivir());
    }
    else
    {
      gameOver.SetActive(true);
    }
  }

  IEnumerator Revivir()
  {
    Debug.Log("Before Waiting 2 seconds");
    yield return new WaitForSeconds(1);
    Vector2 posicionInicial = new Vector2(0, -4);
    Instantiate(PrefabNave, posicionInicial, Quaternion.identity);
    Debug.Log("After Waiting 2 Seconds");
  }

  public void CambiarNivel()
  {
    nivelActual++;
    if (nivelActual >= FindObjectOfType<GameStatus>().ultimoNivel)
    {
      SceneManager.LoadScene("Fin");
    }
    FindObjectOfType<GameStatus>().nivelActual = nivelActual;
    SceneManager.LoadScene("Nivel" + nivelActual);
  }

  public void Renacer(int tipoNave, Vector3 posicionInicial)
  {
    navesAtacando--;

    if (tipoNave == 0)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoAmarillo, posicionInicial, Quaternion.identity, bloque.transform);
    }
    else if (tipoNave == 1)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoRojo, posicionInicial, Quaternion.identity, bloque.transform);
    }
    else if (tipoNave == 2)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoLila, posicionInicial, Quaternion.identity, bloque.transform);
    }
    else
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoVerde, posicionInicial, Quaternion.identity, bloque.transform);
    }
  }

  public Transform[] GetWayPoints(Vector2 position)
  {
    int camino = Random.Range(0, 1);
    if(camino == 0) return wayPoints1;
    else return wayPoints2; 
  }

}
