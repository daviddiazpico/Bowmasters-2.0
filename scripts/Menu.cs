using Godot;
using System;

public partial class Menu : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{	
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnBotonJugarPressed()
	{
		GetTree().ChangeSceneToFile("res://escenas/Partida.tscn");
	}

	private void OnBotonRankingPressed()
	{
		GetTree().ChangeSceneToFile("res://escenas/Ver_ranking.tscn");
	}
	
	private void OnBotonSalirPressed()
	{
		GetTree().Quit();
	}

	public void OnBotonAjustesPressed()
	{
		GD.Print("ajustes");
	}
}



