using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descarte : MonoBehaviour
{
	public List<GameObject> descarte = new List<GameObject>();
	
	[SerializeField]
	private GameObject mao;

	public void RetirarCartas()
	{
		for (int i = mao.transform.childCount - 1; i >= 0; i--)
		{
			descarte.Add(mao.transform.GetChild(i).gameObject);
			mao.transform.GetChild(i).position = this.transform.position;
			mao.transform.GetChild(i).SetParent(this.transform);
		}
	}

	public void AdicionarCarta(GameObject carta)
	{
		descarte.Add(carta);
		carta.GetComponent<Arrastavel>().destino = transform;
		carta.transform.position = this.transform.position;
	}

	public void Esvaziar() => descarte.Clear();
}
