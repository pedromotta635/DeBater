using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static readonly Color vermelho = Cor(200, 50, 50, 1.0f);
	public static readonly Color verde = Cor(0, 200, 0, 1.0f);
	public static readonly Color azul = Cor(0, 200, 200, 1.0f);
	public static readonly Color amarelo = Cor(200, 200, 0, 1.0f);

	public static Color Cor(int r, int g, int b, float transparencia)
	{
		return new Color((float) r / 255, (float) g / 255, (float) b / 255, transparencia);
	}
}