using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Eventos;

public class Plateia : MonoBehaviour
{
	public EventoResultadoDebate debateTerminou = new EventoResultadoDebate();
	[SerializeField]
	private Text textoApoio;
	[SerializeField]
	private int _apoio = 0;
	public int apoio
	{
		get => _apoio;
		set
		{
			if (value > 100) _apoio = 100;
			else if (value < -100) _apoio = -100;
			else _apoio = value;
			_apoio = value >  100 ? 100
			       : value < -100 ? -100
				   : value;
			textoApoio.text = _apoio.ToString();
		}
	}

	public void AlterarApoioPor(int valor)
	{
		apoio += valor;
		transform.Find("Apoio").GetComponent<Text>().text = apoio.ToString();
		if (apoio >= 100)
		{
			debateTerminou.Invoke(ResultadoDebate.Vitoria);
		}
		else if (apoio <= -100)
		{
			debateTerminou.Invoke(ResultadoDebate.Derrota);
		}
	}
}
