using System;

namespace Exercicio3;

    public class Pessoa
    {
        public string Nome;

        private int idade;

        public int Idade
        {
            get { return idade; }
            set
            {
                if (value > 0)
                    idade = value;
                else
                    Console.WriteLine("Idade inválida!");
            }
        }
    }