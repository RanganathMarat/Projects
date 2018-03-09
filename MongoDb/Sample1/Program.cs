using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace Sample1
{

    class Directory
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {

            var connectionString = "mongodb://localhost";
            var mongoClient = new MongoClient(connectionString);

            var mongoServer = mongoClient.GetServer();

            var database = mongoServer.GetDatabase("test");
            Console.WriteLine("Done");

            var directoryCollection = database.GetCollection<Directory>("directory");

            Directory d1 = new Directory();
            d1.Name = "FirstOne";
            directoryCollection.Insert(d1);

            Directory d2 = new Directory();
            d2.Name = "SecondOne";
            directoryCollection.Insert(d2);

            var query = Query<Directory>.EQ(e => e.Name, "FirstOne");
            var d = directoryCollection.FindOne(query);
           Console.WriteLine(d.Name);

        }
    }
}
