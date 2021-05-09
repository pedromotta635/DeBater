using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

abstract public class Carta
{
	private readonly Jogador jogador = Jogador.jogador;

	public static enum Tipo { Argumento, ContraArgumento, Falacia };
	public abstract Tipo tipo { get; }
	
	public abstract bool jogavel { get; set; }
	public abstract string nome { get; set; }
	public abstract string descricao { get; set; }
	public abstract int custo { get; set; }
	public abstract string imagem { get; set;}

	public Color cor = Utils.amarelo;

	public abstract string Formatar();

	public abstract void AplicarEfeito(Plateia plateia);
}

abstract public class Argumento : Carta
{
	public override Tipo tipo { get; } = Tipo.Argumento;
	public override bool jogavel = true;
}

abstract public class ContraArgumento : Carta
{
	public override Tipo tipo { get; } = Tipo.ContraArgumento;
}

abstract public class Falacia : Carta
{
	public override Tipo tipo { get; } = Tipo.Falacia;
}


public class ArgumentoBasico : Argumento
{
	public override string nome { get; set; } = "Argumentar";
	public override int custo { get; set; } = 1;
	public override string descricao { get; set; } = "Convence a audiência em [e].";
	public int efeito { get; set; } = 20;

	public override string imagem { get; set; }
	public override void AplicarEfeito(Plateia plateia)
	{
		plateia.AlterarApoioPor(efeito);
	}

	public override string Formatar()
	{
		return descricao.Replace("[e]", efeito.ToString());
	}
}

public class ContraArgumentoBasico : ContraArgumento
{
	public override string nome = { get; set; } = "Réplica";
	public override int custo { get; set; } = 1;
	public override string descricao { get; set; } = "Contra-argumento por [e]";
	public int efeito = 10;
	
	public override void AplicarEfeito(Plateia plateia)
	{
		jogador.nivelContraArgumento += efeito;
	}

	public override string Formatar()
	{
		return descricao.Replace("[e]", efeito.ToString());
	}
}
