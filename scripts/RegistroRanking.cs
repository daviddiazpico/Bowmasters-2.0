using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public partial class RegistroRanking : Node
{
    string nombreJugador;
    int partJugadas;
    int partGanadas;
    int partPerdidas;

    public RegistroRanking(string nombreJugador)
    {
        this.nombreJugador = nombreJugador;
        partJugadas = 0;
        partGanadas = 0;
        partPerdidas = 0;
    }

    public RegistroRanking() { }

    public string NombreJugador { get => nombreJugador; set => nombreJugador = value; }
    public int PartJugadas { get => partJugadas; set => partJugadas = value; }
    public int PartGanadas { get => partGanadas; set => partGanadas = value; }
    public int PartPerdidas { get => partPerdidas; set => partPerdidas = value; }

    public override string ToString()
    {
        return $"{nombreJugador} - {partJugadas} - {partGanadas} - {partPerdidas}";
    }

    public static void Guardar(List<RegistroRanking> ranking)
    {
        JsonSerializerOptions opciones = new JsonSerializerOptions{ WriteIndented = true };
        string jsonString = JsonSerializer.Serialize(ranking, opciones);
        File.WriteAllText("ranking.json", jsonString);
    }

    public static List<RegistroRanking> Cargar()
    {
        List<RegistroRanking> rankingCargado;

        string datosFichero = File.ReadAllText("ranking.json");
        rankingCargado = JsonSerializer.Deserialize<List<RegistroRanking>>(datosFichero);

        return rankingCargado;
    }

}
