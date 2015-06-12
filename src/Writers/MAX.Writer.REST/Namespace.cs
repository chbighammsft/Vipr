using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    public class Namespace
    {
        private OdcmModel model;
        private OdcmNamespace n;

        public IEnumerable<OdcmEnum> Enums { get { return n.Enums; } }
        public IEnumerable<OdcmClass> Classes { get { return n.Classes; } }

        public Namespace(OdcmNamespace n, OdcmModel model)
        {
            this.n = n;
            this.model = model;
        }
    }
}
