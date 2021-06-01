using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Arrastavel : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public Transform destino;
	public GameObject placeholder;
	public Carta carta;
	private CanvasGroup cg;

	void Start()
	{
		cg = GetComponent<CanvasGroup>();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		var le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = GetComponent<LayoutElement>().preferredWidth;
		le.flexibleWidth = 0;
		le.flexibleHeight = 0;

		placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

		destino = this.transform.parent;
		this.transform.SetParent(this.transform.parent.parent);
		cg.blocksRaycasts = false;
	}

	public void OnDrag(PointerEventData eventData)
	{

		this.transform.position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.SetParent(destino);
		this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
		cg.blocksRaycasts = true;
		Destroy(placeholder);
	}
}
