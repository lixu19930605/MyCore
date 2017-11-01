using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MyCore.Repository
{
    public class TestRepository
    {
        public DataTable Test()
        {
            SqlConnection sqlConnection = new SqlConnection("server=.;user=sa;pwd=lixu19930605.");
            sqlConnection.Open();

            DataTable dt = new DataTable();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = @"SELECT name FROM sys.sysdatabases WITH(NOLOCK) ORDER BY sid DESC";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd.CommandText, sqlConnection);

            sqlDataAdapter.Fill(dt);

            return dt;
        }

        public DataTable Get(string tableName)
        {
            SqlConnection sqlConnection = new SqlConnection("");
            sqlConnection.Open();

            DataTable dt = new DataTable();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = $@"
                                SELECT  d.name TableName,
                                        a.colorder 字段序号,
                                        a.name FieldName,
                                        --(CASE WHEN COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 THEN '√' ELSE '' END) 标识,
                                        --(CASE WHEN (
                                        --    SELECT COUNT(*)
                                        --    FROM sysobjects
                                        --    WHERE (name IN
                                        --              (SELECT name
                                        --              FROM sysindexes
                                        --              WHERE (id = a.id) AND (indid IN
                                        --                        (SELECT indid
                                        --                        FROM sysindexkeys
                                        --                      WHERE (id = a.id) AND (colid IN
                                        --                    (SELECT colid
                                        --                                FROM syscolumns
                                        --                                WHERE (id = a.id) AND (name = a.name))))))) AND
                                        --            (xtype = 'PK'))>0 THEN '√' ELSE '' END) 主键,
                                        b.name Type,
                                        --a.length 占用字节数,
                                        COLUMNPROPERTY(a.id,a.name,'PRECISION') AS Length,
                                        --ISNULL(COLUMNPROPERTY(a.id,a.name,'Scale'),0) AS 小数位数,
                                        --(CASE WHEN a.isnullable=1 THEN '√' ELSE '' END) 允许空,
                                        ISNULL(e.text,'') DefaultValue,
                                      ISNULL(g.[value],'') AS FieldDescription
                                FROM syscolumns a
                                    LEFT JOIN systypes b ON a.xtype=b.xusertype
                                    INNER JOIN sysobjects d ON a.id=d.id AND d.xtype='U' AND d.name <>'dtproperties'
                                    LEFT JOIN syscomments e ON a.cdefault=e.id
                                    LEFT JOIN sys.extended_properties g  ON a.id=g.major_id AND a.colid = g.minor_id
                                WHERE d.name='{tableName}'
                                ORDER BY a.id,a.colorder";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd.CommandText, sqlConnection);

            sqlDataAdapter.Fill(dt);
            sqlConnection.Close();

            return dt;
        }

        public DataTable GetTableName(string dataBaseName)
        {
            SqlConnection sqlConnection = new SqlConnection("server=.;user=sa;pwd=lixu19930605.");
            sqlConnection.Open();

            DataTable dt = new DataTable();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandText = $@"USE {dataBaseName} SELECT Name FROM SysObjects Where XType='U' ORDER BY Name";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd.CommandText, sqlConnection);

            sqlDataAdapter.Fill(dt);

            return dt;
        }
    }
}
