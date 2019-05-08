using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace GitSync
{
    class Configuracao
    {
        private String Caminho { get; }
        private String Usuario { get; set; }
        private String Senha { get; set; }

        public Configuracao(String caminho)
        {
            Caminho = caminho;
        }

        public Boolean Verificar()
        {
            return (File.Exists(Caminho) && ExtrairUsuarioSenha());
        }

        private Boolean ExtrairUsuarioSenha()
        {
            try
            {
                using (StreamReader leitor = new StreamReader(Caminho))
                {
                    while (!leitor.EndOfStream)
                    {
                        String linha = leitor.ReadLine();
                        String[] partes = Regex.Split(linha, ":");
                        if (partes.Length != 2)
                        {
                            return false;
                        }
                        switch (partes[0])
                        {
                            case "User":
                                Usuario = partes[1];
                                break;
                            case "Password":
                                Senha = partes[1];
                                break;
                            default:
                                return false;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
