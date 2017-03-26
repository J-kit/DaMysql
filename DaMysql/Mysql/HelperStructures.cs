namespace DaMysql.Mysql
{
    internal class MysqlField
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    internal class MysqlParamContainer
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }

    internal class RunQueryAsVoid { }

    internal class HelperStructures
    {
    }

    internal class SelectStatementContainer
    {
        public string Statement { get; set; }
        public MysqlParamContainer[] EscapedStrings { get; set; }
    }
}