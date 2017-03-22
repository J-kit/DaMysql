using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaMysql.Mysql
{
    internal class mysqlField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    internal class mysqlParamContainer
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    internal class runQueryAsVoid { }

    internal class HelperStructures
    {
    }

    internal class SelectStatementContainer
    {
        public string Statement { get; set; }
        public mysqlParamContainer[] escapedStrings { get; set; }
    }
}