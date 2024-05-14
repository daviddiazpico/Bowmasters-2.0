using Godot;
using System;

public partial class VerRanking : Control
{
	Ranking ranking;
	int numRegistrosMostrados;

	Label lblRanking;
	Button botonSiguiente;
	Button botonAtras;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ranking = new Ranking();
		numRegistrosMostrados = 0;

		lblRanking = GetNode<Label>("Lbl_MostrarRanking");
		botonSiguiente = GetNode<Button>("BotonSiguiente");
		botonAtras = GetNode<Button>("BotonAtras");
		GD.Print(ranking.ListaRanking.Count);
		if (ranking.ListaRanking.Count < 10)
		{
			foreach (RegistroRanking r in ranking.ListaRanking)
			{
				lblRanking.Text += r + "\n";
			}
		}
		else
		{
			botonSiguiente.Visible = true;
			for (int i = 0; i<10; i++)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
			}
			numRegistrosMostrados += 10;
		}
		GD.Print(numRegistrosMostrados);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void _OnBotonAtrasPressed()
	{
		if (numRegistrosMostrados > 0)
		{
			lblRanking.Text = "";
			numRegistrosMostrados-=20;
			for (int i = numRegistrosMostrados; i<numRegistrosMostrados+10; i++)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
			}
			numRegistrosMostrados+=10;
		}

		if (numRegistrosMostrados <= 10)
		{
			botonAtras.Visible = false;
		}
	}

	public void _OnBotonSiguientePressed()
	{
		botonAtras.Visible = true;
		if (numRegistrosMostrados < ranking.ListaRanking.Count)
		{
			lblRanking.Text = "";
			for (int i = numRegistrosMostrados; i<numRegistrosMostrados+10; i++)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
			}
			numRegistrosMostrados+=10;
		}
	}
}
