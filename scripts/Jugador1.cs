using Godot;
using System;

public partial class Jugador1 : Jugador
{
    public override void _Ready()
	{
		Position = new Vector2(157, 200);

		anim_CargarDisparo = GetNode<AnimatedSprite2D>("Sprite1Jugador1");
		anim_AguantarDisparo = GetNode<AnimatedSprite2D>("Sprite2Jugador1");
		anim_Disparar = GetNode<AnimatedSprite2D>("Sprite3Jugador1");

		flecha = (PackedScene)ResourceLoader.Load("res://escenas/Flecha.tscn");

		barraVida = GetNode<ProgressBar>("BarraVidaJugador1");
		
		vida = 200;
		haGanado = true;
	}

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity.Y += gravity * (float)delta;
		}
		Velocity = velocity;
		MoveAndSlide();
	}

	public void _OnAnimationSprite1Looped()
	{
		anim_CargarDisparo.Stop();
		anim_CargarDisparo.Visible = false;

		anim_AguantarDisparo.Visible = true;
		anim_AguantarDisparo.Play();
		disparoCargado = true;
	}

	public void _OnAnimationSprite3Looped()
	{
		anim_Disparar.Stop();
		anim_Disparar.Visible = false;

		anim_CargarDisparo.Stop();
		anim_CargarDisparo.Visible = true;
	}
}
