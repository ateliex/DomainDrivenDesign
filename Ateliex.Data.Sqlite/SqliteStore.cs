using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.DomainModel;

namespace Ateliex
{
    public class SqliteStore : IAppendOnlyStore
    {
        private readonly string _connectionString;

        public SqliteStore(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Append(string name, byte[] data, long expectedVersion = -1)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    var version = NewMethod(name, expectedVersion, connection, transaction);

                    const string sql = @"
INSERT INTO 'EventStore' ('Name', 'Version', 'Data')
VALUES(@name, @version, @data)
";

                    using (var command = new SqliteCommand(sql, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@name", name);

                        command.Parameters.AddWithValue("@version", version + 1);

                        command.Parameters.AddWithValue("@data", data);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
            }
        }

        private static long NewMethod(string name, long expectedVersion, SqliteConnection connection, SqliteTransaction transaction)
        {
            const string sql = @"
SELECT COALESCE(MAX(Version),0)
FROM 'EventStore'
WHERE Name=@name
";

            using (var command = new SqliteCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@name", name);

                var version = (long)command.ExecuteScalar();

                if (expectedVersion != -1)
                {
                    if (version != expectedVersion)
                    {
                        throw new AppendOnlyStoreConcurrencyException(version, expectedVersion, name);
                    }
                }

                return version;
            }
        }

        public IEnumerable<DataWithVersion> ReadRecords(string name, long afterVersion, long maxCount)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                const string sql = @"
SELECT 'Data', 'Version' FROM 'EventStore'
WHERE 'Name' = @name AND 'Version' > @version
ORDER BY 'Version'
LIMIT 0, @take
";

                using (var command = new SqliteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    
                    command.Parameters.AddWithValue("@version", afterVersion);
                    
                    command.Parameters.AddWithValue("@take", maxCount);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var data = (byte[])reader["Data"];
                            
                            var version = (long)reader["Version"];

                            yield return new DataWithVersion(data, version);
                        }
                    }
                }
            }
        }

        public IEnumerable<DataWithName> ReadRecords(long afterVersion, long maxCount)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {

        }

        public void Dispose()
        {

        }
    }
}
