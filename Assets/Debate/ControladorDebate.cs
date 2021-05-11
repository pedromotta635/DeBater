using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorDebate : MonoBehaviour
{
	public GameObject cartaPrefab;

	public List<GameObject> pilha;

	public Descarte descarte;

	private GameObject mao;
	public GameObject textoIA;
	public GameObject popup;
	public Button botaoTurno;

	private Plateia plateia;

	private int efeitoIA = -30;

	private Jogador jogador = Jogador.jogador;

	void Start()
	{
		textoIA.GetComponent<Text>().text = efeitoIA.ToString();

		mao = transform.Find("Mao").gameObject;
		plateia = transform.Find("Plateia").GetComponent<Plateia>();

		descarte = transform.Find("Descarte").GetComponent<Descarte>();
		
		jogador.InicializarBaralho(pilha, cartaPrefab);
		Utils.Embaralhar(pilha);
		DarCartas();
	}
	
	public void VirarCartas(int n)
	{
		int c = 0;
		for (int _ = 0; _ < c; _++)
		{
			pilha[0].transform.SetParent(mao.transform, false);
			pilha.RemoveAt(0);
		}
	}
	
	public void DarCartas()
	{
		int c = pilha.Count;
		if (c >= 4)
		{
			Debug.Log("if");
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else if (c == 0)
		{
			Debug.Log("elseif");
			foreach (GameObject carta in descarte.descarte)
			{
				pilha.Add(carta);
			}
			descarte.Esvaziar();
			Utils.Embaralhar(pilha);
			for (int _ = 0; _ < 4; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		else
		{
			for (int _ = 0; _ < c; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
			foreach (GameObject carta in descarte.descarte)
			{
				Debug.Log("A");
				pilha.Add(carta);
			}
			Debug.Log(pilha.Count);
			descarte.Esvaziar();
			for (int _ = 0; _ < 4 - c; _++)
			{
				pilha[0].transform.SetParent(mao.transform, false);
				pilha.RemoveAt(0);
			}
		}
		
	}
	public void AcaoIA()
	{
		plateia.AlterarApoioPor(efeitoIA);

	}

	public void Vitoria()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Vitória!";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}

	public void Derrota()
	{
		popup.transform.GetChild(0).GetComponent<Text>().text = "Vitória!";
		popup.SetActive(true);
		botaoTurno.interactable = false;
	}
}

