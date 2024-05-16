using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class GuardarDatosRanking : Control
{
	Label lblEnhorabuena;
	TextEdit inputField1;
	TextEdit inputField2;

	string nombreJugador1;
	string nombreJugador2;

	Ranking rankingTemporal;
	bool haGanadoJugador1;
	bool haGanadoJugador2;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		haGanadoJugador1 = DatosPartida.Instancia.HaGanadoJugador1;
		haGanadoJugador2 = DatosPartida.Instancia.HaGanadoJugador2;
		StreamReader ficheroRanking = null;
		rankingTemporal = new Ranking();

		try
		{
			ficheroRanking = new StreamReader("ficheros/ranking.txt");
			string lineaFichero = "";

			while (lineaFichero != null)
			{
				lineaFichero = ficheroRanking.ReadLine();

				if (lineaFichero != null)
				{
					string[] trozosLineaFichero = lineaFichero.Split(';');
					rankingTemporal.AddRegistro(new RegistroRanking(trozosLineaFichero[0], Convert.ToInt32(trozosLineaFichero[1]), 
						Convert.ToInt32(trozosLineaFichero[2]), Convert.ToInt32(trozosLineaFichero[3]), Convert.ToDateTime(trozosLineaFichero[4])));
				}
			}
		}
		catch (FileNotFoundException)
		{
			GD.Print("Fichero no encontrado");
		}
		catch (IOException)
		{
			GD.Print("Problemas con los ficheros");
		}
		finally
		{
			if (ficheroRanking != null)
			{
				ficheroRanking.Close();
			}
		}

		lblEnhorabuena = GetNode<Label>("LblEnhorabuena");

		if (haGanadoJugador1)
		{
			lblEnhorabuena.Text = "ENHORABUENA JUGADOR 1";
		}
		else
		{
			lblEnhorabuena.Text = "ENHORABUENA JUGADOR 2";
		}

		inputField1 = GetNode<TextEdit>("NombreJugador1");
		inputField2 = GetNode<TextEdit>("NombreJugador2");
	}

	public string NombreJugador1 { get => nombreJugador1; set => nombreJugador1 = value; }
    public string NombreJugador2 { get => nombreJugador2; set => nombreJugador2 = value; }
    public bool HaGanadoJugador1 { get => haGanadoJugador1; set => haGanadoJugador1 = value; }
    public bool HaGanadoJugador2 { get => haGanadoJugador2; set => haGanadoJugador2 = value; }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}

	private void _OnBotonIniciarPressed()
	{
		nombreJugador1 = inputField1.Text;
		nombreJugador2 = inputField2.Text;

		if (ExisteElUsuario(nombreJugador1) && ExisteElUsuario(nombreJugador2))
		{
			ModificarRegistro(nombreJugador1, haGanadoJugador1);
			ModificarRegistro(nombreJugador2, haGanadoJugador2);
		}
		else if (ExisteElUsuario(nombreJugador1) && !ExisteElUsuario(nombreJugador2))
		{
			ModificarRegistro(nombreJugador1, haGanadoJugador1);
			rankingTemporal.AddRegistro(new RegistroRanking(nombreJugador2, 1, haGanadoJugador2));
		}
		else if (!ExisteElUsuario(nombreJugador1) && ExisteElUsuario(nombreJugador2))
		{
			rankingTemporal.AddRegistro(new RegistroRanking(nombreJugador1, 1, haGanadoJugador1));
			ModificarRegistro(nombreJugador2, haGanadoJugador2);
		}
		else
		{
			rankingTemporal.AddRegistro(new RegistroRanking(nombreJugador1, 1, haGanadoJugador1));
			rankingTemporal.AddRegistro(new RegistroRanking(nombreJugador2, 1, haGanadoJugador2));
		}
		
		ActualizarRanking();
		GetTree().ChangeSceneToFile("res://escenas/Pantalla_bienvenida.tscn");
	}

	private bool ExisteElUsuario(string nombre)
	{
		return rankingTemporal.ListaRanking.Contains(new RegistroRanking(nombre));
	}

	private void ModificarRegistro(string nombre, bool gano)
	{
		int posicion = rankingTemporal.ListaRanking.FindIndex(r => r.NombreJugador == nombre);
		rankingTemporal.ListaRanking[posicion].PartJugadas++;
		
		if (gano)
		{
			rankingTemporal.ListaRanking[posicion].PartGanadas++;
		}
		else
		{
			rankingTemporal.ListaRanking[posicion].PartPerdidas++;
		}
	}

	private void ActualizarRanking()
	{
		StreamWriter ficheroRanking = null;

		try
		{
			ficheroRanking = new StreamWriter("ficheros/ranking.txt");

			foreach (RegistroRanking reg in rankingTemporal.ListaRanking)
			{
				ficheroRanking.WriteLine(reg.ToString("f"));
			}
		}
		catch (FileNotFoundException)
		{
			GD.Print("Fichero no encontrado.");
		}
		catch (IOException)
		{
			GD.Print("Problemas con los ficheros.");
		}
		finally
		{
			if (ficheroRanking != null)
			{
				ficheroRanking.Close();
			}
		}
	}
}
