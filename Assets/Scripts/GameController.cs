using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{   [SerializeField] GameObject[] contadorVidas;
    [SerializeField] GameObject[] contadorNiveles;
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

  void Start()
    {
    nivelActual = FindObjectOfType<GameStatus>().nivelActual;
    puntos = FindObjectOfType<GameStatus>().puntos;
    vidas = FindObjectOfType<GameStatus>().vidas;
    gameOver.SetActive(false);

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
    // List<GameObject> listEnemigos = new List<GameObject>(FindObjectsOfType<Enemigo>().Select(enemy => enemy.gameObject));
   
    Debug.Log("Enemigos Rstantes: " + enemigosRestantes + " Nivel: " + nivelActual + " Vidas: " + vidas);
    // GetRandomEnemy().SetActive(false);
    GetRandomEnemy().InicioAtaque();
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

  public void AnotarPuntos()
  {
    puntos += 10;
    //GetComponent<AudioSource>().Play();
    FindObjectOfType<GameStatus>().puntos = puntos;
    SumarPuntosMarcador(puntos);
    enemigosRestantes--;
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

      // SceneManager.LoadScene("GameOver");
    }
    else
    {
      gameOver.SetActive(true);
    }
    /*Application.Quit();
    */
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
    if (tipoNave == 0)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoAmarillo, posicionInicial, Quaternion.identity);
    }
    else if (tipoNave == 1)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoRojo, posicionInicial, Quaternion.identity);
    }
    else if (tipoNave == 2)
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoLila, posicionInicial, Quaternion.identity);
    }
    else
    {
      Transform naveEnemigo = Instantiate(PrefabEnemigoVerde, posicionInicial, Quaternion.identity);
    }
  }

}
