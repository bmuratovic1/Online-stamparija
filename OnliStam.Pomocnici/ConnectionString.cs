using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnliStam.Pomocnici
{
    public class ConnectionString
    {
        #region == Fields ==


        #endregion

        #region == Coonstructors ==

        public ConnectionString(string connectionString)
        {
            foreach(string kvp in connectionString.Split(';'))
            {
                if(string.IsNullOrEmpty(kvp))
                    continue;
                var values = kvp.Split('=');
                if(values.Count() != 2)
                    throw new ArgumentException("ConnectionString contains invalid value! Value = " + kvp);
                switch(values[0].ToUpper())
                {
                    case "SERVER":
                        Server = values[1];
                        break;
                    case "PORT":
                        int port = 0;
                        if(int.TryParse(values[1], out port)) Port = port;
                        break;
                    case "DATABASE":
                        Database = values[1];
                        break;
                    case "UID":
                        UserName = values[1];
                        break;
                    case "PASSWORD":
                        Password = values[1];
                        break;
                }
                FullString = connectionString;
            }
        }

        #endregion

        #region == Properties ==

        public string Server { get; set; }

        public int Port { get; set; }

        public string Database { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FullString { get; set; }

        #endregion

        #region == Methods ==

        public override string ToString()
        {
            return string.Format("{0}@{1}", Database, Server);
        }

        public override bool Equals(object obj)
        {
            if(obj is ConnectionString)
            {
                var c2 = obj as ConnectionString;
                return c2.Database == Database && c2.Password == Password && c2.Port == Port && c2.Server == Server && c2.UserName == UserName;
            }
            return base.Equals(obj);
        }

        #endregion
    }
}
