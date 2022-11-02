using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
  public int puntos = 0;
  public int vidas = 3;
  public int nivelActual = 1;
  public int ultimoNivel = 3;
  private void Awake()
  {
    int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
    if (gameStatusCount > 1)
    {
      Destroy(gameObject);
    }
    else
    {
      DontDestroyOnLoad(gameObject);
    }
  }
}
