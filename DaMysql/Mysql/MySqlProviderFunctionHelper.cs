using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DaMysql.Mysql
{
    /// <summary>
    /// Contains useful functions which doesn't really fit into the main mysql class
    /// </summary>
    internal class MySqlFunctionHelper
    {
        internal static string GenerateTypeCorrection(string query, MySqlDataReader genFrom)
        {
            var classRes = new List<string>();
            var fieldNames = Enumerable.Range(0, genFrom.FieldCount).Select(genFrom.GetName).ToArray();
            var tableName = ParseFirstTable(query);
            classRes.Add("class " + tableName);
            classRes.Add("{");
            classRes.AddRange(from t in fieldNames let typeName = genFrom[t].GetType().FullName select $"\tpublic {typeName} {t}{{get; set; }}");
            classRes.Add("}");

            return string.Join("\r\n", classRes.ToArray());
        }

        private static readonly Regex RgxTablename = new Regex(@"FROM.*?([A-Za-z]+)", RegexOptions.Compiled | RegexOptions.ECMAScript);

        internal static string ParseFirstTable(string input)
        {
            try
            {
                return RgxTablename.Match(input).Groups[1].Value;
            }
            catch
            {
                return "sampleTbl";
            }
        }

        /// <summary>
        /// Generates a select statement which selects items from a table where items from toGenObj are not null
        /// </summary>
        /// <param name="table"></param>
        /// <param name="toGenObj"></param>
        /// <returns></returns>
        public static SelectStatementContainer SelectStatementGenerator(string table, object toGenObj)
        {
            var selection = new List<string>();
            var paramContainer = new List<MysqlParamContainer>();

            var tObjProb = toGenObj.GetType().GetProperties();
            foreach (var tObjProbItem in tObjProb)
            {
                var probValue = tObjProbItem.GetValue(toGenObj);
                selection.Add(tObjProbItem.Name);
                if (probValue != null)
                {
                    paramContainer.Add(new MysqlParamContainer() { Name = tObjProbItem.Name, Value = probValue });
                }
            }
            string statHelper = $"Select {string.Join(",", selection)} From `{table}`";
            if (paramContainer.Count != 0)
            {
                statHelper += " where ";
                for (var i = 0; i < paramContainer.Count; i++)
                {
                    if (i != 0)
                    {
                        statHelper += " and ";
                    }

                    statHelper += $"{paramContainer[i].Name} = @par{i}";
                    paramContainer[i].Name = "@par" + i.ToString(); //For further preparing usage
                }
            }
            return new SelectStatementContainer() { Statement = statHelper, EscapedStrings = paramContainer.ToArray() };
        }
    }
}