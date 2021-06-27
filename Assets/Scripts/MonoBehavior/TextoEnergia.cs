using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoEnergia : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI texto;
	private Jogador jogador = Jogador.jogador;

	void Start()
	{
		jogador.energiaMudou.AddListener(SetTexto);
		SetTexto();
	}

	private void SetTexto(int _ = 0)
	{
		texto.text = $"<b>{jogador.energia}</b>({jogador.energiaPorTurno})";
	}

	public void SetTexto(int energiaAtual, int energiaMaxima)
	{
		texto.text = $"<b>{energiaAtual}</b>({energiaMaxima})";
	}
}
