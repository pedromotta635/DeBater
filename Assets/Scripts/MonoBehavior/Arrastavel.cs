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
		get => $"Isso é uma <b>carta</b>. Ela deve ser arrastada até o meio da tela, onde soltá-la causará ela a sair de sua mão e ir até a <b>pilha de descarte</b>. Só pode ser jogada caso você tenha <b>tempo</b> o suficiente para cobrir o <b>custo</b>.\nCusto: {carta.custo}.\nDescrição: {carta.descricao}";
	}

	[SerializeField]
	private ControladorTooltip tooltip;

	private bool mouseEmCima = false;

	void Start()
	{
		tooltip.dono = this;
		tooltip.SetActive(false);
		cg = GetComponent<CanvasGroup>();
		le = GetComponent<LayoutElement>();
		ctrl.instancia.ia.efeitoMudou.AddListener(AtualizarEfeito);
	}

	void Update()
	{
		textoNome.text = carta.nome;
		textoDescricao.text = carta.descricao;
		textoCusto.text = carta.custo.ToString();
		textoTipo.text = carta.tipo.ComoString();
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
		le.preferredWidth = this.le.preferredWidth;
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

	public void OnPointerEnter(PointerEventData eventData)
	{
		mouseEmCima = true;
		StartCoroutine(MostrarTooltip());
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		mouseEmCima = false;
		StartCoroutine(EsconderTooltip());
	}

	private IEnumerator MostrarTooltip()
	{
		yield return new WaitForSeconds(0.2f);
		if (mouseEmCima) tooltip.SetActive(true);
	}

	private IEnumerator EsconderTooltip()
	{
		yield return new WaitForSeconds(0.2f);
		if (!mouseEmCima) tooltip.SetActive(false);
	}
}
