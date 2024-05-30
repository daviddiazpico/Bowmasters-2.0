using Godot;
using System;

public partial class FondoParallax : ParallaxBackground
{
	Vector2 direccion;
	int velocidad;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		direccion = new Vector2(1, 1);
		velocidad = 100;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		ScrollBaseOffset += direccion * (float)(velocidad * delta);
	}
}
