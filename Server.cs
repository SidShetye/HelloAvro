using System;
using HelloAvro.DTO;

namespace HelloAvro
{
    class Server : HelloAvroProtocol
    {
        private const string ThreadName = "server";

        public override string TransformName(EmployeeDTO employee)
        {
            char[] charArray = employee.Name.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

        public override string hello(string name)
        {
            return String.Concat("Hello " + name + " !");
        }

        public override int add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }

        public override int divide(int arg1, int arg2)
        {
            int result;
            try
            {
                result = (arg1/arg2); // we don't make this floating to intentionally trigger divide by 0 exception
            }
            catch (Exception ex)
            {
                var err = new ServerError();
                err.message = ex.ToString();
                throw err;
            }

            return result;
        }

        public override void heartbeat()
        {
            Helper.WriteLine(ThreadName, "Received a heartbeat ping from client ...");
        }
    }
}
