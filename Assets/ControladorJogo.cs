using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using IA;

public class ControladorJogo : MonoBehaviour
{
	public static ControladorJogo instancia;

	private Ia[] iasDisponiveis = {
		new IaBasica(),
		new IaBasica()
	};

	[SerializeField]
	private string _seed;
	public string seed
	{
		get
		{
			if (_seed == null)
			{
				const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
				var stringChars = new char[10];
				for (int i = 0; i < stringChars.Length; i++)
				{
					stringChars[i] = chars[Utils.rng.Next(chars.Length)];
				}
				_seed = new string(stringChars);
			}
			return _seed;
		}
		set { _seed = value; }
	}

	void Awake()
	{
		instancia = this;
	}

	public void BotaoEnciclopedia()
	{
		SceneManager.LoadScene("Enciclopedia", LoadSceneMode.Single);
	}

	public void AplicarSeed()
	{
		Utils.rng = new System.Random(seed.GetHashCode());
	}

	
}