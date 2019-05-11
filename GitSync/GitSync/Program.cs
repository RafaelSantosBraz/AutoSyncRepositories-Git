using System;
using System.IO;
using GitHub;

namespace GitSync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Esperava encontrar um único parâmetro!");
                return;
            }           
            Diretorio diretorio = new Diretorio(args[0]);
            if (!diretorio.Verificar())
            {
                Console.WriteLine("Caminho informado não encontrado!");
                return;
            }
            Configuracao config = new Configuracao("config.txt");
            if (!config.Verificar())
            {
                Console.WriteLine("Erro ao manipular Usuário e Senha no arquivo de Configuração!");
            }
            GitControle controle = new GitControle(config, diretorio);
            controle.CriarCommit("Teste");
        }
    }
}
