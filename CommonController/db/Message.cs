using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CommonController.db
{
    public class Message
    {
        protected long _id;
        protected string _user = String.Empty;
        protected DateTime _dateTime;
        protected string _command = String.Empty;
        protected byte[] _optionalData;
        protected bool _executed;

        public long ID
        {
            get {return _id; }
        }

        public DateTime Timestamp
        {
            get { return _dateTime; }
        }

        public LoggerCustomCommands Command
        {
            get { return LoggerCustomCommands.Deserialize(_command); }
        }

        public static List<Message> getPendingMessages()
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetSqlStringCommand("SELECT * FROM tblMessages WHERE Executed = 0 ORDER BY DateTime");
            IDataReader reader = db.ExecuteReader(dbc);

            List<Message> messages = new List<Message>();

            while (reader.Read())
            {
                messages.Add(LoadFromReader(reader));
            }
            reader.Close();
            return messages;
        }

        protected static Message LoadFromReader(IDataReader reader)
        {
            Message m = new Message();
            if (reader != null && !reader.IsClosed)
            {
                m._id = reader.GetInt64(0);
                if (!reader.IsDBNull(1)) m._user = reader.GetString(1);
                if (!reader.IsDBNull(2)) m._dateTime = reader.GetDateTime(2);
                if (!reader.IsDBNull(3)) m._command = reader.GetString(3);
                if (!reader.IsDBNull(4)) m._optionalData = null;
                if (!reader.IsDBNull(5)) m._executed = reader.GetBoolean(5);
            }
            return m;
        }

        public static long Insert(String cmd, object OptionalData)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_InsertMessage");
            db.AddOutParameter(dbc, "@clv_message", DbType.Int64, sizeof(Int64));
            db.AddInParameter(dbc, "@User", DbType.AnsiString, DBNull.Value);
            db.AddInParameter(dbc, "@Command", DbType.AnsiString, cmd);
            db.AddInParameter(dbc, "@OptionalData", DbType.Binary, OptionalData);
            db.ExecuteNonQuery(dbc);
            return (long)db.GetParameterValue(dbc, "@clv_message");
        }

        public static bool Update(long clv_message)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_UpdateMessage");
            db.AddInParameter(dbc, "@clv_message", DbType.Int64, clv_message);
            return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

    }
}
