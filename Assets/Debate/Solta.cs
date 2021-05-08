using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Solta : MonoBehaviour, IDropHandler
{
	private ControladorJogo controlador;
	private Jogador jogador = Jogador.jogador;
	private Descarte descarte;
	private Plateia plateia;

	public Arrastavel carta;

	void Start()
	{
		controlador = transform.parent.GetComponent<ControladorJogo>();
		plateia = transform.parent.Find("Plateia").GetComponent<Plateia>();
		descarte = transform.parent.Find("Descarte").GetComponent<Descarte>();
	}

    public void OnDrop(PointerEventData eventData)
	{
		carta = eventData.pointerDrag.GetComponent<Arrastavel>();
		if (jogador.ValidarCarta(carta))
		{
			plateia.AlterarApoioPor(carta.efeito);
			Destroy(carta.placeholder);
			descarte.AdicionarCarta(eventData.pointerDrag);
		}
	}
}
