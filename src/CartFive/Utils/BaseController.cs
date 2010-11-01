using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client.Document;
using Raven.Client;

namespace CartFive.Utils
{
    public class BaseController : Controller
    {
        private IDocumentSession _DBSession;

        public IDocumentSession DBSession
        {
            get
            {
                return _DBSession;
            }
        }

        public BaseController():base()
        {
            DocumentStore documentStore = new DocumentStore();
            documentStore.ConnectionStringName="RavenDB";
            documentStore.Conventions.FindTypeTagName = t => t.Name;
            documentStore.Initialize();
            _DBSession = documentStore.OpenSession();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _DBSession.Dispose();
            }
                base.Dispose(disposing);
        }
    }
}