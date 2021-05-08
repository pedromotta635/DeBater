using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plateia : MonoBehaviour
{
	private int _apoio = 0;
	public int apoio
	{
		get
		{
			return _apoio;
		}
		set
		{
			if (value <= 100 && value >= -100) _apoio = value;
		}
	}

	public void AlterarApoioPor(int valor)
	{
		apoio += valor;
		transform.Find("Apoio").GetComponent<Text>().text = apoio.ToString();
	}
}
