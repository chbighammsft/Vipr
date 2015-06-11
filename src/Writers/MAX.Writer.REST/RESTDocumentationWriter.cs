using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    public class RESTDocumentationWriter : IOdcmWriter, IConfigurable
    {
        public RESTDocumentationWriter()
        {

        }

        public IEnumerable<TextFile> GenerateProxy(OdcmModel model)
        {
            var docSet = new DocSet(model);
            var docGenerator = new DocumentationGenerator(docSet);

            return docGenerator.Generate();
        }

        public void SetConfigurationProvider(IConfigurationProvider configurationProvider)
        {
            
        }
    }
}
