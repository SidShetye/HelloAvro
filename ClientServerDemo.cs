using System;
using Avro.ipc;
using Avro.ipc.Specific;
using HelloAvro.DTO;

namespace HelloAvro
{
    public class ClientServerDemo
    {
        private const string Hostname = "localhost";

        public void Run()
        {
            Console.WriteLine("Running the Avro client server demo ...");
       
            // Spawn server
            var responder = new SpecificResponder<HelloAvroProtocol>(new Server());
            var server = new SocketServer(Hostname, 0, responder);
            server.Start();
            Console.WriteLine("Server started on port {0}", server.Port);

            // Spawn client(s) 
            var client = new Client(Hostname, server.Port);

            bool onGoing = true;
            do
            {
                client.Run();
                Console.Write("Repeat client operations? (Y/N) ");
                var keyPress = Console.ReadKey().KeyChar;
                if (keyPress == 'n' || keyPress =='N')
                    onGoing = false;
            } while (onGoing);

            // Close the server to free up the port
            server.Stop();

            Console.WriteLine(Environment.NewLine + Helper.HorizontalLine);
        }
    }
}