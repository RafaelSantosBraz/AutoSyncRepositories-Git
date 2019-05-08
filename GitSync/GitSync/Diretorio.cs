using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GitSync
{
    class Diretorio
    {
        private String Caminho { get; }

        public Diretorio(String caminho)
        {
            Caminho = caminho;
        }
        public Boolean Verificar()
        {
            return Directory.Exists(Caminho);
        }
    }
}
