using System;

namespace DaMysql.Mysql
{
    class MySqlTypeError : Exception
    {
        public string TypeCorrection { get { return _TypeCorrection; } set { _TypeCorrection = value; } }
        string _TypeCorrection;
        public MySqlTypeError()
        {

        }
        public MySqlTypeError(string message,string typeCorrection) : base(message)
        {
            _TypeCorrection = typeCorrection;
        }
        public MySqlTypeError(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
