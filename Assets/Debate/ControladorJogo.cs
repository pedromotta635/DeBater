using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Jogador
{
	public static readonly Jogador jogador = new Jogador();

	private const int energiaInicialImutavel = 4;
	public int energiaInicial = energiaInicialImutavel;
	public int energia = energiaInicialImutavel;

	public List<GameObject> baralho = new List<GameObject>();

	public Jogador()
	{}

	public void InicializarBaralho(Carta[] cartas, List<GameObject> pilha, GameObject prefab)
	{
		foreach (Carta cartaInfo in cartas)
		{
			InicializarCarta(cartaInfo, prefab);
		}
		foreach (GameObject carta in baralho)
		{
			pilha.Add(carta);
		}
	}

	private void InicializarCarta(Carta cartaInfo, GameObject prefab)
	{
		GameObject cartaObjeto = GameObject.Instantiate(prefab);
		baralho.Add(cartaObjeto);
		cartaObjeto.GetComponent<CanvasRenderer>().SetColor(cartaInfo.cor);
		cartaObjeto.transform.Find("Nome").GetComponent<Text>().text = cartaInfo.nome;
		cartaObjeto.transform.Find("Descricao").GetComponent<Text>().text = cartaInfo.AplicarFormatacao();
		cartaObjeto.GetComponent<Arrastavel>().efeito = cartaInfo.efeito;
		cartaObjeto.GetComponent<Arrastavel>().custo = cartaInfo.custo;
		cartaObjeto.transform.Find("Custo").GetComponent<Text>().text = cartaInfo.custo.ToString();
	}

	public bool ValidarCarta(Arrastavel carta)
	{
		if (carta.custo <= energia)
		{
			energia -= carta.custo;
			return true;
		}
		return false;
	}
}

public class ControladorJogo : MonoBehaviour
{
	public GameObject cartaPrefab;

	public List<GameObject> pilha;

	public Descarte descarte;

	private GameObject mao;
	public GameObject textoIA;

	private Plateia plateia;

	private int efeitoIA = -30;

	private Jogador jogador = Jogador.jogador;

	void Start()
	{
		textoIA.GetComponent<Text>().text = efeitoIA.ToString();

		mao = transform.Find("Mao").gameObject;
		plateia = transform.Find("Plateia").GetComponent<Plateia>();

		descarte = transform.Find("Descarte").GetComponent<Descarte>();

		// Cria vetor de cartas no código
		Carta[] cartas = {
			new Carta("Carta 1", "Faz [e] de efeito.", 1, Utils.verde),
			new Carta("Carta 2", "Faz [e] de efeito.", 1, Utils.verde),
			new Carta("Carta 3", "Faz [e] de efeito.", 2, Utils.vermelho),
			new Carta("Carta 4", "Faz [e] de efeito.", 1, Utils.verde),
			new Carta("Carta 5", "Faz [e] de efeito.", 1, Utils.verde),
			new Carta("Carta 6", "Faz [e] de efeito.", 2, Utils.vermelho),
			new Carta("Carta 7", "Faz [e] de efeito.", 3, Utils.azul),
			new Carta("Carta 8", "Faz [e] de efeito.", 1, Utils.verde),
			new Carta("Carta 9", "Faz [e] de efeito.", 0, Utils.amarelo),
			new Carta("Carta 10", "Faz [e] de efeito.", 0, Utils.amarelo)
		};
		jogador.InicializarBaralho(cartas, pilha, cartaPrefab);
		Utils.Embaralhar(pilha);
		DarCartas();
	}
	
	
	public void DarCartas()
	{
		int c = pilha.Count;
		if (c >= 4)
		{
			Debug.Log("if");
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else if (c == 0)
		{
			Debug.Log("elseif");
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
				Debug.Log("A");
				pilha.Add(carta);
			}
			Debug.Log(pilha.Count);
			descarte.Esvaziar();
			for (int _ = 0; _ < 4 - c; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		
	}
	public void AcaoIA()
	{
		plateia.AlterarApoioPor(efeitoIA);

	}
}

