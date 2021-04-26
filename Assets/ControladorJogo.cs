using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Carta
{
	public string nome;
	public string descricao;
	public int efeito;
	public int custo;
	public Color cor;
	public Carta(string nome, string descricao, int efeito, int custo, Color cor)
	{
		this.nome = nome;
		this.descricao = descricao;
		this.efeito = efeito;
		this.custo = custo;
		this.cor = cor;
	}

	public string TextoDescricao()
	{
		return descricao.Replace("[e]", efeito.ToString());
	}
}

public class ControladorJogo : MonoBehaviour
{
	public GameObject cartaPrefab;

	public List<GameObject> baralho;
	public List<GameObject> pilha;

	public Descarte descarte;

	private GameObject mao;

	private Plateia plateia;

	private int efeitoIA = -30;

	void Start()
	{
		mao = transform.Find("Mao").gameObject;
		plateia = transform.Find("Plateia").GetComponent<Plateia>();

		descarte = transform.Find("Descarte").GetComponent<Descarte>();

		// Cria vetor de cartas no código
		Carta[] cartas = {
			new Carta("Carta 1", "Faz [e] de efeito.", 5, 1, Utils.verde),
			new Carta("Carta 2", "Faz [e] de efeito.", 5, 1, Utils.verde),
			new Carta("Carta 3", "Faz [e] de efeito.", 12, 2, Utils.vermelho),
			new Carta("Carta 4", "Faz [e] de efeito.", 5, 1, Utils.verde),
			new Carta("Carta 5", "Faz [e] de efeito.", 7, 1, Utils.verde),
			new Carta("Carta 6", "Faz [e] de efeito.", 12, 2, Utils.vermelho),
			new Carta("Carta 7", "Faz [e] de efeito.", 25, 3, Utils.azul),
			new Carta("Carta 8", "Faz [e] de efeito.", 5, 1, Utils.verde),
			new Carta("Carta 9", "Faz [e] de efeito.", 3, 0, Utils.amarelo),
			new Carta("Carta 10", "Faz [e] de efeito.", 2, 0, Utils.amarelo)
		};

		// Inicializa cartas no baralho
		for (int i = 0; i < 10; i++)
		{
			// Instantiate cria uma nova carta
			baralho.Add(Instantiate(cartaPrefab));
			// Texto
			baralho[i].GetComponent<CanvasRenderer>().SetColor(cartas[i].cor);
			baralho[i].transform.Find("Nome").GetComponent<Text>().text = cartas[i].nome;
			baralho[i].transform.Find("Descricao").GetComponent<Text>().text = cartas[i].TextoDescricao();
			baralho[i].GetComponent<Arrastavel>().efeito = cartas[i].efeito;
			baralho[i].GetComponent<Arrastavel>().custo = cartas[i].custo;
			baralho[i].transform.Find("Custo").GetComponent<Text>().text = cartas[i].custo.ToString();
		}

		// Coloca as cartas na pilha
		foreach (GameObject carta in baralho)
		{
			pilha.Add(carta);
		}
		Shuffle(pilha);
		/*
		foreach (GameObject carta in pilha)
		{
			Debug.Log($"{carta.transform.Find("Nome").GetComponent<Text>().text}: {carta.transform.Find("Descricao").GetComponent<Text>().text}");
		}
		*/
		DarCartas();
	}
	
	public void Shuffle<T>(IList<T> list)  
	{
		System.Random rng = new System.Random();
	    int n = list.Count;  
	    while (n > 1) {  
	    	n--;  
	        int k = rng.Next(n + 1);  
	        T value = list[k];  
	        list[k] = list[n];  
	        list[n] = value;  
	    }  
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
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else
		{
			Debug.Log($"else - {c} - {descarte.descarte.Count}");
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
		// Tira as cartas da pilha e as coloca na mao
		
	}
	public void AcaoIA()
	{
		plateia.AlterarApoioPor(efeitoIA);
	}
}

