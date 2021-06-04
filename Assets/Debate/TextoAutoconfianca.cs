using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ctrl = ControladorDebate;

public class TextoAutoconfianca : MonoBehaviour
{
	private TextMeshProUGUI texto;

	public DonoTexto dono;

	void Start()
	{
		texto = GetComponent<TextMeshProUGUI>();
		StartCoroutine(LateStart());
	}

	private IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame();
		switch (dono)
		{
			case DonoTexto.Jogador:
				Jogador.jogador.autoconfiancaMudou.AddListener(SetTexto);
				break;
			case DonoTexto.IA:
				ctrl.instancia.ia.autoconfiancaMudou.AddListener(SetTexto);
				break;
		}
	}
	public void SetTexto(int autoconfianca)
	{
		texto.text = $"AC: <b>{autoconfianca}";
	}
}
