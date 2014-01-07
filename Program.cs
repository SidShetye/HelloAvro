// Copyright 2013, Sid Shetye
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloAvro.DTO;
using Newtonsoft.Json;

namespace HelloAvro
{
    class Program
    {
        static void Main(string[] args)
        {
            var employee = CreateEmployee();

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

            Console.WriteLine(Environment.NewLine + "Press any key to exit ...");
            Console.ReadLine();
        }

        static EmployeeDTO CreateEmployee()
        {
            var e = new EmployeeDTO();
            e.EmployeeId = 1;
            e.Name = "Cartman";
            e.ActiveProjects = new List<ProjectDTO>();
            var proj1 = new ProjectDTO();
            proj1.ProjectId = 1;
            proj1.ProjectName = "Southfield";
            var proj2 = new ProjectDTO();
            proj2.ProjectId = 2;
            proj2.ProjectName = "Resurrect Kenny";
            e.ActiveProjects.Add(proj2);
            e.Reportees = new[] { 2, 3 };
            e.RawBytes = new byte[] { 0x00, 0x01, 0x02, 0x03 };
            e.Notes = "Quite the animated character";
            e.StillWorksHere = true;

            return e;
        }
    }
}
