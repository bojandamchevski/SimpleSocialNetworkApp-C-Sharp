using Newtonsoft.Json;
using SimpleSocialNetworkApp.Domain.Interfaces;
using SimpleSocialNetworkApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleSocialNetworkApp.Domain.Database
{
    public class Database : IDatabase
    {
        private string _folderPath;
        private string _filePath;
        private int _id;
        public Database()
        {
            _id = 0;
            _folderPath = @"..\..\..\Db";
            _filePath = _folderPath + @$"\Users.json";
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                WriteData(new List<User>());
            }
        }
        public List<User> GetAll()
        {
            return GetData();
        }

        public User GetbyId(int id)
        {
            List<User> dbEntities = GetData();
            return dbEntities.FirstOrDefault(x => x.Id == id);
        }

        public int Insert(User entity)
        {
            List<User> dbEntities = GetData();
            if (dbEntities == null)
            {
                dbEntities = new List<User>();
                _id = 1;
            }
            else
            {
                _id = dbEntities.Count + 1;
            }
            entity.Id = _id;
            dbEntities.Add(entity);
            WriteData(dbEntities);
            return entity.Id;
        }

        public void RemoveById(int id)
        {
            List<User> dbEntities = GetData();
            User entityDb = dbEntities.FirstOrDefault(x => x.Id == id);
            if (entityDb == null)
            {
                Console.WriteLine("User doesn't exist.");
            }
            dbEntities.Remove(entityDb);
            WriteData(dbEntities);
        }

        public void Update(User entity)
        {
            List<User> dbEntities = GetData();
            User entityDb = dbEntities.FirstOrDefault(x => x.Id == entity.Id);
            if (entityDb == null)
            {
                Console.WriteLine("User doesn't exist.");
            }
            dbEntities[dbEntities.IndexOf(entityDb)] = entity;
            WriteData(dbEntities);
        }

        private void WriteData(List<User> dbEntities)
        {
            using (StreamWriter streamWriter = new StreamWriter(_filePath))
            {
                streamWriter.WriteLine(JsonConvert.SerializeObject(dbEntities, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    }));
            }
        }

        private List<User> GetData()
        {
            using (StreamReader streamReader = new StreamReader(_filePath))
            {
                return JsonConvert.DeserializeObject<List<User>>(streamReader.ReadToEnd());
            }
        }
    }
}
