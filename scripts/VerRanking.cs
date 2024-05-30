using Godot;
using System;
using System.IO;

public partial class VerRanking : Control
{
	Ranking ranking;
	int numMostradosTotal;
	int numMostradosRonda;

	Label lblRanking;
	Button botonSiguiente;
	Button botonAtras;
	OptionButton botonOpciones;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		ranking = new Ranking();

		string[] lineasRanking = File.ReadAllLines("ficheros/ranking.txt");
		if (lineasRanking.Length > 0)
		{
			for (int i = 0; i<lineasRanking.Length; i++)
			{
				string[] trozosRegistro = lineasRanking[i].Split(';');
				ranking.AddRegistro(new RegistroRanking(trozosRegistro[0], Convert.ToInt32(trozosRegistro[1]), 
					Convert.ToInt32(trozosRegistro[2]), Convert.ToInt32(trozosRegistro[3]), Convert.ToDateTime(trozosRegistro[4])));
			}
		}

		ranking.ListaRanking.Sort();

		lblRanking = GetNode<Label>("Lbl_MostrarRanking");
		botonSiguiente = GetNode<Button>("BotonSiguiente");
		botonAtras = GetNode<Button>("BotonAtras");
		botonOpciones = GetNode<OptionButton>("BotonOpciones");
		
		botonSiguiente.Visible = false;
		botonAtras.Visible = false;
		
		MostrarPrimerosRegistros();
	}

	public Ranking Ranking { get => ranking; set => ranking = value; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void _OnBotonAtrasPressed()
	{
		if (numMostradosTotal > 10)
		{
			lblRanking.Text = "";

			numMostradosTotal -= numMostradosRonda+10;
			int i = numMostradosTotal;
			numMostradosRonda = 0;

			while (i < numMostradosTotal+10 && i < ranking.ListaRanking.Count)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
				i++;
				numMostradosRonda++;
			}
			numMostradosTotal+=numMostradosRonda;
		}
	}

	private void _OnBotonSiguientePressed()
	{
		if (numMostradosTotal < ranking.ListaRanking.Count)
		{
			lblRanking.Text = "";

			numMostradosRonda = 0;
			int i = numMostradosTotal;
			
			while (i < numMostradosTotal+10 && i < ranking.ListaRanking.Count)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
				i++;
				numMostradosRonda++;
			}
			numMostradosTotal+=numMostradosRonda;
		}
	}

	private void _OnBotonVolverPressed()
	{
		GetTree().ChangeSceneToFile("res://escenas/Pantalla_bienvenida.tscn");
	}

	private void _OnBotonOrdenarPressed()
	{
		int idxOpcion = botonOpciones.GetSelectedId();

		if (idxOpcion == 0)
		{
			ranking.ListaRanking.Sort();
		}
		else if (idxOpcion == 1)
		{
			ranking.ListaRanking.Sort((r1, r2) => r2.PartPerdidas.CompareTo(r1.PartPerdidas));
		}
		else if (idxOpcion == 2)
		{
			ranking.ListaRanking.Sort((r1, r2) => r2.PartJugadas.CompareTo(r1.PartJugadas));
		}

		MostrarPrimerosRegistros();
	}

	private void MostrarPrimerosRegistros()
	{
		numMostradosTotal = 0;

		lblRanking.Text = "";
		if (ranking.ListaRanking.Count < 10)
		{
			foreach (RegistroRanking r in ranking.ListaRanking)
			{
				lblRanking.Text += r + "\n";
			}
		}
		else
		{
			ActivarBotones();
			for (int i = 0; i<10; i++)
			{
				lblRanking.Text += ranking.ListaRanking[i] + "\n";
			}
			numMostradosTotal += 10;
		}
	}

	private void ActivarBotones()
	{
		botonSiguiente.Visible = true;
		botonAtras.Visible = true;
	}
}
