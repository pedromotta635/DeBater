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

	public void SetTexto()
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
