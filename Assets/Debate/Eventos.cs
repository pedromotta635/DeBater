using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cartas;
using IA;

namespace Eventos
{
	public enum ResultadoDebate { Derrota, Vitoria }
	[System.Serializable]
	public class EventoResultadoDebate : UnityEvent<ResultadoDebate> {}
	[System.Serializable]
	public class EventoCarta : UnityEvent<Carta> {}
	[System.Serializable]
	public class EventoInt : UnityEvent<int> {}
	[System.Serializable]
	public class EventoString : UnityEvent<string> {}
	[System.Serializable]
	public class EventoEfeito : UnityEvent<IEfeito> {}
}