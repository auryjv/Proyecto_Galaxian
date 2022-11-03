using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{   [SerializeField] GameObject[] contadorVidas;
    private int  puntos;
    private int  vidas;
    private int nivelActual;
    private int ultimoNivel;
   // private int enemigosRestantes;
    [SerializeField] TextMeshProUGUI marcadorText;
    public Transform PrefabEnemigoRojo;
    public Transform PrefabEnemigoVerde;
    public Transform PrefabEnemigoLila;
    public Transform PrefabEnemigoAmarillo;
  void Start()
    {

    puntos = FindObjectOfType<GameStatus>().puntos;
    SumarPuntosMarcador(puntos);
    vidas = FindObjectOfType<GameStatus>().vidas;
    for (int i = vidas; i < contadorVidas.Length; i++)
    contadorVidas[i].gameObject.SetActive(false);
    nivelActual = FindObjectOfType<GameStatus>().nivelActual;
   // enemigosRestantes = FindObjectsOfType<Enemigo>().Length;
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
   /* enemigosRestantes--;

    if (enemigosRestantes == 0 && nivelActual == 3)
    {
      SceneManager.LoadScene("Fin");
    }*/
  }

  public void RestarVida()
  {
    vidas--;
    contadorVidas[vidas].SetActive(false);
    FindObjectOfType<GameStatus>().vidas = vidas;

    if (vidas <= 0)
    {
      SceneManager.LoadScene("GameOver");
    }
    Application.Quit();
  }
  public void CambiarNivel()
  {
    nivelActual++;
    if (nivelActual > FindObjectOfType<GameStatus>().ultimoNivel)
    {
      nivelActual = 1;
    }
    FindObjectOfType<GameStatus>().nivelActual = nivelActual;
   // SceneManager.LoadScene("Nivel" + nivelActual);
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
