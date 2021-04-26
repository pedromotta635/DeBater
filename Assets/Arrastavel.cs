using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Arrastavel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public Transform destino;
	public GameObject placeholder;

	public int efeito, custo;

	public void OnBeginDrag(PointerEventData eventData)
	{
		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		var le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		le.preferredHeight = GetComponent<LayoutElement>().preferredHeight;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

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
		this.transform.SetParent(destino);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		Destroy(placeholder);
	}
}
