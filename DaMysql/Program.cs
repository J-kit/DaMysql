using System.Collections.Generic;
using DaMysql.Mysql;
using System.Diagnostics;

namespace DaMysql
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var db = new MysqlProvider(new MysqlConnectionData("127.0.0.1", "csAudioDB", "root", strictMode: true));

            var selectFinalVersion = db.DoSelect("ytdownloadqueue", new YtDownloadQueue() { id = 2, ytid = "agg" });
            var parsed = db.ExecQuery<YtDownloadQueue>("SELECT * FROM `ytDownloadQueue`");
            var parsed1 = db.ExecQuery<Dictionary<string, object>>("SELECT * FROM `ytDownloadQueue`");

            Debugger.Break();
        }
    }
}