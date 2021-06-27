using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ctrl = ControladorDebate;

public class TextoContraArgumento : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI texto;

	public DonoTexto dono;

	void Start()
	{
		StartCoroutine(LateStart());
	}

	private IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame();
		switch (dono)
		{
			case DonoTexto.Jogador:
				Jogador.jogador.contraArgumentoMudou.AddListener(SetTexto);
				break;
			case DonoTexto.IA:
				ctrl.instancia.ia.contraArgumentoMudou.AddListener(SetTexto);
				break;
		}
	}

	public void SetTexto(int ca)
	{
		texto.text = ca.ToString();
	}
}
