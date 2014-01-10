using System;
using System.Collections.Generic;
using HelloAvro.DTO;

namespace HelloAvro
{
    class Helper
    {
        public static string HorizontalLine = "==============================================";
        public static EmployeeDTO CreateEmployee()
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

        public static void WriteLine(string who, string format, params Object[] arg)
        {
            Console.Write(who + ": ");
            Console.WriteLine(format,arg);
        }
    }
}
