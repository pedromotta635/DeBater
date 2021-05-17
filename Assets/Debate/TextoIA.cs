using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextoIA : MonoBehaviour
{
	[SerializeField]
	private Text texto;

	public void SetTexto(string str, Carta.Tipo tipo)
	{
		Color cor;
		switch (tipo)
		{
			case Carta.Tipo.Argumento:
				cor = Utils.vermelho;
				break;
			case Carta.Tipo.ContraArgumento:
				cor = Utils.azul;
				break;
			case Carta.Tipo.Falacia:
				cor = Utils.amarelo;
				break;
			default:
				cor = Utils.branco;
				break;
		}
		texto.text = str;
		texto.color = cor;
	}
}
