using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Carta
{
	private Jogador jogador = Jogador.jogador;
	public string nome;
	public string descricao;
	public int custo;
	public int efeito = 0;
	public Color cor;

	public static string Formatar(string descricao, params Tuple<string, string>[] subs)
	{
		string final = descricao;
		foreach (var sub in subs)
		{
			final = final.Replace(sub.Item1, sub.Item2);
		}
		return final;
	}

	public Carta(string nome, string descricao, int custo, Color cor)
	{
		this.nome = nome;
		this.descricao = descricao;
		this.custo = custo;
		this.cor = cor;
	}

	public string AplicarFormatacao()
	{
		return descricao.Replace("[e]", efeito.ToString());
	}

	public virtual void AplicarEfeito(Plateia plateia)
	{}
}

