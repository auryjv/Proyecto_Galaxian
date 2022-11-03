using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Waiting : MonoBehaviour
{
  [SerializeField] float velocidadX;
  [SerializeField] GameObject linea1;
  [SerializeField] GameObject linea2;
  [SerializeField] GameObject linea3;
  [SerializeField] GameObject linea4;
  [SerializeField] GameObject fila1;
  [SerializeField] GameObject fila2;
  [SerializeField] GameObject fila3;
  [SerializeField] GameObject fila4;
  [SerializeField] GameObject gameOver;
  [SerializeField] GameObject push;
  float delayGameOver = 3.0f;
  float delayLinea1 = 5.0f;
  float delayLinea2 = 6.0f;
  float delayLinea3 = 7.0f;
  float delayLinea4 = 8.0f;
  float delayFilas = 9.0f;
  float delayPush = 30.0f;
  float initX;

  public void LanzarJuego()
  {
    SceneManager.LoadScene("nivel1");
  }

  void Start()
    {
     push.SetActive(false);
     gameOver.SetActive(true);
     linea1.SetActive(false);
     linea2.SetActive(false);
     linea3.SetActive(false);
     linea4.SetActive(false);
     initX = fila1.transform.position.x;
     float x = initX + 200.0f;
     fila1.transform.Translate(x, 0, 0);
     fila2.transform.Translate(x, -5, 0);
     fila3.transform.Translate(x, -10, 0);
     fila4.transform.Translate(x, -15, 0);
  }

    void Update()
    {
      delayPush -= Time.deltaTime;
      delayGameOver -= Time.deltaTime;
      delayLinea1 -= Time.deltaTime;
      delayLinea2 -= Time.deltaTime;
      delayLinea3 -= Time.deltaTime;
      delayLinea4 -= Time.deltaTime;
      delayFilas  -= Time.deltaTime;
    if (delayGameOver <= 0)
      gameOver.SetActive(false);
    if (delayLinea1 <= 0)
        linea1.SetActive(true);
      if (delayLinea2 <= 0)
        linea2.SetActive(true);
      if (delayLinea3 <= 0)
        linea3.SetActive(true);
      if (delayLinea4 <= 0)
        linea4.SetActive(true);
      if (delayFilas < 0)
        {
          if(fila1.transform.position.x > initX)
            fila1.transform.Translate(-fila1.transform.position.x * velocidadX * Time.deltaTime, 0, 0);
          else if (fila2.transform.position.x > initX)
            fila2.transform.Translate(-fila2.transform.position.x * velocidadX * Time.deltaTime, 0, 0);
          else if (fila3.transform.position.x > initX)
            fila3.transform.Translate(-fila3.transform.position.x * velocidadX * Time.deltaTime, 0, 0);
          else if (fila4.transform.position.x > initX)
            fila4.transform.Translate(-fila4.transform.position.x * velocidadX * Time.deltaTime, 0, 0);
        }
    if (delayPush < 0)
    {
      linea1.SetActive(false);
      linea2.SetActive(false);
      linea3.SetActive(false);
      linea4.SetActive(false);
      fila1.SetActive(false);
      fila2.SetActive(false);
      fila3.SetActive(false);
      fila4.SetActive(false);
      push.SetActive(true);
      if (Input.GetButtonDown("Fire1"))
      {
        LanzarJuego();
      }
    }

  }
  
}
