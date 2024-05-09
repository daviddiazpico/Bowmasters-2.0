using Godot;
using System;

public partial class Flecha : RigidBody2D
{
	AnimatedSprite2D anim_Flecha;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.Position = new Vector2(10, -10);
		anim_Flecha = GetNode<AnimatedSprite2D>("SpriteFlecha");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Disparar(Vector2 velocidadFlecha)
	{
		//anim_Flecha.Play();
		this.LinearVelocity = velocidadFlecha;
	}

	private void _OnBodyEntered(Node body)
	{
		if (body is TileMap)
		{
			this.QueueFree();
		}
	}
}
