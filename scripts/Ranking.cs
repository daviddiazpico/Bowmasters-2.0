using Godot;
using System;
using System.Collections.Generic;

public partial class Ranking : Node
{
    List<RegistroRanking> listaRanking;

    public Ranking()
    {
        //listaRanking = RegistroRanking.Cargar();
        listaRanking = new List<RegistroRanking>();
        listaRanking.Add(new RegistroRanking("David"));
        listaRanking.Add(new RegistroRanking("Ismael"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Yee"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Iker"));
        listaRanking.Add(new RegistroRanking("Dabi"));
    }

    public List<RegistroRanking> ListaRanking { get => listaRanking; }

    public void AddRegistro(RegistroRanking r)
    {
        listaRanking.Add(r);
    }

}
