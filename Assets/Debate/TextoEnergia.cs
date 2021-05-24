using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoEnergia : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI texto;

	public void SetTexto(int energiaAtual, int energiaMaxima)
	{
		texto.text = $"<b>{energiaAtual}</b>({energiaMaxima})";
	}
}
