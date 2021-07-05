using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Eventos;
using Cartas;
using Buffs;

public interface IDebatedor
{}

/*
 * Classe Responsável por:
 *   Realizar as ações para o funcionamento do estado
 *   Armazenar o estado do jogador no debate
 *   Realizar as ações do jogador no debate
 */
public class Jogador : IDebatedor
{
	// A única instância do Jogador
	public static readonly Jogador jogador = new Jogador();

	private const int energiaInicial = 3;
	public int energiaPorTurno = energiaInicial;
	private int _energia = energiaInicial;
	public int energia
	{
		get => _energia;
		set
		{
			_energia = value > 0 ? value : 0;
			GerenciadorEventos.energiaMudou.Invoke(_energia);
		}
	}
	public const int cartasPorTurno = 5;

	public List<Buff> buffs = new List<Buff>();

	// Lista de cartas para serem usadas no debate
	public List<GameObject> baralho = new List<GameObject>();

	// Lista da representação das cartas no código
	public List<Carta> cartas;

	private Jogador()

	{
		cartas = new List<Carta> {
			new CausaConsequencia(),
			new CausaConsequencia(),
			new CausaConsequencia(),
			new FakeNews(),
			new Analogia(),
			new AdHominem(),
			new ApeloAutoridade(),
			new OpiniaoEspecialista(),
			new OpiniaoEspecialista(),
			new OpiniaoEspecialista(),
			new AdHominem(),
			new ApeloAutoridade(),
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
		cartaObjeto.GetComponent<Arrastavel>().carta = cartaInfo;
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