﻿using System;
﻿using Akka.Actor;

namespace WinTail
{
    #region Program
    class Program
    {
        public static ActorSystem MyActorSystem;

        static void Main(string[] args)
        {
			// initialize MyActorSystem
			MyActorSystem = ActorSystem.Create("MyActorSystem");

			// time to make your first actors!
			var consoleWriterProps = Props.Create<ConsoleWriterActor>();
			IActorRef consoleWriterActor = MyActorSystem.ActorOf(consoleWriterProps, nameof(consoleWriterActor));
			var validationActorProps = Props.Create(() => new ValidationActor(consoleWriterActor));
			IActorRef validationActor = MyActorSystem.ActorOf(validationActorProps, nameof(validationActor));
			var consoleReaderProps = Props.Create<ConsoleReaderActor>(validationActor);
			IActorRef consoleReaderActor = MyActorSystem.ActorOf(consoleReaderProps, nameof(consoleReaderActor));

			// tell console reader to begin
			consoleReaderActor.Tell(ConsoleReaderActor.StartCommand);

			// blocks the main thread from exiting until the actor system is shut down
			MyActorSystem.WhenTerminated.Wait();
        }
    }
    #endregion
}
