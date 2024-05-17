using Godot;
using System;
using System.IO;

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
		string[] lineasRanking = File.ReadAllLines("ficheros/ranking.txt");
		GD.Print(lineasRanking.Length);
		if (lineasRanking.Length > 0)
		{
			for (int i = 0; i<lineasRanking.Length; i++)
			{
				string[] trozosRegistro = lineasRanking[i].Split(';');
				ranking.AddRegistro(new RegistroRanking(trozosRegistro[0], Convert.ToInt32(trozosRegistro[1]), 
					Convert.ToInt32(trozosRegistro[2]), Convert.ToInt32(trozosRegistro[3]), Convert.ToDateTime(trozosRegistro[4])));
			}
		}
		

		numRegistrosMostrados = 0;

		lblRanking = GetNode<Label>("Lbl_MostrarRanking");
		botonSiguiente = GetNode<Button>("BotonSiguiente");
		botonAtras = GetNode<Button>("BotonAtras");
		
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
	}

	public Ranking Ranking { get => ranking; set => ranking = value; }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	private void _OnBotonAtrasPressed()
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

	private void _OnBotonSiguientePressed()
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

	private void _OnBotonVolverPressed()
	{
		GetTree().ChangeSceneToFile("res://escenas/Pantalla_bienvenida.tscn");
	}
}
