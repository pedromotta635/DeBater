using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Cartas
{
	// Classe base de todas as cartas
	abstract public class Carta
	{
		// Tipo da carta
		public enum Tipo { Argumento, ContraArgumento, Falacia };

		public abstract Tipo tipo { get; }
		public abstract bool jogavel { get; set; }
		public abstract string nome { get; set; }
		public abstract string descricao { get; set; }
		public abstract int custo { get; set; }
		public abstract string imagem { get; }

		public Color cor = Utils.amarelo;



		// Função que aplica a formatação ao texto
		public abstract string Formatar();

		public abstract void AtualizarEfeito(Plateia plateia);

		// Função chamada quando a carta é jogada
		public abstract void AplicarEfeito(Plateia plateia);
	}

	// Classe base de todos os Argumentos
	abstract public class Argumento : Carta
	{
		public override Tipo tipo { get; } = Tipo.Argumento;
		public override bool jogavel { get; set; } = true;
	}

	// Classe base de todos os Contra-Argumentos
	abstract public class ContraArgumento : Carta
	{
		public override Tipo tipo { get; } = Tipo.ContraArgumento;
		public override bool jogavel { get; set; } = true;
	}

	// Classe base de todas as Falácias
	abstract public class Falacia : Carta
	{
		public override Tipo tipo { get; } = Tipo.Falacia;
		public override bool jogavel { get; set; } = true;
	}

	// Classes declaradas a partir daqui representam cartas individuais
	public class ArgumentoBasico : Argumento
	{
		public override string nome { get; set; } = "Argumentar";
		private string _descricao = "Convence a audiência em [e].";
		public override string descricao
		{
			get => _descricao.Replace("[e]", efeito.ToString());
			set { _descricao = value; }
		}
		public override string imagem { get; }
		public override int custo { get; set; } = 1;
		private const int efeitoInicial = 20;
		private int efeito = 20;

		public override void AplicarEfeito(Plateia plateia)
		{
			plateia.AlterarApoioPor(efeito);
		}

		public override void AtualizarEfeito(Plateia plateia)
		{
			efeito = efeitoInicial + Jogador.jogador.autoconfianca;
		}

		public override string Formatar()
		{
			return descricao.Replace("[e]", efeito.ToString());
		}
	}

	public class ContraArgumentoBasico : ContraArgumento
	{
		public override string nome { get; set; } = "Réplica";
		private string _descricao = "Contra - argumenta por [e].";
		public override string descricao
		{
			get => _descricao.Replace("[e]", efeito.ToString());
			set { _descricao = value; }
		}
		public override string imagem { get; }
		public override int custo { get; set; } = 1;
		private const int efeitoInicial = 10;
		private int efeito = efeitoInicial;
		
		public override void AplicarEfeito(Plateia plateia)
		{
			Jogador.jogador.nivelContraArgumento += efeito; // nivelContraArgumento.set(nivelContraArgumento + efeito)
		}

		public override void AtualizarEfeito(Plateia plateia)
		{
			efeito = efeitoInicial + Jogador.jogador.autoconfianca;
		}

		public override string Formatar()
		{
			return descricao.Replace("[e]", efeito.ToString());
		}
	}
}
