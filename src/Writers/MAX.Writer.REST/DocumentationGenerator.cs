using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vipr.Core;
using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    internal class DocumentationGenerator
    {
        private DocSet docSet;
        private List<TextFile> FileList;
        private StringBuilder sb;

        public DocumentationGenerator(DocSet docSet)
        {
            this.docSet = docSet;
        }

        internal IEnumerable<TextFile> Generate()
        {
            FileList = new List<TextFile>();

            sb = new StringBuilder();

            foreach (var rootType in docSet.RootTypes.Values)
            {
                sb.AppendFormat("# {0} #", rootType.Name);
                sb.AppendLine();

                var entities = docSet.Entities.Where(e => e.TypeName == rootType.TypeName);
                foreach (var entity in entities.OrderBy(e=>e.IsCollection))
                {
                    Write(entity);
                }
                sb.AppendLine();
            }
            
            FileList.Add(new TextFile("docSet.md", sb.ToString()));

            return FileList;
        }

        private void Write(Entity entity)
        {
            if (entity.IsCollection)
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "/<identifier>", entity.Name);
                sb.AppendLine();
                sb.AppendLine("or");
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "('<identifier>')", entity.Name);
                sb.AppendLine();
            }
            else
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat, entity.Name);
                sb.AppendLine();
                sb.AppendLine("or");
            }
        }
    }
}