using Exercicio3;

{
    Pessoa p = new Pessoa();
    p.Nome = "Arthur";
    p.Idade = 18;

    Console.WriteLine($"Idade: {p.Idade}");

    p.Idade = -5; // teste
}