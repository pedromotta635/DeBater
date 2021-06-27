using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cartas.Carta;

public static class Utils
{
	public static readonly Color vermelho = Cor(200, 50, 50);
	public static readonly Color verde = Cor(0, 200, 0);
	public static readonly Color azul = Cor(0, 200, 200);
	public static readonly Color amarelo = Cor(200, 200, 0);
	public static readonly Color branco = Cor(0, 0, 0);
	public static System.Random rng = new System.Random();

	/*public static Color Cor(int r, int g, int b)
	{
		return new Color((float) r / 255, (float) g / 255, (float) b / 255, 1.0f);
	}*/

	public static Color Cor(int r, int g, int b, float transparencia = 1.0f)
	{
		return new Color((float) r / 255, (float) g / 255, (float) b / 255, transparencia);
	}

	// Método Fisher-Yates para embaralhar listas
	public static void Embaralhar<T>(IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}

	#region Extensoes

	public static string ComoString(this Tipo t)
	{
		switch(t)
		{
			case Tipo.Argumento:
				return "Argumento";
			case Tipo.ContraArgumento:
				return "Contra-Argumento";
			case Tipo.Falacia:
				return "Falacia";
			default:
				return "";
		}
	}
	#endregion
}