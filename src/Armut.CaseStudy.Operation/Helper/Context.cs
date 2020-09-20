

using System;
using Armut.CaseStudy.Model;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Armut.CaseStudy.Operation.Helper
{
    public class Context : IContext
    {
        private readonly IDatabaseSettings settings;
        private readonly ILogger<Context> logger;

        public Context(IDatabaseSettings settings, ILogger<Context> logger)
        {
            this.settings = settings;
            this.logger = logger;
        }

        public IMongoCollection<SingupModel> UserContext()
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                return database.GetCollection<SingupModel>(settings.UsersCollectionName);
            }
            catch (Exception err)
            {
                logger.LogError(err, "Context-UserContext throw error");
                throw;
            }
        }


        public IMongoCollection<User> UserInfoContext()
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                return database.GetCollection<User>(settings.UserInfoCollectionName);
            }
            catch (Exception err)
            {
                logger.LogError(err, "Context-UserInfoContext throw error");
                throw;
            }
        }

        public IMongoCollection<SendMessage> MessageContext()
        {
            try
            {
                var client = new MongoClient(settings.ConnectionString);
                var database = client.GetDatabase(settings.DatabaseName);
                return database.GetCollection<SendMessage>(settings.MessageCollectionName);
            }
            catch (Exception err)
            {
                logger.LogError(err, "Context-MessageContext throw error");
                throw;
            }
        }
    }

    public interface IContext
    {
        IMongoCollection<SingupModel> UserContext();
        IMongoCollection<User> UserInfoContext();
        IMongoCollection<SendMessage> MessageContext();

    }
}
