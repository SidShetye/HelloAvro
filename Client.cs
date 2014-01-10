using System;
using Avro.ipc;
using Avro.ipc.Specific;
using HelloAvro.DTO;

namespace HelloAvro
{
    public class Client
    {
        private const string ThreadName = "client";
        private readonly HelloAvroProtocolCallback _serverProxy;
        private readonly Random _rng;

        public Client(string hostname, int serverport)
        {
            var transceiver = new SocketTransceiver(hostname, serverport);
            _serverProxy = SpecificRequestor.CreateClient<HelloAvroProtocolCallback>(transceiver);
            _rng = new Random(); // this isn't cryptographically strong 
        }

        public void Run()
        {
            // Send a one-way message to the server
            Helper.WriteLine(ThreadName,
                "heartbeat() - i.e. calling our 'heartbeat' one-way RPC on the server (no response expected) ...");
            _serverProxy.heartbeat();

            // "TransformName" RPC, we send a complex object (automatically serialized along the way)
            var employee = Helper.CreateEmployee();
            var resultName = _serverProxy.TransformName(employee);
            Helper.WriteLine(ThreadName, "TransformName({0}) = {1} (we sent an entire complex record)", employee.Name,
                resultName);

            // "Hello" RPC
            const string myName = "Avro";
            var myHello = _serverProxy.hello("Avro");
            Helper.WriteLine(ThreadName, "hello({0}) = {1}", myName, myHello);

            // "Add" server RPC
            int arg0 = _rng.Next(20);
            int arg1 = _rng.Next(20);
            var add = _serverProxy.add(arg0, arg1);
            Helper.WriteLine(ThreadName, "add({0}, {1}) = {2}", arg0, arg1, add);

            // "Divide" server RPC
            if (arg1 == 0) ++arg1; // just to avoid any divide by zero exception HERE ! (we demo it down)
            var divide = _serverProxy.divide(arg0, arg1);
            Helper.WriteLine(ThreadName, "divide({0}, {1}) = {2}", arg0, arg1, divide);

            // "Divide" server RPC but can throw native exceptions remotely
            try
            {
                arg1 = 0;
                divide = _serverProxy.divide(arg0, arg1);
                Helper.WriteLine(ThreadName, "divide({0}, {1}) = {2}", arg0, arg1, divide);
            }
            catch (ServerError ex)
            {
                Helper.WriteLine(ThreadName, "divide({0}, {1}) received exception (AS EXPECTED). Details: {2}", arg0,
                    arg1, ex.message);
            }
        }
    }
}