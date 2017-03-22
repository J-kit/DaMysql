using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DaMysql.Mysql
{
    /// <summary>
    /// Contains useful functions which doesn't really fit into the main mysql class
    /// </summary>
    class MySqlFunctionHelper
    {
        internal static string generateTypeCorrection(string query, MySqlDataReader genFrom)
        {
            List<string> classRes = new List<string>();
            var fieldNames = Enumerable.Range(0, genFrom.FieldCount).Select(i => genFrom.GetName(i)).ToArray();
            var tableName = parseFirstTable(query);
            classRes.Add("class " + tableName);
            classRes.Add("{");
            for (int i = 0; i < fieldNames.Length; i++)
            {
                var typeName = genFrom[fieldNames[i]].GetType().FullName;
                classRes.Add(string.Format("\tpublic {0} {1}{{get; set; }}", typeName, fieldNames[i]));
            }
            classRes.Add("}");

            return string.Join("\r\n", classRes.ToArray());
        }

        static readonly Regex rgxTablename = new Regex(@"FROM.*?([A-Za-z]+)", RegexOptions.Compiled | RegexOptions.ECMAScript);
        internal static string parseFirstTable(string input)
        {
            try
            {
                return rgxTablename.Match(input).Groups[1].Value;
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
        public static SelectStatementContainer selectStatementGenerator(string table, object toGenObj)
        {
            List<string> selection = new List<string>();
            List<mysqlParamContainer> paramContainer = new List<mysqlParamContainer>();

            var tObjProb = toGenObj.GetType().GetProperties();
            object probValue;
            foreach (var tObjProbItem in tObjProb)
            {
                probValue = tObjProbItem.GetValue(toGenObj);
                selection.Add(tObjProbItem.Name);
                if (probValue != null)
                {
                    paramContainer.Add(new mysqlParamContainer() { Name = tObjProbItem.Name, Value = probValue });
                }
            }
            string statHelper = string.Format("Select {0} From `{1}`", string.Join(",", selection), table);
            if (paramContainer.Count != 0)
            {
                statHelper += " where ";
                for (int i = 0; i < paramContainer.Count; i++)
                {
                    if (i != 0)
                    {
                        statHelper += " and ";
                    }

                    statHelper += string.Format("{0} = @par{1}", paramContainer[i].Name, i);
                    paramContainer[i].Name = "@par" + i.ToString(); //For further preparing usage
                }
            }
            return new SelectStatementContainer() { Statement = statHelper, escapedStrings = paramContainer.ToArray() };
        }

    }
}
