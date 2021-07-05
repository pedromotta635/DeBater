using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cartas;
using IA;
using ctrl = ControladorDebate;

public class TextoIA : MonoBehaviour
{
	[SerializeField]
	private Text texto;

	void Start()
	{
		ctrl.instancia.ia.efeitoMudou.AddListener(SetTexto);
	}

	private void SetTexto(IEfeito efeito) => SetTexto(efeito.texto, efeito.tipo);

	public void SetTexto(string str, Carta.Tipo tipo)
	{
		Color cor;
		switch (tipo)
		{
			case Carta.Tipo.Argumento:
				cor = Utils.vermelho;
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
