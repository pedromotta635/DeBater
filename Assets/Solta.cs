using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Solta : MonoBehaviour, IDropHandler
{
	private int _vida;
	public int vida
	{
		get
		{
			return _vida;
		}
		set
		{
			if (value < 0) _vida = 0;
			else _vida = value;
		}
	}

	public Arrastavel carta;

	void Start()
	{
		vida = 10;
	}

    public void OnDrop(PointerEventData eventData)
	{
		Debug.Log($"drop {gameObject.name}");
		carta = eventData.pointerDrag.GetComponent<Arrastavel>();
		vida -= carta.dano;
		Destroy(carta.placeholder);
		Destroy(eventData.pointerDrag);
		if (vida == 0)
		{
			transform.GetChild(0).Rotate(00.0f, 00.0f, 90.0f);
		}
	}
}
