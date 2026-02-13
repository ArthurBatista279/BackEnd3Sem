using System;

interface IAutenticavel
{
    bool Autenticar(string senha);
}

class Usuario : IAutenticavel
{
    public bool Autenticar(string senha)
    {
        return senha == "123";
    }
}

class Administrador : IAutenticavel
{
    public bool Autenticar(string senha)
    {
        return senha == "admin";
    }
}