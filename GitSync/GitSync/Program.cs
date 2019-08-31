using System;
using LibGit2Sharp;
using CommandLine;
using GitSync.arguments;

namespace GitSync
{
    class Program
    {

        static void Main(string[] args) => Parser.Default.ParseArguments<Options>(args).WithParsed(opts => ExecuteWithOptions(opts));

        static void ExecuteWithOptions(Options options)
        {
            try
            {
                new GitConfiguration();
            }
            catch (Exception e)
            {
                if (e.Message == Exceptions.ConfigurationFileDoesNotExist)
                {
                    // git user file does not exist
                    Console.WriteLine(Exceptions.ConfigureGitUser);
                    Console.Write("* Git username: ");
                    string username = Console.ReadLine();
                    Console.Write("* Git email: ");
                    string email = Console.ReadLine();
                    Console.Write("* Git password: ");
                    string pass = "";
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            pass += key.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                            {
                                pass = pass.Substring(0, (pass.Length - 1));
                                Console.Write("\b \b");
                            }
                            else if (key.Key == ConsoleKey.Enter)
                            {
                                break;
                            }
                        }
                    } while (true);
                    Arguments.AplyUserChanges(
                        new Options()
                        {
                            Username = username,
                            Email = email,
                            Password = pass
                        }
                    );
                }
                else
                {
                    // git user file exists but it was not possible to read
                    Console.WriteLine(Exceptions.ConfigurationFileManipulationError);
                }
                return;
            }
            int optionsCase = Arguments.GetCaseNumber(options);
            if (optionsCase == Arguments.INVALID_CASE)
            {
                Console.WriteLine(Exceptions.InvalidArgsCombination);
                return;
            }
            else if (optionsCase == Arguments.ERROR_CONFIGURATION_FILE)
            {
                Console.WriteLine(Exceptions.ErrorManipulatingConfigFile);
                return;
            }
            try
            {
                if (new GitController(options.Path).ExecuteCase(optionsCase, options))
                {
                    Console.WriteLine("\nSynced!");
                }
                else
                {
                    Console.WriteLine(Exceptions.ImcompletedSync);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
