using Godot;
using System;
using System.Threading;

public partial class Jugador1 : CharacterBody2D
{
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	AnimatedSprite2D anim_CargarDisparo;
	AnimatedSprite2D anim_AguantarDisparo;
	AnimatedSprite2D anim_Disparar;
	PackedScene flecha;
	bool disparoCargado;

	public override void _Ready()
	{
		Position = new Vector2(157, 200);
		anim_CargarDisparo = GetNode<AnimatedSprite2D>("Sprite1Jugador1");
		anim_AguantarDisparo = GetNode<AnimatedSprite2D>("Sprite2Jugador1");
		anim_Disparar = GetNode<AnimatedSprite2D>("Sprite3Jugador1");
		flecha = (PackedScene)ResourceLoader.Load("res://escenas/Flecha.tscn");
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
		}
		else if (Input.IsActionJustReleased("espacio"))
		{
			if (disparoCargado)
			{
				Atacar();
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

	private void Atacar()
	{
		ActivarAnimacionDisparar();
		Flecha nuevaFlecha = CrearFlecha();

		if (nuevaFlecha != null)
		{
			nuevaFlecha.Disparar();
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
}
