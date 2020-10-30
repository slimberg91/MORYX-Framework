// Copyright (c) 2020, Phoenix Contact GmbH & Co. KG
// Licensed under the Apache License, Version 2.0

using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using Moryx.Model.Configuration;
using Moryx.Tools.Wcf;
using Moryx.Web;

namespace Moryx.Runtime.Maintenance.Plugins.Databases
{
    /// <summary>
    /// Service contracts for database operations.
    /// </summary>
    [ServiceContract]
    [ServiceVersion("3.0.0.0")]
    internal interface IDatabaseMaintenance
    {
        /// <summary>
        /// Get all database configs
        /// </summary>
        /// <returns>The fetched DataModels.</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "/", Method = WebRequestMethods.Http.Get,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        DataModel[] GetAll();

        /// <summary>
        /// Drop all data models
        /// </summary>
        [OperationContract]
        [WebInvoke(UriTemplate = "/", Method = WebRequestMethodsExtension.Http.Delete,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse EraseAll();

        /// <summary>
        /// Get all database config
        /// </summary>
        /// <returns>The fetched DataModel.</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}", Method = WebRequestMethods.Http.Get,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        DataModel GetModel(string targetModel);

        /// <summary>
        /// Set database config
        /// </summary>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/config", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        void SetDatabaseConfig(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Test a new config
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/config/test", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        TestConnectionResponse TestDatabaseConfig(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Create all datamodels with current config
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "createall", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse CreateAll();

        /// <summary>
        /// Create a new database matching the model
        /// </summary>
        /// <returns>True if database could be created</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/create", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse CreateDatabase(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Erases the database given by the model
        /// </summary>
        /// <returns>True if erased successful</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}", Method = WebRequestMethodsExtension.Http.Delete,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse EraseDatabase(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Dumps the database matching the model to create a restoreable backup
        /// </summary>
        /// <returns>True if async dump is in progress</returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/dump", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse DumpDatabase(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Restores the database.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/restore", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse RestoreDatabase(string targetModel, RestoreDatabaseRequest request);

        /// <summary>
        /// Updates database model to the specified update.
        /// </summary>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/{migrationName}/migrate", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        DatabaseUpdateSummary MigrateDatabaseModel(string targetModel, string migrationName, DatabaseConfigModel config);

        /// <summary>
        /// Rollback of all migrations made
        /// </summary>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/rollback", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse RollbackDatabase(string targetModel, DatabaseConfigModel config);

        /// <summary>
        /// Execute setup for this config
        /// </summary>
        [OperationContract]
        [WebInvoke(UriTemplate = "model/{targetModel}/setup", Method = WebRequestMethods.Http.Post,
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json)]
        InvocationResponse ExecuteSetup(string targetModel, ExecuteSetupRequest request);
    }
}
