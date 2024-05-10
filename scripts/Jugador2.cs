using Godot;
using System;

public partial class Jugador2 : CharacterBody2D
{
	[Signal]
	public delegate void TurnoCompletadoEventHandler();

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	// Animaciones personaje
	AnimatedSprite2D anim_CargarDisparo;
	AnimatedSprite2D anim_AguantarDisparo;
	AnimatedSprite2D anim_Disparar;

	// Escena flecha
	PackedScene flecha;

	Vector2 velocidadFlecha;
	bool disparoCargado;

    public override void _Ready()
	{
		Position = new Vector2(1060, 200);

		anim_CargarDisparo = GetNode<AnimatedSprite2D>("Sprite1Jugador2");
		anim_AguantarDisparo = GetNode<AnimatedSprite2D>("Sprite2Jugador2");
		anim_Disparar = GetNode<AnimatedSprite2D>("Sprite3Jugador2");

		flecha = (PackedScene)ResourceLoader.Load("res://escenas/Flecha.tscn");
	}

	public AnimatedSprite2D Anim_CargarDisparo { get => anim_CargarDisparo; set => anim_CargarDisparo = value; }
    public AnimatedSprite2D Anim_AguantarDisparo { get => anim_AguantarDisparo; set => anim_AguantarDisparo = value; }
    public AnimatedSprite2D Anim_Disparar { get => anim_Disparar; set => anim_Disparar = value; }
    public Vector2 VelocidadFlecha { get => velocidadFlecha; set => velocidadFlecha = value; }
    public bool DisparoCargado { get => disparoCargado; set => disparoCargado = value; }

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

	public void DesactivarTodasLasAnimaciones()
	{
		anim_CargarDisparo.Stop();

		anim_AguantarDisparo.Stop();
		anim_AguantarDisparo.Visible = false;

		anim_Disparar.Stop();
		anim_Disparar.Visible = false;
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

	public void ActivarAnimacionDisparar()
	{
		anim_AguantarDisparo.Stop();
		anim_AguantarDisparo.Visible = false;
		
		anim_Disparar.Visible = true;
		anim_Disparar.Play();
	}

	public void Atacar(Vector2 velocidadFlecha)
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

	public Flecha CrearFlecha()
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

		Vector2 parabola = posicionMouse;
		parabola = new Vector2(-parabola.X, -parabola.Y);

		return parabola;
	}
}
