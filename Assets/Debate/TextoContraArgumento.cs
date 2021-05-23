using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TextoContraArgumento : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI texto;

	public void SetTexto(int ca)
	{
		texto.text = ca.ToString();
	}
}
