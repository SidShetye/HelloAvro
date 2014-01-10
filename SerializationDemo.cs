using System;
using HelloAvro.DTO;
using Newtonsoft.Json;

namespace HelloAvro
{
    /// <summary>
    /// This class demonstrates how to perform just serialization and deserialization in Avro. 
    /// No RPC/IPC related demonstration here.
    /// </summary>
    class SerializationDemo
    {
        public void Run()
        {
            Console.WriteLine("Running the Avro serialization demo ...");

            var employee = Helper.CreateEmployee();

            // Serialization
            var bytes = SerializerAvro.Serialize(employee);
            Console.WriteLine("Serialized object to {0} bytes", bytes.Length);
            Console.WriteLine("Bytes are: {0}", BitConverter.ToString(bytes));

            // Deserialization
            Console.WriteLine("Deserializing bytes back into object ... " + Environment.NewLine);
            var regenerated = SerializerAvro.Deserialize<EmployeeDTO>(bytes);

            // Verification : We compare original object with the object regenerated
            // after passing through a serialize=>deserialize round trip. 
            // We compare the Json equivalent of this object to keep it simple and lazy
            // and don't feel like implementing a proper 'Equals' method
            // FYI, Json usage here has NOTHING to do with Avro serialization 
            var origJson = JsonConvert.SerializeObject(employee);
            var regenJson = JsonConvert.SerializeObject(regenerated);

            if (origJson.Equals(regenJson))
                Console.WriteLine("Success. Object through the serialize=>deserialize round trip are identical.");
            else

                Console.WriteLine("FAILED! We lost data during the serialize=>deserialize round trip");

            //Console.WriteLine(Environment.NewLine + "Press any key to exit ...");
            //Console.ReadLine();
            Console.WriteLine(Helper.HorizontalLine);
        }
    }
}
