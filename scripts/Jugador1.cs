using Godot;
using System;

public partial class Jugador1 : CharacterBody2D
{
	AnimatedSprite2D animacionDisparar;
	PackedScene flecha;

    public override void _Ready()
    {
        Position = new Vector2(157, 200);
		animacionDisparar = GetNode<AnimatedSprite2D>("SpriteJugador1");
		flecha = (PackedScene)ResourceLoader.Load("res://escenas/Flecha.tscn");
    }
    // Get the gravity from the project settings to be synced with RigidBody nodes.
    public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

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

		if (Input.IsActionJustPressed("espacio"))
		{
			Atacar();
		}
		else
		{
			DejarAtacar();
		}
	}

	private void Atacar()
	{
		if (flecha != null)
		{
			Flecha nuevaFlecha = flecha.Instantiate<Flecha>();
			this.AddChild(nuevaFlecha);
			nuevaFlecha.Position = new Vector2(10, 10);
			//nuevaFlecha.Mover();
			animacionDisparar.Play();
		}
		
		
	}

	private void DejarAtacar()
	{
		flecha.IsQueuedForDeletion();
		animacionDisparar.Stop();
	}
}
