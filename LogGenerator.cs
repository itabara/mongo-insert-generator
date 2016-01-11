using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MongoDBGenerator
{
    internal class LogGenerator
    {
        private List<string> actions = new List<string>() { "VIEW", "DOWNLOAD", "UPDATE" };
        private Dictionary<int, List<string>> cookies = new Dictionary<int, List<string>>()
        {
            { 1, new List<string>() { "'auth'", "'lastpage'", "'firstvisit'" } },
            { 2, new List<string>() { "'session'", "'guest'", "'lastpage'"} },
            { 3, new List<string>() { "'session'", "'admin'"} }
        };

        internal void GenerateLog()
        {            
            var owner_objectids = File.ReadAllLines("../../data/objectids-owner.txt").Select(line => line.Split(' ')).ToList();
            var ipaddresses = File.ReadAllLines("../../data/ipaddresses.txt").Select(line => line.Split(' ')).ToList();            
            var cities = File.ReadAllLines("../../data/cities.csv").Select(line => line.Split(' ')).ToList(); // "[long, long]"

            File.Delete("output.js");

            StringBuilder document = new StringBuilder();
            var r = new Random();
            for (int row = 0; row < 20000; row++)
            {                
                document.Append("db.logs.insert({");
                document.AppendFormat("\"_id\": {0}", row);
                document.AppendFormat(",\"request_ip\": \"{0}\"", ipaddresses.RandomChoice().Single());
                document.AppendFormat(",\"owner\": ObjectId(\"{0}\")", owner_objectids.RandomChoice().Single());                
                document.Append(",\"request_date\": Date()");
                document.AppendFormat(",\"request_method\": {0}", "\"GET\"");
                document.AppendFormat(",\"request_uri\": \"iuliantabara.com\\posts\\{0}\"", row);
                document.AppendFormat(",\"action\": \"VIEW\"");
                document.AppendFormat(",\"request_time_milliseconds\":{0}", r.Next(400));
                document.AppendFormat(",\"loc\":{0}", cities.RandomChoice().Single());
                document.AppendFormat(",\"cookies\":[{0}]", String.Join(",", cookies.RandomChoice().Value.ToArray()));
                document.AppendLine("});");              
            }
            File.AppendAllText("output.js", document.ToString());
        }


    }
}
