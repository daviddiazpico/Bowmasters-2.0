using Godot;
using System;

public partial class Jugador : CharacterBody2D
{
    // Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    // Animaciones personaje
	protected AnimatedSprite2D anim_CargarDisparo;
	protected AnimatedSprite2D anim_AguantarDisparo;
	protected AnimatedSprite2D anim_Disparar;

    // Escena flecha
	protected PackedScene flecha;

	protected Vector2 velocidadFlecha;
	protected bool disparoCargado;

    public AnimatedSprite2D Anim_CargarDisparo { get => anim_CargarDisparo; set => anim_CargarDisparo = value; }
    public AnimatedSprite2D Anim_AguantarDisparo { get => anim_AguantarDisparo; set => anim_AguantarDisparo = value; }
    public AnimatedSprite2D Anim_Disparar { get => anim_Disparar; set => anim_Disparar = value; }
    public Vector2 VelocidadFlecha { get => velocidadFlecha; set => velocidadFlecha = value; }
    public bool DisparoCargado { get => disparoCargado; set => disparoCargado = value; }

    public void DesactivarTodasLasAnimaciones()
	{
		anim_CargarDisparo.Stop();

		anim_AguantarDisparo.Stop();
		anim_AguantarDisparo.Visible = false;

		anim_Disparar.Stop();
		anim_Disparar.Visible = false;
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
		
		Vector2 direccion = (posicionMouse - Position).Normalized();

		return direccion * 1000;
	}
}
