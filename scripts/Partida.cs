using Godot;
using System;

public partial class Partida : Node2D
{
	Timer temporizadorPartida;

	// Jugadores
	Jugador1 jugador1;
	Jugador2 jugador2;
	CharacterBody2D jugadorActivo;

	// Camaras
	Camera2D camaraPartida;
	Camera2D camaraJugador1;
	Camera2D camaraJugador2;

	// True cuando el turno es del jugador1 y False cuando es del jugador2
	bool turnoJugador;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		if (DatosPartida.Instancia == null)
        {
            DatosPartida.Instancia = new DatosPartida();
        }
        else
        {
            DatosPartida.Instancia.QueueFree();
        }

		temporizadorPartida = GetNode<Timer>("TemporizadorPartida");

		jugador1 = GetNode<Jugador1>("Jugador1");
		jugador2 = GetNode<Jugador2>("Jugador2");
		jugadorActivo = jugador1;

		camaraPartida = GetNode<Camera2D>("CamaraPartida");
		camaraJugador1 = GetNode<Camera2D>("Jugador1/CamaraJugador1");
		camaraJugador2 = GetNode<Camera2D>("Jugador2/CamaraJugador2");

		turnoJugador = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (temporizadorPartida.IsStopped())
		{
			ActivarBarrasVida();
			SeguirJugador();

			if (jugador1.HaMuerto() || jugador2.HaMuerto())
			{
				DatosPartida.Instancia.HaGanadoJugador1 = jugador1.HaGanado;
				DatosPartida.Instancia.HaGanadoJugador2 = jugador2.HaGanado;
				GetTree().ChangeSceneToFile("res://escenas/GuardarDatosRanking.tscn");
			}
			else
			{
				if (jugadorActivo is Jugador1)
				{
					TurnoJugador1();
				}
				else if (jugadorActivo is Jugador2)
				{
					TurnoJugador2();
				}
			}
		}
	}

	private void SeguirJugador()
	{
		camaraPartida.Enabled = false;
		camaraJugador1.Enabled = false;
		camaraJugador2.Enabled = false;

		if (turnoJugador)
		{
			camaraJugador1.Enabled = true;
		}
		else
		{
			camaraJugador2.Enabled = true;
		}
	}

	private void TurnoJugador1()
	{
		// Reiniciar la velocidad de la flecha del jugador 2
		jugador2.VelocidadFlecha = new Vector2(0, 0);

		SeguirJugador();

		if (Input.IsActionPressed("espacio"))
		{
			jugador1.Anim_CargarDisparo.Play();

			if (Input.IsActionJustPressed("click_izquierdo"))
			{
				jugador1.VelocidadFlecha = jugador1.AjustarDisparo();
			}
		}

		if (Input.IsActionJustReleased("espacio"))
		{
			if (jugador1.DisparoCargado)
			{
				jugador1.Atacar(jugador1.VelocidadFlecha);
				jugadorActivo = jugador2;
				turnoJugador = false;
			}
			else
			{
				jugador1.DesactivarTodasLasAnimaciones();
			}
		}
	}

	private void TurnoJugador2()
	{
		// Reiniciar la velocidad de la flecha del jugador 1
		jugador1.VelocidadFlecha = new Vector2(0, 0);

		SeguirJugador();

		if (Input.IsActionPressed("espacio"))
		{
			jugador2.Anim_CargarDisparo.Play();

			if (Input.IsActionJustPressed("click_izquierdo"))
			{
				jugador2.VelocidadFlecha = jugador2.AjustarDisparo();
			}
		}

		if (Input.IsActionJustReleased("espacio"))
		{
			if (jugador2.DisparoCargado)
			{
				jugador2.Atacar(jugador2.VelocidadFlecha);
				jugadorActivo = jugador1;
				turnoJugador = true;
			}
			else
			{
				jugador2.DesactivarTodasLasAnimaciones();
			}
		}
	}

	private void ActivarBarrasVida()
	{
		jugador1.BarraVida.Visible = true;
		jugador2.BarraVida.Visible = true;
	}
}
