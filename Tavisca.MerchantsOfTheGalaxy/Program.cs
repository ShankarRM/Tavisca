using System;

namespace Tavisca.MerchantsOfTheGalaxy
{
    class Program
    {
        static void Main(string[] args)
        {
            var interpreter = new Interpreter();
            CommandResult commandResult = new CommandResult();
            var sentence = new string[]{
                "glob is I",
                "prok is V",
                "pish is X",
                "tegj is L",
                "glob glob Silver is 34 Credits",
                "glob prok Gold is 57800 Credits",
                "pish pish Iron is 3910 Credits",
                "how much is pish tegj glob glob ?",
                "how many Credits is glob prok Silver ?",
                "how many Credits is glob prok Gold ?",
                "how many Credits is glob prok Iron ?"
            };
            foreach (var command in sentence)
            {
                commandResult = interpreter.ParseAndExecute(command);
                Console.WriteLine(commandResult.ResultText);
            }
        }
    }
}
