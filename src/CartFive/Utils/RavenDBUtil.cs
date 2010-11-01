using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Client;
using Raven.Client.Document;

namespace CartFive.Utils
{
    public static class RavenDBUtil
    {
        public static IDocumentSession GetRavenDBConnection()
        {            
            DocumentStore documentStore = new DocumentStore();
            documentStore.ConnectionStringName="RavenDB";
            documentStore.Initialize();
            return documentStore.OpenSession();
        }

        public static DocumentSession GetDocumentSession()
        {
            
            DocumentStore documentStore = new DocumentStore();
            documentStore.ConnectionStringName = "RavenDB";
            documentStore.Initialize();

            DocumentSession session = new DocumentSession(documentStore, null, null);
            return session;
        }
    }
}