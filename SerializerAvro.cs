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

using System.IO;
using Avro.IO;
using Avro.Specific;

namespace HelloAvro
{
    public static class SerializerAvro
    {
        public static byte[] Serialize<T>(T thisObj) where T : ISpecificRecord
        {
            using (var ms = new MemoryStream())
            {
                var enc = new BinaryEncoder(ms);
                var writer = new SpecificDefaultWriter(thisObj.Schema); // Schema comes from pre-compiled, code-gen phase
                writer.Write(thisObj, enc);
                return ms.ToArray();
            }
        }

        public static T Deserialize<T>(byte[] bytes) where T : ISpecificRecord, new()
        {
            using (var ms = new MemoryStream(bytes))
            {
                var dec = new BinaryDecoder(ms);
                var regenObj = new T();

                var reader = new SpecificDefaultReader(regenObj.Schema, regenObj.Schema);
                reader.Read(regenObj, dec);
                return regenObj;
            }
        }

        /// <summary>
        /// Same as the deserialize method above but we allow called to provider an existing
        /// Avro DTO object that can be reused across calls to avoid new'ing an object
        /// for each call (for high performance situations)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="regenObj"></param>
        /// <returns></returns>
        public static T DeserializeReuseObject<T>(byte[] bytes, T regenObj) where T : ISpecificRecord
        {
            using (var ms = new MemoryStream(bytes))
            {
                var dec = new BinaryDecoder(ms);

                var reader = new SpecificDefaultReader(regenObj.Schema, regenObj.Schema);
                reader.Read(regenObj, dec);
                return regenObj;
            }
        }
    }
}
