using Godot;
using System;

public partial class Partida : Node2D
{
	Timer temporizadorPartida;
	Camera2D camaraPartida;
	Camera2D camaraJugador1;
	Camera2D camaraJugador2;

	// True cuando el turno es del jugador1 y False cuando es del jugador2
	bool turnoJugador;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		temporizadorPartida = GetNode<Timer>("TemporizadorPartida");
		camaraPartida = GetNode<Camera2D>("CamaraPartida");
		camaraJugador1 = GetNode<Camera2D>("Jugador1/CamaraJugador1");

		turnoJugador = true;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (temporizadorPartida.IsStopped())
		{
			SeguirJugadores();
		}
	}

	private void SeguirJugadores()
	{
		camaraPartida.Enabled = false;
		camaraJugador1.Enabled = false;
		//camaraJugador2.Enabled = false;

		if (turnoJugador)
		{
			camaraJugador1.Enabled = true;
		}
		else
		{
			//camaraJugador2.Enabled = true;
		}
	}
}
