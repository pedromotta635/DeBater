using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Eventos;

public class TextoEnergia : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI texto;

	void Start()
	{
		GerenciadorEventos.energiaMudou.AddListener(SetTexto);
		SetTexto();
	}

	private void SetTexto(int _ = 0) => SetTexto(Jogador.jogador.energia, Jogador.jogador.energiaPorTurno);

	public void SetTexto(int energiaAtual, int energiaMaxima)
	{
		texto.text = $"<b>{energiaAtual}</b>({energiaMaxima})";
	}
}
