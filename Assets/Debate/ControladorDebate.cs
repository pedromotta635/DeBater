using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Classe responsável por controlar o funcionamento de um debate
public class ControladorDebate : MonoBehaviour
{
	public static ControladorDebate instancia;

	public GameObject cartaPrefab;

	public List<GameObject> pilha;

	[SerializeField]
	private Descarte descarte;

	public GameObject mao;
	[SerializeField]
	private TextoIA textoIA;
	[SerializeField]
	private TextoContraArgumento textoContraArgumentoJogador;
	[SerializeField]
	private TextoContraArgumento textoContraArgumentoIA;
	public GameObject popup;
	public Button botaoTurno;

	[SerializeField]
	private Plateia plateia;
	private Ia ia = new IaBasica();

	private Jogador jogador = Jogador.jogador;

	void Awake()
	{
		instancia = this;
	}

	void Start()
	{
		ia.NovoEfeito();
		textoIA.SetTexto(ia.efeitoAtual.texto, ia.efeitoAtual.tipo);
		//textoIA.GetComponent<Text>().text = ia.texto;
		jogador.InicializarBaralho(pilha, cartaPrefab);
		Utils.Embaralhar(pilha);
		DarCartas();
	}

	void Update()
	{
		textoContraArgumentoJogador.SetTexto(jogador.nivelContraArgumento);
	}

	private void EsvaziarDescarte()
	{
		foreach (GameObject carta in descarte.descarte)
		{
			pilha.Add(carta);
		}
		descarte.Esvaziar();
		Utils.Embaralhar(pilha);
	}

	private void VirarCarta()
	{
		pilha[0].transform.SetParent(mao.transform, false);
		pilha.RemoveAt(0);
	}
	
	public void VirarCartas(int n)
	{
		int c;
		for (c = 0; c < n; c++)
		{
			if (pilha.Count == 0)
			{
				if (descarte.descarte.Count > 0) EsvaziarDescarte();
				else break;
			}
			VirarCarta();
		}
	}
	//*
	public void DarCartas() => VirarCartas(Jogador.cartasPorTurno);  
	/*/
	public void DarCartas()
	{
		int c = pilha.Count;
		if (c >= 4)
		{
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else if (c == 0)
		{
			foreach (GameObject carta in descarte.descarte)
			{
				pilha.Add(carta);
			}
			descarte.Esvaziar();
			Utils.Embaralhar(pilha);
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else
		{
			for (int _ = 0; _ < c; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
			foreach (GameObject carta in descarte.descarte)
			{
				pilha.Add(carta);
			}
			descarte.Esvaziar();
			for (int _ = 0; _ < 4 - c; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}	
	}
	*/

	public void AcabarTurno()
	{
		descarte.RetirarCartas();
		AcaoIA();
		DarCartas();
		jogador.NovoTurno();
	}

	public void AcaoIA()
	{
		ia.Acao(plateia);
		ia.NovoEfeito();
		textoIA.SetTexto(ia.efeitoAtual.texto, ia.efeitoAtual.tipo);
	}

	public void Vitoria()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Vitória!";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}

	public void Derrota()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Derrota";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}
}

