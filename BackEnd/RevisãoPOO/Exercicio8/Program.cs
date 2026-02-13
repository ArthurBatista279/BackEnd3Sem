using System;

{
    Usuario u = new Usuario();
    Administrador a = new Administrador();

    Console.WriteLine("Usuário autenticou? " + u.Autenticar("123"));
    Console.WriteLine("Administrador autenticou? " + a.Autenticar("admin"));
}