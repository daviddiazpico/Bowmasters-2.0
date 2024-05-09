using Godot;
using System;
using System.Threading;

public partial class Jugador1 : CharacterBody2D
{
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Animaciones personaje
	AnimatedSprite2D anim_CargarDisparo;
	AnimatedSprite2D anim_AguantarDisparo;
	AnimatedSprite2D anim_Disparar;

	// Escena flecha
	PackedScene flecha;

	bool disparoCargado;
	float anguloDisparo;
	Vector2 velocidadFlecha;

	public override void _Ready()
	{
		Position = new Vector2(157, 200);

		anim_CargarDisparo = GetNode<AnimatedSprite2D>("Sprite1Jugador1");
		anim_AguantarDisparo = GetNode<AnimatedSprite2D>("Sprite2Jugador1");
		anim_Disparar = GetNode<AnimatedSprite2D>("Sprite3Jugador1");

		flecha = (PackedScene)ResourceLoader.Load("res://escenas/Flecha.tscn");

		anguloDisparo = 0;
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

		if (Input.IsActionPressed("espacio"))
		{
			anim_CargarDisparo.Play();

			if (Input.IsActionJustPressed("click_izquierdo"))
			{
				velocidadFlecha = AjustarDisparo();
			}
		}
		
		if (Input.IsActionJustReleased("espacio"))
		{
			if (disparoCargado)
			{
				GD.Print(velocidadFlecha);
				Atacar(velocidadFlecha);
			}
			else
			{
				DesactivarTodasLasAnimaciones();
			}
		}
	}

	private void DesactivarTodasLasAnimaciones()
	{
		anim_CargarDisparo.Stop();

		anim_AguantarDisparo.Stop();
		anim_AguantarDisparo.Visible = false;

		anim_Disparar.Stop();
		anim_Disparar.Visible = false;
	}

	private void OnAnimationSprite1Looped()
	{
		anim_CargarDisparo.Stop();
		anim_CargarDisparo.Visible = false;

		anim_AguantarDisparo.Visible = true;
		anim_AguantarDisparo.Play();
		disparoCargado = true;
	}

	private void OnAnimationSprite3Looped()
	{
		anim_Disparar.Stop();
		anim_Disparar.Visible = false;

		anim_CargarDisparo.Stop();
		anim_CargarDisparo.Visible = true;
	}

	private void ActivarAnimacionDisparar()
	{
		anim_AguantarDisparo.Stop();
		anim_AguantarDisparo.Visible = false;
		
		anim_Disparar.Visible = true;
		anim_Disparar.Play();
	}

	private void Atacar(Vector2 velocidadFlecha)
	{
		ActivarAnimacionDisparar();
		Flecha nuevaFlecha = CrearFlecha();

		if (nuevaFlecha != null)
		{
			nuevaFlecha.Disparar(velocidadFlecha);
			disparoCargado = false;
		}
		else
		{
			GD.Print("Flecha instanciada == null");
		}
	}

	private Flecha CrearFlecha()
	{
		if (flecha != null)
		{
			Flecha nuevaFlecha = flecha.Instantiate<Flecha>();
			this.AddChild(nuevaFlecha);

			return nuevaFlecha;
		}

		return null;
	}

	public Vector2 AjustarDisparo()
	{
		Vector2 posicionMouse = GetGlobalMousePosition();

		Vector2 parabola = posicionMouse - new Vector2(10, -10);
		parabola = new Vector2(parabola.X, -parabola.Y);

		return parabola;
	}
}
