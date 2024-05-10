using Godot;
using System;

public partial class Partida : Node2D
{
	Timer temporizadorPartida;
	Jugador1 jugador1;
	Jugador2 jugador2;
	CharacterBody2D jugadorActivo;
	Camera2D camaraPartida;
	Camera2D camaraJugador1;
	Camera2D camaraJugador2;

	// True cuando el turno es del jugador1 y False cuando es del jugador2
	bool turnoJugador;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		temporizadorPartida = GetNode<Timer>("TemporizadorPartida");

		jugador1 = GetNode<Jugador1>("Jugador1");
		jugador2 = GetNode<Jugador2>("Jugador2");
		jugadorActivo = jugador1;

		camaraPartida = GetNode<Camera2D>("CamaraPartida");
		camaraJugador1 = GetNode<Camera2D>("Jugador1/CamaraJugador1");
		camaraJugador2 = GetNode<Camera2D>("Jugador2/CamaraJugador2");

		turnoJugador = true;

		if (temporizadorPartida.IsStopped())
		{
			SeguirJugadores();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (jugadorActivo is Jugador1)
		{
			GD.Print("j1");
			TurnoJugador1();
		}
		else if (jugadorActivo is Jugador2)
		{
			GD.Print("j2");
			TurnoJugador2();
		}
		
	}

	private void SeguirJugadores()
	{
		camaraPartida.Enabled = false;
		camaraJugador1.Enabled = false;
		camaraJugador2.Enabled = false;

		if (turnoJugador)
		{
			turnoJugador = false;
			camaraJugador1.Enabled = true;
		}
		else
		{
			turnoJugador = true;
			camaraJugador2.Enabled = true;
		}
	}

	public void TurnoJugador1()
	{
		if (Input.IsActionJustPressed("espacio"))
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
			}
			else
			{
				jugador1.DesactivarTodasLasAnimaciones();
			}
		}
	}

	public void TurnoJugador2()
	{
		if (Input.IsActionJustPressed("espacio"))
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
			}
			else
			{
				jugador2.DesactivarTodasLasAnimaciones();
			}
		}
	}
}
