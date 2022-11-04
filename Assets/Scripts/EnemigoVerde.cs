using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoVerde : Enemigo
{

  void Start()
  {
    base.Start();
    TipoNave = 3;
    Puntos = 30;
  }


}
