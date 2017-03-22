using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaMysql.Mysql;
using System.Diagnostics;

namespace DaMysql
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new MysqlProvider(new MysqlConnectionData("127.0.0.1", "csAudioDB", "root"));

            var selectFinalVersion = db.DoSelect("ytdownloadqueue", new ytDownloadQueue() { id = 2, ytid = "agg" });
            Debugger.Break();
        }
    }

    public class ytDownloadQueue
    {
        public uint? id { get; set; }
        public string ytid { get; set; }
        public string title { get; set; }
    }
}