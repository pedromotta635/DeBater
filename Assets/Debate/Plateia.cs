using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plateia : MonoBehaviour
{
	private int _apoio = 0;
	public int apoio
	{
		get => _apoio;
		set
		{
			if (value > 100) _apoio = 100;
			else if (value < -100) _apoio = -100;
			else _apoio = value;
		}
	}
	private ControladorJogo controlador;

	void Start()
	{
		controlador = transform.parent.GetComponent<ControladorJogo>();
	}

	public void AlterarApoioPor(int valor)
	{
		apoio += valor;
		transform.Find("Apoio").GetComponent<Text>().text = apoio.ToString();
		if (apoio >= 100)
		{
			controlador.Vitoria();
		}
	}
}
