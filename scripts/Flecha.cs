using Godot;
using System;

public partial class Flecha : RigidBody2D
{
	// Animacion flecha
	AnimatedSprite2D anim_Flecha;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (GetParent() is Jugador1)
		{
			Position = new Vector2(30, -10);
		}
		else if (GetParent() is Jugador2)
		{
			Position = new Vector2(-30, -10);
		}
		
		anim_Flecha = GetNode<AnimatedSprite2D>("SpriteFlecha");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Disparar(Vector2 velocidadFlecha)
	{
		//anim_Flecha.Play();
		LinearVelocity = velocidadFlecha;
	}

	private void _OnBodyEntered(Node body)
	{
		Jugador1 jugador1 = GetParent().GetNode<Jugador1>("/root/Partida/Jugador1");
		Jugador2 jugador2 = GetParent().GetNode<Jugador2>("/root/Partida/Jugador2");

		if (body is TileMap)
		{
			QueueFree();
			jugador1.BarraVida.Visible = true;
			jugador2.BarraVida.Visible = true;
		}
		else if (body is CharacterBody2D)
		{
			if (body is Jugador1)
			{
				jugador1.QuitarVida();
				QueueFree();
				jugador1.BarraVida.Visible = true;
				jugador2.BarraVida.Visible = true;
			}
			else if (body is Jugador2)
			{
				jugador2.QuitarVida();
				QueueFree();
				jugador1.BarraVida.Visible = true;
				jugador2.BarraVida.Visible = true;
			}
		}
	}
}
