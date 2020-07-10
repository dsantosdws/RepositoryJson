using RepositoryJson.BaseRepository.BaseEntitys;
using RepositoryJson.BaseRepository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RepositoryJson.BaseRepository.Repositorys
{
    public abstract class RepositoryJson<TEntity> : 
        IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private string NameFile { get; }

        public string FullPathFile
        {
            get
            {
                return @"Database/" + NameFile;
            }
        }

        public RepositoryJson(string nameFile)
        {
            NameFile = nameFile;
        }

        public async Task Save(TEntity model)
        {
            var type = typeof(TEntity);

            var listModels = new List<TEntity>();
            
            if (!File.Exists(FullPathFile))
            {
                listModels.Add(model);
            }
            else
            {
                listModels = await DeserializeObject();

                var index = listModels.FindIndex(f => f.Id.Equals(model.Id));
                
                if (index < 0)
                    listModels.Add(model);                
                else
                    listModels[index] = model;                
            }
            await SaveFile(listModels);
        }
       
        private async Task SaveFile(object obj)
        {
            var jsonResult = SerializeObject(obj);
            await File.WriteAllTextAsync(FullPathFile, jsonResult);
        }

        private string SerializeObject(object obj)
        {            
            return JsonConvert.SerializeObject(obj);
        }

        protected async Task<List<TEntity>> DeserializeObject()
        {
            var fileResult = await ReadAllFile();

            if (string.IsNullOrEmpty(fileResult))
            {
                return new List<TEntity>();
            }

            return await Task.FromResult(JsonConvert.DeserializeObject<List<TEntity>>(fileResult));
        }

        private async Task<string> ReadAllFile()
        {
            if (File.Exists(FullPathFile))
            {
                return await File.ReadAllTextAsync(FullPathFile);
            }

            return string.Empty;
        }

        public async Task Delete(TEntity model)
        {
            var type = typeof(TEntity);

            var listModels = await DeserializeObject();

            var objctToDelete = listModels.Find(f => f.Id.Equals(model.Id));

            listModels.Remove(objctToDelete);

            await SaveFile(listModels);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await DeserializeObject();
        }

        public async Task<TEntity> GetBy(Guid Id)
        {
            var result = await DeserializeObject();

            return result.Find(f => f.Id.Equals(Id));
        }

        public async Task<List<TEntity>> Search(Func<TEntity, bool> predicate)
        {
            var result = await DeserializeObject();

            return result
                .Where(predicate)
                .ToList();
        }

        public async Task<TEntity> SearchFirstOrDefault(Func<TEntity, bool> predicate)
        {
            var result = await DeserializeObject();

            return result
                .Where(predicate)
                .FirstOrDefault();
        }
    }
}