using System;

using System;

class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }
}

class Funcionario : Pessoa
{
    public double Salario { get; set; }

    public Funcionario(string nome, int idade, double salario) : base(nome, idade)
    {
        Salario = salario;
    }
}

