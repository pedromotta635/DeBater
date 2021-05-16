using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IEfeito
{
	string texto { get; }

	void Aplicar(Plateia plateia);
}

public class EfeitoArgumentar : IEfeito
{
	public string texto { get; }

	private int efeito;

	public EfeitoArgumentar(int efeito)
	{
		this.efeito = efeito;
		this.texto = efeito.ToString();
	}

	public void Aplicar(Plateia plateia)
	{
		plateia.AlterarApoioPor(-efeito);
	}
}

public class EfeitoContraArgumentar : IEfeito
{
	public string texto { get; }

	private int efeito;
	private Ia ia;

	public EfeitoContraArgumentar(Ia ia, int efeito)
	{
		this.ia = ia;
		this.efeito = efeito;
		this.texto = efeito.ToString();
	}

	public void Aplicar(Plateia plateia)
	{
		ia.nivelContraArgumento += efeito;
	}
}

public abstract class Ia
{
	public virtual string texto { get; set; }

	public virtual int nivelContraArgumento { get; set; } = 0;

	protected abstract IEfeito[] efeitos { get; set; }

	private IEfeito _efeitoAtual;
	public virtual IEfeito efeitoAtual
	{
		get
		{
			if (_efeitoAtual == null)
			{
				_efeitoAtual = NovoEfeito();
			}
			return _efeitoAtual;
		}
		private set
		{
			_efeitoAtual = value;
		}
	}

	public virtual IEfeito NovoEfeito()
	{
		int i = Utils.rng.Next(efeitos.Length);
		texto = efeitos[i].texto;
		efeitoAtual = efeitos[i];
		return efeitos[i];
	}

	public abstract void Acao(Plateia plateia);


}

public class IaBasica : Ia
{
	protected override IEfeito[] efeitos { get; set; } = {
		new EfeitoArgumentar(30),
		new EfeitoArgumentar(40),
		new EfeitoArgumentar(20)
	};

	public override void Acao(Plateia plateia)
	{
		efeitoAtual.Aplicar(plateia);
	}
}