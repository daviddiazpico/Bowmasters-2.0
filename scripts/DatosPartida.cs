using Godot;
using System;

public partial class DatosPartida : Node
{
    private bool haGanadoJugador1;
    private bool haGanadoJugador2;

    public static DatosPartida Instancia { get; set; }
    public bool HaGanadoJugador1 { get => haGanadoJugador1; set => haGanadoJugador1 = value; }
    public bool HaGanadoJugador2 { get => haGanadoJugador2; set => haGanadoJugador2 = value; }

}
