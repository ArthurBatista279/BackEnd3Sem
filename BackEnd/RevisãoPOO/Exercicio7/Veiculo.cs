using System;

class Veiculo
{
    public virtual void Mover()
    {
        Console.WriteLine("O veículo está se movendo...");
    }
}

class Carro : Veiculo
{
    public override void Mover()
    {
        Console.WriteLine("O carro se move usando motor.");
    }
}

class Bicicleta : Veiculo
{
    public override void Mover()
    {
        Console.WriteLine("A bicicleta se move pedalando.");
    }
}