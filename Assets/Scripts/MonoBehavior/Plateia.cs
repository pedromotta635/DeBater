using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Eventos;
using Controlador = ControladorDebate;

public class Plateia : MonoBehaviour
{
	public static Plateia instancia;
	private Jogador jogador = Jogador.jogador;
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
			_apoio = Mathf.Clamp(value, -100, 100);
			textoApoio.text = _apoio.ToString();
			if      (_apoio ==  100) debateTerminou.Invoke(ResultadoDebate.Vitoria);
			else if (_apoio == -100) debateTerminou.Invoke(ResultadoDebate.Derrota);
		}
	}

	void Awake()
	{
		instancia = this; 
	}

	/*
	public void AlterarApoioPor(int valor)
	{
		int v = valor;
		if (valor > 0)
		{
			int nca = Controlador.instancia.ia.nivelContraArgumento;
			if (nca > 0)
			{
				v -= nca;
				if (v > 0)
				{
					Controlador.instancia.ia.nivelContraArgumento = 0;
					apoio += v;
				}
				else
				{
					nca -= valor;
					int varAutoconfianca = 1 + nca / 2;
					jogador.autoconfianca -= varAutoconfianca;
					Controlador.instancia.ia.autoconfianca += varAutoconfianca;
					Controlador.instancia.ia.nivelContraArgumento = nca;
				}
			}
			else
			{
				apoio += v;
			}
		}
		else if (valor < 0)
		{
			int nca = jogador.nivelContraArgumento;
			if (nca > 0)
			{
				v -= nca;
				if (v > 0)
				{
					jogador.nivelContraArgumento = 0;
					apoio += v;
				}
				else
				{
					nca -= -valor;
					int varAutoconfianca = 1 + nca / 2;
					Controlador.instancia.ia.autoconfianca -= varAutoconfianca;
					jogador.autoconfianca += varAutoconfianca;
					jogador.nivelContraArgumento = nca;
				}
			}
			else
			{
				apoio += v;
			}
		}
	}

	/*/
	public void AlterarApoioPor(int valor)
	{
		apoio += valor;
		textoApoio.text = apoio.ToString();
	}
	//*/
}
