using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface IEfeito
{
	string texto { get; }

	Carta.Tipo tipo { get; }

	void AtualizarEfeito(Plateia plateia);

	void Aplicar(Plateia plateia);
}

public class EfeitoArgumentar : IEfeito
{
	private Ia ia;
	public string texto { get; }

	public Carta.Tipo tipo { get; } = Carta.Tipo.Argumento;

	private readonly int efeitoInicial;
	private int _efeito;
	private int efeito
	{
		get => _efeito;
		set { _efeito = value > 0 ? value : 0;}
	}

	public EfeitoArgumentar( Ia ia, int efeito)
	{
		this.ia = ia;
		this.efeitoInicial = this.efeito = efeito;
		this.texto = efeito.ToString();
	}

	public void AtualizarEfeito(Plateia plateia)
	{
		efeito = efeitoInicial + ia.autoconfianca;
	}

	public void Aplicar(Plateia plateia)
	{
		plateia.AlterarApoioPor(-efeito);
	}
}

public class EfeitoContraArgumentar : IEfeito
{
	public string texto { get; }

	public Carta.Tipo tipo { get; } = Carta.Tipo.ContraArgumento;

	private readonly int efeitoInicial;
	private int efeito;
	private Ia ia;

	public EfeitoContraArgumentar(Ia ia, int efeito)
	{
		this.ia = ia;
		this.efeitoInicial = this.efeito = efeito;
		this.texto = efeito.ToString();
	}

	public void AtualizarEfeito(Plateia plateia)
	{
		efeito = efeitoInicial + ia.autoconfianca;
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

	public virtual int autoconfianca { get; set; } = 0;

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
		private set { _efeitoAtual = value; }
	}

	public virtual IEfeito NovoEfeito()
	{
		int i = Utils.rng.Next(efeitos.Length);
		texto = efeitos[i].texto;
		efeitoAtual = efeitos[i];
		return efeitos[i];
	}

	public virtual void AtualizarEfeito(Plateia plateia) => efeitoAtual?.AtualizarEfeito(plateia);

	public abstract void Acao(Plateia plateia);


}

public class IaBasica : Ia
{
	protected override IEfeito[] efeitos { get; set; }

	public IaBasica()
	{
		efeitos = new IEfeito[] {
			new EfeitoArgumentar(this, 30),
			new EfeitoArgumentar(this, 40),
			new EfeitoArgumentar(this, 20)
		};
	}

	public override void Acao(Plateia plateia)
	{
		efeitoAtual.Aplicar(plateia);
	}
}