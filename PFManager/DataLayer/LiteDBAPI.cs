using LiteDB;
using PFManager.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PFManager.DataLayer
{
    public class LiteDBAPI : IDocumentDataBaseAPI
    {
        public string DatabaseName { get; }

        public LiteDBAPI(string dbName)
        {
            string path = Assembly.GetExecutingAssembly().Location;
            path = Path.GetDirectoryName(path);
            DatabaseName = Path.Combine(path, dbName + ".DB");// @"C:\Projects\robotools\RoboTools\RoboTools\bin\Debug\RoboTools.DB";
            MapLiteDBObject();
        }

        public LiteDBAPI(string dbName, string dbPath)
        {
            DatabaseName = Path.Combine(dbPath, dbName + ".DB");
            MapLiteDBObject();
        }

        public void RegisterClassMap<T>()
        {
            //BsonClassMap.RegisterClassMap<T>();
        }

        //public bool AddTest(ITest test)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<ITest> LoadTestList()
        //{
        //    return LoadDocuments<ITest>("Tests");

        //}


        void MapLiteDBObject()
        {
            BsonMapper.Global.IncludeFields = true;
            BsonMapper.Global.EnumAsInteger = true;

            BsonMapper.Global.RegisterType<Type>
            (
                serialize: (type) =>
                {
                    var bson = new BsonDocument();
                    bson["DeclaringTypeName"] = $"{type.FullName}, {type.Module.Name.Replace(".dll", "")}";
                    return bson;
                },
                deserialize: (bson) =>
                {
                    Type type = Type.GetType(bson["DeclaringTypeName"]);
                    return type;
                }
            );


            BsonMapper.Global.RegisterType<MethodInfo>
            (
                serialize: (mi) =>
                {
                    var bson = new BsonDocument();
                    bson["MethodName"] = mi.Name;
                    bson["DeclaringTypeName"] = $"{mi.DeclaringType.FullName}, {mi.ReflectedType.Module.Name.Replace(".dll", "")}";
                    return bson;
                },
                deserialize: (bson) =>
                {
                    Type type = Type.GetType(bson["DeclaringTypeName"]);
                    MethodInfo mi = type.GetMethod(bson["MethodName"]);
                    return mi;
                }
            );

            BsonMapper.Global.RegisterType<PropertyInfo>
            (
                serialize: (pi) =>
                {
                    var bson = new BsonDocument();

                    bson["Name"] = pi.Name;
                    bson["DeclaringTypeName"] = $"{pi.DeclaringType.FullName}, {pi.ReflectedType.Module.Name.Replace(".dll", "")}";
                    return bson;
                },
                deserialize: (bson) =>
                {
                    Type type = Type.GetType(bson["DeclaringTypeName"]);
                    PropertyInfo pi = type.GetProperty(bson["Name"]);
                    return pi;
                }
            );

            BsonMapper.Global.RegisterType<DateTime>
            (
                serialize: (type) =>
                {
                    var bson = new BsonDocument();

                    bson["Date"] = type.Ticks.ToString();
                    return bson;
                },
                deserialize: (bson) =>
                {
                    DateTime type = new DateTime(long.Parse(bson["Date"]));
                    return type;
                }
            );

            BsonMapper.Global.RegisterType<TimeSpan>
            (
                serialize: (type) =>
                {
                    var bson = new BsonDocument();

                    bson["TimeSpan"] = type.Ticks.ToString();
                    return bson;
                },
                deserialize: (bson) =>
                {
                    TimeSpan type = new TimeSpan(long.Parse(bson["TimeSpan"]));
                    return type;
                }
            );

        }

        public bool DeleteCollection(string collectionName)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                return db.DropCollection(collectionName);
            }
        }

        public void DeleteRecord<T>(string collectionName, Guid id)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var col = db.GetCollection<T>(collectionName);
                col.Delete(id);
            }
        }

        public void DeleteRecord<T>(string collectionName, int id)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var col = db.GetCollection<T>(collectionName);
                col.Delete(id);
            }
        }

        public bool InsertDocument<T>(string collectionName, T document)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var col = db.GetCollection<T>(collectionName);
                if (col.Insert(document) != null)
                    return true;
                return false;
            }
        }

        public bool InsertMany<T>(string collectionName, List<T> collection, bool deleteCurrentCollection = true)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var col = db.GetCollection<T>(collectionName);
                int res = col.InsertBulk(collection);
                if (res == collection.Count)
                    return true;
                return false;
            }
        }

        public List<T> LoadDocuments<T>(string collectionName)
        {
            try
            {
                using (var db = new LiteDatabase(DatabaseName))
                {
                    var col = db.GetCollection<T>(collectionName);
                    var item = col.FindAll().ToList();
                    return col.FindAll().ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool UpsertDocument<T>(string collectionName, T document)
        {
            using (var db = new LiteDatabase(DatabaseName))
            {
                var col = db.GetCollection<T>(collectionName);
                return col.Update(document);
            }
        }

        public bool IsCollectionExists(string collectionName)
        {
            throw new NotImplementedException();
        }
    }
}
