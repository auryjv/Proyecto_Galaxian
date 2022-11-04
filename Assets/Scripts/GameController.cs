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
    public TextMeshProUGUI gameOver;
    private float ataqueDelay;
    private int navesAtacando = 0;
    private int maxNavesAtacando = 5;

  void Start()
    {
    nivelActual = FindObjectOfType<GameStatus>().nivelActual;
    puntos = FindObjectOfType<GameStatus>().puntos;
    vidas = FindObjectOfType<GameStatus>().vidas;
    gameOver.gameObject.SetActive(false);
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
  }

  void Update()
  {
    ataqueDelay -= Time.deltaTime;
    if (ataqueDelay < 0)
    {
      if (navesAtacando < maxNavesAtacando)
      {
        Enemigo e = GetRandomEnemy();
        if (e != null)
        {
          e.InicioAtaque();
          ataqueDelay = Random.Range(3, 5);
          navesAtacando++;
        }
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
    if (listaEnemigos.Length > 0)
    {
      int n = Random.Range(0, listaEnemigos.Length);
      return listaEnemigos[n].gameObject.GetComponent<Enemigo>();
    }
    return null;
  }

  public void SumarPuntosMarcador(int puntos)
  {
   marcadorText.text = "" + puntos;
  }

  public void AnotarPuntos(bool naveAtacando, int p)
  {    
    enemigosRestantes--;


    if (naveAtacando)
    {
      navesAtacando--;
      puntos += (p + 30);
    } else
    {
      puntos += p;
    }
    FindObjectOfType<GameStatus>().puntos = puntos;
    SumarPuntosMarcador(puntos);

    if (enemigosRestantes == 0)
    {

      StartCoroutine(CambiarNivel());
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
      StartCoroutine(Revivir());
    }
    else
    {
      gameOver.gameObject.SetActive(true);
    }
  }

  IEnumerator Revivir()
  {
    yield return new WaitForSeconds(1);
    Vector2 posicionInicial = new Vector2(0, -4);
    Instantiate(PrefabNave, posicionInicial, Quaternion.identity);
  }

  IEnumerator CambiarNivel()
  {
    yield return new WaitForSeconds(2);
    nivelActual++;
    if (nivelActual > FindObjectOfType<GameStatus>().ultimoNivel)
    {
      StartCoroutine(ReiniciarJuego());
    }
    else
    {
      FindObjectOfType<GameStatus>().nivelActual = nivelActual;
      SceneManager.LoadScene("Nivel" + nivelActual);
    }
  }

  IEnumerator ReiniciarJuego()
  {
    gameOver.text = "You Win";
    gameOver.gameObject.SetActive(true);
    yield return new WaitForSeconds(2);
    SceneManager.LoadScene("Inicio");
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
    int camino = Random.Range(0, 100);
    if(camino%2 == 0) return wayPoints1;
    else return wayPoints2; 
  }

}
