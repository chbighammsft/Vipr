using System;
using System.Collections.Generic;
using System.Linq;
using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    internal class DocSet
    {
        private OdcmModel model;

        public IEnumerable<Namespace> Namespaces {get; private set;}

        public DocSet(OdcmModel model)
        {
            this.model = model;

            Namespaces = model.Namespaces.Where(n => !n.Name.Equals("edm", StringComparison.OrdinalIgnoreCase))
                .Select(n => new Namespace(n, model));
        }
    }
}