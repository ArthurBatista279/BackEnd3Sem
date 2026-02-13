using System;

class Pessoa
{
    public string Nome { get; set; }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public void Apresentar()
    {
        Console.WriteLine($"Olá! Meu nome é {Nome}.");
    }

    public void Apresentar(string sobrenome)
    {
        Console.WriteLine($"Olá! Meu nome é {Nome} {sobrenome}.");
    }
}


