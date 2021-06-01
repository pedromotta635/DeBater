using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextoAutoconfianca : MonoBehaviour
{
	private TextMeshProUGUI texto;

	void Start()
	{
		texto = GetComponent<TextMeshProUGUI>();
	}

	public void SetTexto(int autoconfianca)
	{
		texto.text = $"AC: <b>{autoconfianca}";
	}
}
