using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Model
{
    public class SingleConnection
    {
        private SingleConnection() { }
        private static SingleConnection _ConsString = null;
        private String _String = null;
        private static string _dataBase = Properties.Settings.Default.dataBase;
        private static List<string> _logins = new List<string>
        {
            @"user id=1146_reader;password=111111;",
            @"user id=1146_admin;password=masterkey;"
        };
        private static int _loginSelect = Properties.Settings.Default.loginSelect;
        
        public static int LoginSelect
        {
            get
            {
                return _loginSelect;
            }
            set
            {
                if(_logins.Count <= value)
                    _loginSelect = _logins.Count;
                else
                    _loginSelect = value;
                Properties.Settings.Default.loginSelect = _loginSelect;
                Properties.Settings.Default.Save();
            }
        }
        public static string DataBase
        {
            get
            {
                return _dataBase;
            }
            set
            {
                _dataBase = value;
                Properties.Settings.Default.dataBase = _dataBase;
                Properties.Settings.Default.Save();
            }
        }
        public static string Logins
        {
            get
            {
                return _logins[_loginSelect];
            }
            set
            {
                _logins.Add(value); _loginSelect = _logins.IndexOf(value);
            }
        }
        public static string ConString
        {
            get
            {
                if (_ConsString == null)
                {
                    _ConsString = new SingleConnection { _String = SingleConnection.Connect() };
                    return _ConsString._String;
                }
                else
                    return _ConsString._String;
            }
        }
        public static string Connect()
        {
            //Build an SQL connection string
            SqlConnectionStringBuilder sqlString = new SqlConnectionStringBuilder()
            {
                DataSource = "SIPL35\\SQL2016".ToString(), // Server name
                InitialCatalog = "Join8ShopDB",  //Database
                UserID = "Sa",         //Username
                Password = "Sa123!@#",  //Password
            };
            //Build an Entity Framework connection string
            EntityConnectionStringBuilder entityString = new EntityConnectionStringBuilder()
            {
                Provider = "System.Data.SqlClient",
                Metadata = "res://*/Model.OASU.csdl|res://*/Model.OASU.ssdl|res://*/Model.OASU.msl",
                ProviderConnectionString = String.Format(@"data source={0};initial catalog=db1146;{1}", _dataBase, _logins[_loginSelect])// sqlString.ToString()
            };
            return entityString.ConnectionString;
        }
    }
}
