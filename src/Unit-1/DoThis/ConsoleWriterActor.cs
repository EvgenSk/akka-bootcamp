using System;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for serializing message writes to the console.
    /// (write one message at a time, champ :)
    /// </summary>
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
			if (message is Messages.InputError errorMsg)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(errorMsg.Reason);
			}
			else if (message is Messages.InputSuccess succsessMsg)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(succsessMsg.Reason);
			}
			else
			{
				Console.WriteLine(message);
			}

			Console.ResetColor();
		}
    }
}
