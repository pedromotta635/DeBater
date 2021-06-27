using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Cartas;
using IA;

using ctrl = ControladorDebate;


public class Arrastavel : MonoBehaviour, ITemTooltip, IDragHandler, IBeginDragHandler, IEndDragHandler,
                          IPointerEnterHandler, IPointerExitHandler
{
	public Transform destino;
	public GameObject placeholder;
	public Carta carta;
	private CanvasGroup cg;
	private LayoutElement le;

	[SerializeField]
	private TextMeshProUGUI textoNome;
	[SerializeField]
	private TextMeshProUGUI textoCusto;
	[SerializeField]
	private TextMeshProUGUI textoTipo;
	[SerializeField]
	private TextMeshProUGUI textoDescricao;

	public string titulo { get => carta.nome; }
	public string descricao
	{
		get => $"Isso é uma <i>carta</i>. Ela deve ser arrastada até o meio da tela, onde soltá-la causará ela a sair de sua mão e ir até a <i>pilha de descarte</i>. Só pode ser jogada caso você tenha <i>energia</i> o suficiente para cobrir o <i>custo</i>.\nCusto: {carta.custo}.\nDescrição: {carta.descricao}";
	}

	[SerializeField]
	private ControladorTooltip tooltip;

	private bool mouseEmCima = false;

	void Start()
	{
		tooltip.SetActive(false);
		cg = GetComponent<CanvasGroup>();
		le = GetComponent<LayoutElement>();
		ctrl.instancia.ia.efeitoMudou.AddListener(AtualizarEfeito);
	}

	private void AtualizarEfeito(IEfeito _)
	{
		carta.AtualizarEfeito(Plateia.instancia);

	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		tooltip.SetActive(false);
		placeholder = new GameObject();
		placeholder.transform.SetParent(this.transform.parent);
		var le = placeholder.AddComponent<LayoutElement>();
		le.preferredWidth = le.preferredWidth;
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

	private IEnumerator MostrarTooltip()
	{
		yield return new WaitForSeconds(0.5f);
		if (mouseEmCima) tooltip.SetActive(true);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseEmCima = true;
		StartCoroutine(MostrarTooltip());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseEmCima = false;
		tooltip.SetActive(false);
	}

}
