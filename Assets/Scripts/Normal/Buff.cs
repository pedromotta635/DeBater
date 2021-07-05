using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IA;
using Cartas;
using Eventos;

namespace Buffs
{
	public interface IBuffTemporario
	{
		int turnos { get; set; }
	}

	public abstract class Buff
	{
		public enum Tipo { Aditivo, Multiplicativo, Misc }
		
		

		public abstract Tipo tipo { get; }
		public abstract string texto { get; }
		public virtual string imagem { get; }
	}

	public abstract class BuffAditivo : Buff
	{
		protected const int valorMaximo = 99;
		protected const int valorMinimo = -99;

		public override Tipo tipo { get; } = Tipo.Aditivo;
		public override string texto { get => valor.ToString(); }
		public abstract Carta.Tipo[] cartasAplicaveis { get; }

		private int _valor = 0;
		public virtual int valor
		{
			get => _valor; 
			set => _valor = value <= valorMinimo ? valorMinimo
			              : value >= valorMaximo ? valorMaximo
			              : value;
		}
		public BuffAditivo(int valor)
		{
			this.valor = valor;
		}
	}

	public class Carisma : BuffAditivo
	{
		public override Carta.Tipo[] cartasAplicaveis { get; } = {
			Carta.Tipo.Argumento,
			Carta.Tipo.Falacia,
		};

		public Carisma(int valor) : base(valor)
		{}
	}

	public class Erudicao : BuffAditivo
	{
		public override Carta.Tipo[] cartasAplicaveis { get; } = {
			Carta.Tipo.Argumento,
		};

		public Erudicao(int valor) : base(valor)
		{}
	}

	public class Obscurantismo : BuffAditivo
	{
		public override Carta.Tipo[] cartasAplicaveis { get; } = {
			Carta.Tipo.Falacia,
		};

		public Obscurantismo(int valor) : base(valor)
		{}
	}

	public class Nervosismo : BuffAditivo, IBuffTemporario
	{
		private int _turnos;
		public int turnos
		{
			get => _turnos;
			set => Math.Max(value, 0);
		}

		public override Carta.Tipo[] cartasAplicaveis { get; } = {
			Carta.Tipo.Argumento,
			Carta.Tipo.Falacia,
		};
		
		public Nervosismo(int valor, int turnos) : base(valor)
		{
			ControladorDebate.instancia.turnoTerminou.AddListener(() => turnos--);
			this.turnos = turnos;
		}
	}

	public class Raiva : BuffAditivo, IBuffTemporario
	{
		private int _turnos;
		public int turnos
		{
			get => _turnos;
			set => Math.Max(value, 0);
		}

		public override Carta.Tipo[] cartasAplicaveis { get; } = {
			Carta.Tipo.Argumento,
			Carta.Tipo.Falacia,
		};
		
		public Raiva(int valor, int turnos) : base(valor)
		{
			ControladorDebate.instancia.turnoTerminou.AddListener(() => turnos--);
			this.turnos = turnos;
		}
	}
}