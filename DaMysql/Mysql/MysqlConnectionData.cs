namespace DaMysql.Mysql
{
    class MysqlConnectionData
    {
        public string Server { get { return _server; } set { _server = value; } }
        public string Port { get { return _port; } set { _port = value; } }
        public string Db { get { return _db; } set { _db = value; } }
        public string User { get { return _user; } set { _user = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public bool Pooling { get { return _pooling; } set { _pooling = value; } }
        public bool StrictMode { get { return _strictMode; } set { _strictMode = value; } }

        string _server;
        string _port;
        string _db;
        string _user;
        string _password;
        bool _pooling;
        bool _strictMode;

        public MysqlConnectionData() { }
        public MysqlConnectionData(string server, string db, string user, string password = "", string port = "3306", bool pooling = false, bool strictMode = false)
        {
            _server = server;
            _port = port;
            _db = db;
            _user = user;
            _password = password;
            _pooling = pooling;
            _strictMode = strictMode;
        }
        public override string ToString()
        {
            return string.Format("Server={0};Port={1};Database={2};User ID={3};Password={4};Pooling={5}", _server, _port, _db, _user, _password, (_pooling ? "true" : "false"));
        }
    }
}
