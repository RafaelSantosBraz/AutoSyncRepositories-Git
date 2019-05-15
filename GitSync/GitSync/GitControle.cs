using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync
{
    class GitControle
    {

        private Configuracao Configuracao { get; }
        private Diretorio Diretorio { get; }

        public GitControle(Configuracao configuracao, Diretorio diretorio)
        {
            Configuracao = configuracao;
            Diretorio = diretorio;
        }

        public Boolean CriarCommit(String mensagem)
        {
            return ExecutarComandoCMD("dir");            
        }

        private Boolean ExecutarComandoCMD(String comando)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + comando)
                {
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                System.Diagnostics.Process proc = new System.Diagnostics.Process()
                {
                    StartInfo = info
                };
                proc.Start();
                string result = proc.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
