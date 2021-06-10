using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Cartas;
using ctrl = ControladorDebate;
using IA;

public class Arrastavel : MonoBehaviour, ITemTooltip, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	public Transform destino;
	public GameObject placeholder;
	public Carta carta;
	private CanvasGroup cg;

	private Text textoDescricao;

	public string titulo { get => carta.nome; }
	public string descricao
	{
		get => $"Isso é uma <i>carta</i>. Ela deve ser arrastada até o meio da tela, onde soltá-la causará ela a sair de sua mão e ir até a <i>pilha de descarte</i>. Só pode ser jogada caso você tenha <i>energia</i> o suficiente para cobrir o <i>custo</i>.\nCusto: {carta.custo}.\nDescrição: {carta.descricao}";
	}

	[SerializeField]
	private GameObject prefabTooltip;
	private GameObject tooltip;

	private bool mouseEmCima = false;

	void Awake()
	{
		tooltip = Instantiate(prefabTooltip, transform.parent.parent);
		tooltip.SetActive(false);
	}

	void Start()
	{
		
		cg = GetComponent<CanvasGroup>();
		textoDescricao = transform.GetChild(2).transform.GetComponent<Text>();
		ctrl.instancia.ia.efeitoMudou.AddListener(AtualizarEfeito);
	}

	private void AtualizarEfeito(IEfeito _)
	{
		carta.AtualizarEfeito(Plateia.instancia);

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

	private IEnumerator MostrarTooltip()
	{
		yield return new WaitForSeconds(1.0f);
		if (mouseEmCima);
	}

	void OnMouseEnter()
	{
		mouseEmCima = true;
		tooltip.SetActive(true);
		//StartCoroutine(MostrarTooltip());
	}

	void OnMouseOver()
	{
		tooltip.SetActive(true);
	}

	void OnMouseExit()
	{
		mouseEmCima = false;
		tooltip.SetActive(false);
	}

}
