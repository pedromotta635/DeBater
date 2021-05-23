using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Classe Responsável por:
 *   Realizar as ações para o funcionamento do estado
 *   Armazenar o estado do jogador no debate
 *   Realizar as ações do jogador no debate
 */
public class Jogador
{
	// A única instância do Jogador
	public static readonly Jogador jogador = new Jogador();

	private const int energiaInicial = 4;
	public int energiaPorTurno = energiaInicial;
	public int energia = energiaInicial;
	public const int cartasPorTurno = 5;

	// O nível de Contra-Argumento
	[SerializeField]
	private int _nivelContraArgumento = 0;
	public int nivelContraArgumento
	{
		get => _nivelContraArgumento;
		set { _nivelContraArgumento = value < 0 ? 0 : value; }
	}

	// Lista de cartas para serem usadas no debate
	public List<GameObject> baralho = new List<GameObject>();

	// Lista da representação das cartas no código
	public List<Carta> cartas;

	private Jogador()
	{
		cartas = new List<Carta> {
			new ArgumentoBasico(),
			new ArgumentoBasico(),
			new ArgumentoBasico(),
			new ArgumentoBasico(),
			new ArgumentoBasico(),
			new ContraArgumentoBasico(),
			new ContraArgumentoBasico()
		};
	}

	public void InicializarBaralho(List<GameObject> pilha, GameObject prefab)
	{
		foreach (Carta cartaInfo in cartas)
		{
			InicializarCarta(cartaInfo, prefab);
		}
		foreach (GameObject carta in baralho)
		{
			pilha.Add(carta);
		}
	}

	private void InicializarCarta(Carta cartaInfo, GameObject prefab)
	{
		GameObject cartaObjeto = GameObject.Instantiate(prefab);
		baralho.Add(cartaObjeto);
		cartaObjeto.GetComponent<CanvasRenderer>().SetColor(cartaInfo.cor);
		cartaObjeto.transform.Find("Nome").GetComponent<Text>().text = cartaInfo.nome;
		cartaObjeto.transform.Find("Descricao").GetComponent<Text>().text = cartaInfo.Formatar();
		cartaObjeto.GetComponent<Arrastavel>().carta = cartaInfo;
		cartaObjeto.transform.Find("Custo").GetComponent<Text>().text = cartaInfo.custo.ToString();
	}

	public bool ValidarCarta(Carta carta)
	{
		if (carta.jogavel && carta.custo <= energia)
		{
			energia -= carta.custo;
			return true;
		}
		return false;
	}

	public void NovoTurno()
	{
		energia = energiaPorTurno;
	}
}