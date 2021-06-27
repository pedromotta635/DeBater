using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControladorTooltip : MonoBehaviour
{
	[SerializeField]
	private TextMeshProUGUI titulo;
	[SerializeField]
	private TextMeshProUGUI descricao;

	public ITemTooltip dono;

	void Update()
	{
		SetTexto();
	}

	public void SetActive(bool value)
	{
		gameObject.SetActive(value);
		SetTexto();
	}

	private void SetTexto()
	{
		titulo.text = dono.titulo;
		descricao.text = dono.descricao;
	}
}

public interface ITemTooltip
{
	string titulo { get; }
	string descricao { get; }

}
