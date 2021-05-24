using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Eventos;

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
	[SerializeField]
	private TextoEnergia textoEnergia;

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
		plateia.debateTerminou.AddListener(FimDeDebate);
		ia.NovoEfeito();
		textoIA.SetTexto(ia.efeitoAtual.texto, ia.efeitoAtual.tipo);
		jogador.InicializarBaralho(pilha, cartaPrefab);
		Utils.Embaralhar(pilha);
		DarCartas();
	}

	void Update()
	{
		textoContraArgumentoJogador.SetTexto(jogador.nivelContraArgumento);
		textoEnergia.SetTexto(jogador.energia, jogador.energiaPorTurno);
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
	public void DarCartas() => VirarCartas(Jogador.cartasPorTurno);
	
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

	private void FimDeDebate(ResultadoDebate res)
	{
		switch (res)
		{
			case ResultadoDebate.Vitoria:
				Vitoria();
				break;
			case ResultadoDebate.Derrota:
				Derrota();
				break;
		}
	}

	private void Vitoria()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Vitória!";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}

	private void Derrota()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Derrota";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}
}

