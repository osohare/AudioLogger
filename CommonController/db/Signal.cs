using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace CommonController.db
{
    public class Signal
    {
        public static List<KLogger> GetAllFromDatabase()
        {
            Database db = DatabaseFactory.CreateDatabase();
            String strSql =
                "SELECT * " +
                "FROM tblSignals ";
            IDataReader dr = db.ExecuteReader(db.GetSqlStringCommand(strSql));

            List<KLogger> loggers = new List<KLogger>();

            if (dr != null && !dr.IsClosed)
            {
                while (dr.Read())
                {
                    KLogger k = new KLogger();
                    k.stationID = dr.GetInt64(0);
                    if (!dr.IsDBNull(1)) k.station = dr.GetString(1);
                    if (!dr.IsDBNull(2)) k.deviceName = dr.GetString(2);
                    if (!dr.IsDBNull(3)) k.lineName = dr.GetString(3);
                    if (!dr.IsDBNull(4)) k.bitRate = EnumFromValue.FromBitRate(dr.GetInt32(4));
                    if (!dr.IsDBNull(5)) k.sampleRate = EnumFromValue.FromSampleRate(dr.GetInt32(5));
                    if (!dr.IsDBNull(6)) k.volume = (uint)dr.GetInt32(6);
                    if (!dr.IsDBNull(7)) k.recycleInterval = dr.GetInt32(7);
                    if (!dr.IsDBNull(8)) k.workingDirectory = dr.GetString(8);
                    if (!dr.IsDBNull(9)) k.enabled = dr.GetBoolean(9);
                    loggers.Add(k);
                }
            }
            dr.Close();
            return loggers;
        }

        public static KLogger GetFromDatabase(int id)
        {
            Database db = DatabaseFactory.CreateDatabase();
            String strSql = 
			    "SELECT * " +
			    "FROM tblSignals " +
			    "WHERE clv_signal = " + id;
            IDataReader dr = db.ExecuteReader(db.GetSqlStringCommand(strSql));
            
            KLogger k = new KLogger();

            if (dr != null && !dr.IsClosed)
            {
                if (!dr.Read()) throw new Exception("This signal does not exist in the database");
                k.stationID = dr.GetInt64(0);
                if (!dr.IsDBNull(1)) k.station = dr.GetString(1);
                if (!dr.IsDBNull(2)) k.deviceName = dr.GetString(2);
                if (!dr.IsDBNull(3)) k.lineName = dr.GetString(3);
                if (!dr.IsDBNull(4)) k.bitRate = EnumFromValue.FromBitRate(dr.GetInt32(4));
                if (!dr.IsDBNull(5)) k.sampleRate = EnumFromValue.FromSampleRate(dr.GetInt32(5));
                if (!dr.IsDBNull(6)) k.volume = (uint)dr.GetInt32(6);
                if (!dr.IsDBNull(7)) k.recycleInterval = dr.GetInt32(7);
                if (!dr.IsDBNull(8)) k.workingDirectory = dr.GetString(8);
                if (!dr.IsDBNull(9)) k.enabled = dr.GetBoolean(9);
            }
            dr.Close();
            return k;
        }

        public static void RefreshFromDatabase(KLogger k)
        {
            Database db = DatabaseFactory.CreateDatabase();
            String strSql =
                "SELECT * " +
                "FROM tblSignals " +
                "WHERE clv_signal = " + k.stationID;
            IDataReader dr = db.ExecuteReader(db.GetSqlStringCommand(strSql));

            if (dr != null && !dr.IsClosed)
            {
                if (!dr.Read()) throw new Exception("This signal does not exist in the database");
                k.stationID = dr.GetInt64(0);
                if (!dr.IsDBNull(1)) k.station = dr.GetString(1);
                if (!dr.IsDBNull(2)) k.deviceName = dr.GetString(2);
                if (!dr.IsDBNull(3)) k.lineName = dr.GetString(3);
                if (!dr.IsDBNull(4)) k.bitRate = EnumFromValue.FromBitRate(dr.GetInt32(4));
                if (!dr.IsDBNull(5)) k.sampleRate = EnumFromValue.FromSampleRate(dr.GetInt32(5));
                if (!dr.IsDBNull(6)) k.volume = (uint)dr.GetInt32(6);
                if (!dr.IsDBNull(7)) k.recycleInterval = dr.GetInt32(7);
                if (!dr.IsDBNull(8)) k.workingDirectory = dr.GetString(8);
                if (!dr.IsDBNull(9)) k.enabled = dr.GetBoolean(9);
            }
            dr.Close();
        }

        public static bool Update(KLogger k)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_UpdateSignal");
            db.AddInParameter(dbc, "@clv_signal", DbType.Int64, k.stationID);
            db.AddInParameter(dbc, "@signal", DbType.AnsiString, k.station);
            db.AddInParameter(dbc, "@deviceName", DbType.AnsiString, k.deviceName);
            db.AddInParameter(dbc, "@lineName", DbType.AnsiString, k.lineName);
            db.AddInParameter(dbc, "@bitRate", DbType.Int32, k.bitRate.GetHashCode());
            db.AddInParameter(dbc, "@sampleRate", DbType.Int32, k.sampleRate.GetHashCode());
            db.AddInParameter(dbc, "@volume", DbType.Int32, (int)k.volume);
            db.AddInParameter(dbc, "@recycleInterval", DbType.Int32, k.recycleInterval);
            db.AddInParameter(dbc, "@workingDirectory", DbType.AnsiString, k.workingDirectory);
            db.AddInParameter(dbc, "@enabled", DbType.Boolean, k.enabled);
            return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

        public static bool Insert(KLogger k)
        {
            Database db = DatabaseFactory.CreateDatabase();
       		DbCommand dbc = db.GetStoredProcCommand("F_InsertSignal");
            db.AddOutParameter(dbc, "@clv_signal", DbType.Int64, sizeof(Int64));
            db.AddInParameter(dbc, "@signal", DbType.AnsiString, k.station);
            db.AddInParameter(dbc, "@deviceName", DbType.AnsiString, k.deviceName);
            db.AddInParameter(dbc, "@lineName", DbType.AnsiString, k.lineName);
            db.AddInParameter(dbc, "@bitRate", DbType.Int32, k.bitRate.GetHashCode());
            db.AddInParameter(dbc, "@sampleRate", DbType.Int32, k.sampleRate.GetHashCode());
            db.AddInParameter(dbc, "@volume", DbType.Int32, (int)k.volume);
            db.AddInParameter(dbc, "@recycleInterval", DbType.Int32, k.recycleInterval);
            db.AddInParameter(dbc, "@workingDirectory", DbType.AnsiString, k.workingDirectory);
            db.AddInParameter(dbc, "@enabled", DbType.Boolean, k.enabled);
		    return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

        public static bool Delete(Int64 stationID)
        {
            Database db = DatabaseFactory.CreateDatabase();
            DbCommand dbc = db.GetStoredProcCommand("F_DeleteSignal");
            db.AddInParameter(dbc, "@clv_signal", DbType.Int64, stationID);
            return (db.ExecuteNonQuery(dbc) > 0 ? true : false);
        }

    }
}
