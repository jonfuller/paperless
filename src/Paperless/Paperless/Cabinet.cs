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
                                  } );
        }

        public IEnumerable<Tuple<string, int>> GetTags()
        {
            var tags = _database
                .GetCollection( "docs" )
                .Find( new Document(), 0, 0, new Document() { { "tags", "" } } )
                .Documents
                .Map( d => d["tags"] as IEnumerable<string> )
                .Flatten()
                .GroupBy( tag => tag )
                .Map( grp => new Tuple<string, int>() { First = grp.Key, Second = grp.Count() } )
                .ToList();

            return tags;
        }

        public void Dispose()
        {
            _database.Logout();
            _mongo.Disconnect();
        }
    }
}