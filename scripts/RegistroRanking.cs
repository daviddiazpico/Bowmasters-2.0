using Godot;
using System;
using System.Collections.Generic;
using System.IO;

public partial class RegistroRanking : Node, IEquatable<RegistroRanking>, IComparable<RegistroRanking>
{
    string nombreJugador;
    int partJugadas;
    int partGanadas;
    int partPerdidas;
    DateTime primeraVezJugo;

    public RegistroRanking(string nombreJugador)
    {
        this.nombreJugador = nombreJugador;
        partJugadas = 0;
        partGanadas = 0;
        partPerdidas = 0;
        primeraVezJugo = DateTime.Today;
    }

    public RegistroRanking(string nombre, int partJugadas, bool haGanado)
    {
        this.nombreJugador = nombre;
        this.partJugadas = partJugadas;

        if (haGanado)
        {
            partGanadas = 1;
            partPerdidas = 0;
        }
        else
        {
            partGanadas = 0;
            partPerdidas = 1;
        }
        
        primeraVezJugo = DateTime.Today;
    }

    public RegistroRanking(string nombreJugador, int partJugadas, 
        int partGanadas, int partPerdidas, DateTime primeraVezJugo)
    {
        this.nombreJugador = nombreJugador;
        this.partJugadas = partJugadas;
        this.partGanadas = partGanadas;
        this.partPerdidas = partPerdidas;
        this.primeraVezJugo = primeraVezJugo;
    }

    public string NombreJugador { get => nombreJugador; set => nombreJugador = value; }
    public int PartJugadas { get => partJugadas; set => partJugadas = value; }
    public int PartGanadas { get => partGanadas; set => partGanadas = value; }
    public int PartPerdidas { get => partPerdidas; set => partPerdidas = value; }
    public DateTime PrimeraVezJugado { get => primeraVezJugo; set => primeraVezJugo = value; }


    public override string ToString()
    {  
        return $"{nombreJugador.ToUpper()}:  " +
            $"Jugadas: {partJugadas}  -  " +
            $"Win: {partGanadas}  -  " +
            $"Lose: {partPerdidas}  -  " +
            $"1ª Partida: {primeraVezJugo.ToString("d")}";
    }

    public string ToString(string formato)
    {
        if (formato == "f")
        {
            return $"{nombreJugador};{partJugadas};{partGanadas};{partPerdidas};{primeraVezJugo.ToString("d")}";
        }
        else
        {
            return null;
        }
    }

    public bool Equals(RegistroRanking other)
    {
        return this.nombreJugador.Equals(other.nombreJugador);
    }

    public int CompareTo(RegistroRanking registro)
    {
        return registro.partGanadas.CompareTo(this.partGanadas);
    }
}
