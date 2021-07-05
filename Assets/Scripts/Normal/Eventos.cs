using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using Cartas;
using IA;

namespace Eventos
{
	public enum ResultadoDebate { Derrota, Vitoria }
	[Serializable]
	public class EventoResultadoDebate : UnityEvent<ResultadoDebate> {}
	[Serializable]
	public class EventoCarta : UnityEvent<Carta> {}
	[Serializable]
	public class EventoInt : UnityEvent<int> {}
	[Serializable]
	public class EventoString : UnityEvent<string> {}
	[Serializable]
	public class EventoEfeito : UnityEvent<IEfeito> {}

	public static class GerenciadorEventos
	{
		public static readonly EventoInt energiaMudou = new EventoInt();
	}
}