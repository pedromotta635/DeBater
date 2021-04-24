using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Arrastavel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public Transform destino;

	public GameObject placeholder;

	public int dano = 5;

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("beginDrag");

		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		var l = placeholder.AddComponent<LayoutElement>();
		l.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		l.preferredWidth = GetComponent<LayoutElement>().preferredHeight;
		l.flexibleWidth = 0;
		l.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

		destino = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{

		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		Debug.Log("endDrag");
		this.transform.SetParent(destino);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Destroy(placeholder);
	}
}
