using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Eventos
{
	public enum ResultadoDebate { Derrota, Vitoria }
	[System.Serializable]
	public class EventoResultadoDebate : UnityEvent<ResultadoDebate>
	{}
	[System.Serializable]
	public class EventoCarta : UnityEvent<Carta>
	{}
}