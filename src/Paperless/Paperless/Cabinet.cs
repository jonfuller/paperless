using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MongoDB.Driver;

namespace Paperless
{
    public class Cabinet : IDisposable
    {
        private Mongo _mongo;
        private Database _database;

        public Cabinet(Mongo mongo, string dbName)
        {
            _mongo = mongo;
            _mongo.Connect();
            _database = _mongo.getDB(dbName);
        }

        public void FileDocument(DateTime date, IEnumerable<string> tags, string originalFilename, IEnumerable<byte> content)
        {
            var collection = _database.GetCollection("docs");

            collection.Insert(new Document()
                                  {
                                      {"tags", tags},
                                      {"date", date},
                                      {"original_filename", Path.GetFileName(originalFilename)},
                                      {"content", new Binary( content.ToArray())}
                                  });
        }

        public void Dispose()
        {
            _database.Logout();
            _mongo.Disconnect();
        }
    }
}