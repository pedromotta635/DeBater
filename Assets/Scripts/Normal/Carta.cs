using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Buffs;

namespace Cartas
{
	// Classe base de todas as cartas
	abstract public class Carta
	{
		// Tipo da carta
		public enum Tipo { Argumento, Falacia, Qualidade };

		public abstract Tipo tipo { get; }
		public abstract bool jogavel { get; set; }
		public abstract string nome { get; set; }
		public abstract string descricao { get; }
		public abstract int custo { get; set; }
		public virtual string imagem { get; }

		public Color cor = Utils.amarelo;

		public Carta()
		{}

		public abstract void AtualizarEfeito(Plateia plateia);

		// Função chamada quando a carta é jogada
		public abstract void AplicarEfeito(Plateia plateia);

		protected abstract string DescricaoFormatada(string descricao);
	}

	abstract public class Qualidade : Carta
	{
		public override Tipo tipo { get; } = Tipo.Argumento;
		public override bool jogavel { get; set; } = true;

		public override void AtualizarEfeito(Plateia plateia)
		{}
	}

	// Classe base de todos os Argumentos
	abstract public class Argumento : Carta
	{
		public override Tipo tipo { get; } = Tipo.Argumento;
		public override bool jogavel { get; set; } = true;
		public int efeitoInicial { get; protected set; }
		public virtual int efeito { get => GetEfeitoReal(efeitoInicial); }

		public Argumento() : base()
		{
			efeitoInicial = 0;
		}

		public Argumento(int efeito) : this()
		{
			this.efeitoInicial = efeito;
		}

		public override void AtualizarEfeito(Plateia plateia)
		{}
		
		protected override string DescricaoFormatada(string descricao)
		{
			var sb = new StringBuilder(descricao);
			sb.Replace("[E]", efeito.ToString());
			sb.Replace("[e]", (efeito / 3).ToString());
			return sb.ToString();
		}

		protected virtual int GetEfeitoReal(int inicial)
		{
			if (Jogador.jogador == null) Debug.Log("FFFFF");
			int efeitoReal = inicial;
			foreach (var buff in Jogador.jogador.buffs)
			{
				if (buff is BuffAditivo
				    && Array.Exists((buff as BuffAditivo).cartasAplicaveis, x => x == this.tipo))
				{
					switch (buff.tipo)
					{
					case Buff.Tipo.Aditivo:
						efeitoReal += (buff as BuffAditivo).valor;
						break;
					case Buff.Tipo.Multiplicativo:
						break;
					}
				}
			}
			return efeitoReal;
		}

		public override void AplicarEfeito(Plateia plateia)
		{
			plateia.AlterarApoioPor(efeito);
		}
	}

	// Classe base de todas as Falácias
	abstract public class Falacia : Argumento
	{
		public override Tipo tipo { get; } = Tipo.Falacia;
	}

	/*/ Classes declaradas a partir daqui representam cartas individuais
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
			base.AtualizarEfeito(Plateia.instancia);
			efeito = efeitoInicial;
		}
		}*/

	public class CausaConsequencia : Argumento
	{
		public override string nome { get; set; } = "Causa e Consequência";
		private const string _descricao = "Argumenta por <b>[E]</b>/<b>[e]</b>.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 1;

		public CausaConsequencia()
		{
			efeitoInicial = 7;
		}
	}

	public class OpiniaoEspecialista : Argumento
	{
		public override string nome { get; set; } = "Opinião de Especialista";
		private const string _descricao = "Argumenta por <b>[E]</b>/<b>[e]</b>. Aplica 2 de <b>Nervosismo</b> por <b>2</b> turnos.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 1;

		public OpiniaoEspecialista()
		{
			efeitoInicial = 5;
		}

		public override void AplicarEfeito(Plateia plateia)
		{
			base.AplicarEfeito(plateia);
			//Jogador.jogador.buffs.Add(new Nervosismo(2, 2));
		}
	}

	public class Analogia : Argumento
	{
		public override string nome { get; set; } = "Analogia";
		private const string _descricao = "Argumenta por <b>[E]</b>/<b>[e]</b>.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 2;

		public Analogia()
		{
			efeitoInicial = 14;
		}
	}

	public class ApeloAutoridade : Falacia
	{
		public override string nome { get; set; } = "Apelo à Autoridade";
		private const string _descricao = "Argumenta por <b>[e]</b>/<b>[E]</b>.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 1;

		public ApeloAutoridade()
		{
			efeitoInicial = 7;
		}
	}

	public class AdHominem : Falacia
	{
		public override string nome { get; set; } = "Ad-Hominem";
		private const string _descricao = "Argumenta por <b>[e]</b>/<b>[E]</b>. Aplica <b>2</b> de <b>Raiva</b> por <b>2</b> turnos.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 1;

		public AdHominem()
		{
			efeitoInicial = 5;
		}

		public override void AplicarEfeito(Plateia plateia)
		{
			base.AplicarEfeito(plateia);
			//Jogador.jogador.buffs.Add(new Raiva(2, 2));
		}
	}

	public class FakeNews : Falacia
	{
		public override string nome { get; set; } = "Fake News";
		private const string _descricao = "Argumenta por <b>[e]</b>/<b>[E]</b>.";
		public override string descricao { get => DescricaoFormatada(_descricao); }
		public override int custo { get; set; } = 2;

		public FakeNews()
		{
			efeitoInicial = 14;
		}
	}
}
