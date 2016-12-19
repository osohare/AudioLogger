using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CommonController.db
{
    public class TimeLine
    {
        protected long _id;
        protected long _clv_signal;
        protected DateTime _timeline;
        protected long _ticks;

        public long Id
        {
            get { return _id; }
        }
        public long clv_signal
        {
            get { return _clv_signal; }
        }
        public DateTime Timeline
        {
            get { return _timeline; }
        }
        public long Ticks
        {
            get { return _ticks; }
        }


        public static List<TimeLine> getMediaContent(int clv_signal, DateTime t0, DateTime t1)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetSqlStringCommand("SELECT * FROM tblTimeLine WHERE clv_signal = @clv_signal AND timeline BETWEEN @t0 AND @t1");
            db.AddInParameter(dbc, "@clv_signal", DbType.Int16, clv_signal);
            db.AddInParameter(dbc, "@t0", DbType.DateTime, t0);
            db.AddInParameter(dbc, "@t1", DbType.DateTime, t1);
            IDataReader reader = db.ExecuteReader(dbc);

            List<TimeLine> timelines = new List<TimeLine>();

            while (reader.Read())
            {
                timelines.Add(LoadFromReader(reader));
            }
            reader.Close();
            return timelines;
        }

        protected static TimeLine LoadFromReader(IDataReader reader)
        {
            TimeLine t = new TimeLine();
            if (reader != null && !reader.IsClosed)
            {
                t._id = reader.GetInt64(0);
                if (!reader.IsDBNull(1)) t._clv_signal = reader.GetInt64(1);
                if (!reader.IsDBNull(2)) t._timeline = reader.GetDateTime(2);
                if (!reader.IsDBNull(3)) t._ticks = reader.GetInt64(3);
            }
            return t;
        }
        public static bool Insert(KLogger k, String filename)
        {
            filename = filename.Substring(filename.LastIndexOf("\\") + 1);
            filename = filename.Substring(0, filename.Length - 4);

            Int64 ticks = Int64.Parse(filename.Split('.')[0]);
            DateTime timeline = new DateTime(ticks);

            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_InsertTimeLine");
            db.AddOutParameter(dbc, "@clv_timeline", DbType.Int64, sizeof(Int64));
            db.AddInParameter(dbc, "@clv_signal", DbType.Int64, k.stationID);
		    db.AddInParameter(dbc, "@timeline", DbType.DateTime, timeline);
		    db.AddInParameter(dbc, "@ticks", DbType.Int64, ticks);
            return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

        public static bool Delete(Int64 clv_timeline)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_DeleteTimeLine");
            db.AddInParameter(dbc, "@clv_timeline", DbType.Int64, clv_timeline);
            return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

    }
}
