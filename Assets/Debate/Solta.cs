using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Eventos;

public class Solta : MonoBehaviour, IDropHandler
{
	private Jogador jogador = Jogador.jogador;
	[SerializeField]
	private Descarte descarte;
	[SerializeField]
	private Plateia plateia;

	public EventoCarta cartaJogada = new EventoCarta();

    public void OnDrop(PointerEventData eventData)
	{
		var objeto = eventData.pointerDrag.GetComponent<Arrastavel>();
		if (jogador.ValidarCarta(objeto.carta))
		{
			objeto.carta.AplicarEfeito(plateia);
			cartaJogada.Invoke(objeto.carta);
			//plateia.AlterarApoioPor(carta.efeito);
			Destroy(objeto.placeholder);
			descarte.AdicionarCarta(eventData.pointerDrag);
		}
	}
}
