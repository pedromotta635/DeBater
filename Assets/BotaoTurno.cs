using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotaoTurno : MonoBehaviour
{
	private Descarte descarte;

	private ControladorJogo controlador;

	void Start()
	{
		controlador = transform.parent.GetComponent<ControladorJogo>();
		descarte = transform.parent.Find("Descarte").GetComponent<Descarte>();
	}

	public void OnButtonPress()
	{
		Debug.Log("turno");
		descarte.RetirarCartas();
		controlador.AcaoIA();
		controlador.DarCartas();
	}
}
