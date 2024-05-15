using Godot;
using System;
using System.Collections.Generic;

public partial class Ranking : Node
{
    List<RegistroRanking> listaRanking;

    public Ranking()
    {
        listaRanking = new List<RegistroRanking>();
    }

    public List<RegistroRanking> ListaRanking { get => listaRanking; }

    public void AddRegistro(RegistroRanking r)
    {
        listaRanking.Add(r);
    }

    public void ModificarRegistro(RegistroRanking r, bool haGanado)
    {
        r.PartJugadas++;
        
        if (haGanado)
        {
            r.PartGanadas++;
        }
        else
        {
            r.PartPerdidas++;
        }
    }

}
