using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controlador : MonoBehaviour
{
	public List<GameObject> baralho;
	public GameObject cartaPrefab;

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
			baralho.add(Instantiate(cartaPrefab, transform.position, transform.rotation));
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
		return descricao.replace("[e]", efeito.toString());
	}
}