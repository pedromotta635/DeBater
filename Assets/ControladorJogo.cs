using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ControladorJogo : MonoBehaviour
{
	public GameObject cartaPrefab;
	public List<GameObject> baralho;
	public List<GameObject> pilha;

	void Start()
	{
		Carta[] cartas = {
			new Carta("Carta 1", "Faz [e] de efeito.", 5, 1),
			new Carta("Carta 2", "Faz [e] de efeito.", 5, 1),
			new Carta("Carta 3", "Faz [e] de efeito.", 12, 2),
			new Carta("Carta 4", "Faz [e] de efeito.", 5, 1),
			new Carta("Carta 5", "Faz [e] de efeito.", 7, 1),
			new Carta("Carta 6", "Faz [e] de efeito.", 12, 2),
			new Carta("Carta 7", "Faz [e] de efeito.", 25, 3),
			new Carta("Carta 8", "Faz [e] de efeito.", 5, 1),
			new Carta("Carta 9", "Faz [e] de efeito.", 3, 0),
			new Carta("Carta 10", "Faz [e] de efeito.", 2, 0)
		};

		for (int i = 0; i < 10; i++)
		{
			baralho.Add(Instantiate(cartaPrefab));
			baralho[i].transform.Find("Nome").GetComponent<Text>().text = cartas[i].nome;
			baralho[i].transform.Find("Descricao").GetComponent<Text>().text = cartas[i].TextoDescricao();
		}

		foreach (GameObject carta in baralho)
		{
			Debug.Log(carta.transform.Find("Descricao").GetComponent<Text>().text);
			pilha.Add(carta);
		}
		Shuffle(pilha);
		foreach (GameObject carta in pilha)
		{
			Debug.Log($"{carta.transform.Find("Nome").GetComponent<Text>().text}: {carta.transform.Find("Descricao").GetComponent<Text>().text}");
		}
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
}

class Carta
{
	public string nome;
	public string descricao;
	public int efeito;
	public int custo;
	public Carta(string nome, string descricao, int efeito, int custo)
	{
		this.nome = nome;
		this.descricao = descricao;
		this.efeito = efeito;
		this.custo = custo;
	}

	public string TextoDescricao()
	{
		return descricao.Replace("[e]", efeito.ToString());
	}
}