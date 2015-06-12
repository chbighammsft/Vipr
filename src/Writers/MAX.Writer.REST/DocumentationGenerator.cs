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
                Write(rootType);
            }

            foreach (var resourceNamespace in docSet.Namespaces)
            {
                Write(resourceNamespace);
            }

            FileList.Add(new TextFile("docSet.md", sb.ToString()));

            return FileList;
        }

        private void Write(Namespace resourceNamespace)
        {
            Write(resourceNamespace.Enums);
            Write(resourceNamespace.Classes);
        }

        private void Write(IEnumerable<OdcmClass> classes)
        {
            if (classes.Count() > 0)
            {
                sb.AppendLine(GetBookmark("classes"));
                sb.AppendLine("## Classes ##");
                sb.AppendLine();

                foreach (var resourceClass in classes.Where(c =>c.Kind==OdcmClassKind.Complex))
                {
                    if (!docSet.RootTypes.ContainsKey(resourceClass.Name))
                    {
                        Write(resourceClass);
                        sb.AppendLine();
                    }
                }
            }
        }

        private void Write(OdcmClass resourceClass)
        {
            sb.AppendLine(GetBookmark(resourceClass.Name));

            sb.AppendFormat("## {0} class ##", resourceClass.Name);
            sb.AppendLine();

            var structuralProperties = resourceClass.StructuralProperties();
            if (structuralProperties.Count() > 0)
            {
                sb.AppendLine("## Direct properties ##");
                sb.AppendLine("| Name | Type | Description |");
                sb.AppendLine("|----- |----- |----- |");
                foreach (var directProperty in structuralProperties)
                {
                    sb.AppendFormat("|{0} | {1} | {2} |", directProperty.Name, GetPropertyType(directProperty.Type), directProperty.Description);
                    sb.AppendLine();
                }
            }
        }

        private object GetPropertyType(OdcmType type)
        {
            return type.Name;
        }

        private void Write(IEnumerable<OdcmEnum> enums)
        {
            if (enums.Count() > 0)
            {
                sb.AppendLine(GetBookmark("enumerations"));
                sb.AppendLine("### Enumerations ###");
                sb.AppendLine();

                foreach (var enumeration in enums)
                {
                    Write(enumeration);
                    sb.AppendLine();
                }
            }
        }

        private void Write(OdcmEnum enumeration)
        {
            sb.AppendLine(GetBookmark(enumeration.Name));
            sb.AppendFormat("### {0} enumeration ###", enumeration.Name);
            sb.AppendLine();
            sb.AppendLine("| Value | Name |");
            sb.AppendLine("|----- |----- |");
            foreach (var enumMember in enumeration.Members)
            {
                Write(enumMember);
            }
        }

        private void Write(OdcmEnumMember enumMember)
        {
            sb.AppendFormat("| {0} | {1} |", enumMember.Value, enumMember.Name);
            sb.AppendLine();
        }

        private void Write(RootType rootType)
        {
            sb.AppendLine(GetBookmark(rootType.TypeName));
            sb.AppendFormat("# {0} #", rootType.TypeName);
            sb.AppendLine();

            var entities = docSet.Entities.Where(e => e.TypeName == rootType.TypeName);
            foreach (var entity in entities.OrderBy(e => e.IsCollection))
            {
                Write(entity);
            }
            sb.AppendLine();
        }

        private string GetBookmark(string bookmarkName)
        {
            return string.Format("<a name=\"bk_{0}\"></a>", bookmarkName);
        }

        private void Write(Entity entity)
        {
            if (entity.IsCollection)
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "/[identifier]", entity.Name);
                sb.AppendLine();
                sb.AppendLine("or");
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat + "('[identifier]')", entity.Name);
                sb.AppendLine();
            }
            else
            {
                sb.AppendFormat(ConfigurationService.Settings.ServiceURLFormat, entity.Name);
                sb.AppendLine();
                sb.AppendLine("or");
            }

            var directProperties = entity.Properties.Where(p => p.IsDirect).OrderBy(p => p.Name);
            if (directProperties.Count() > 0)
            {
                sb.AppendLine("## Direct properties ##");
                sb.AppendLine("| Name | Type | Description |");
                sb.AppendLine("|----- |----- |----- |");
                foreach (var property in directProperties)
                {
                    Write(property);
                }
            }
            var navigationProperties = entity.Properties.Where(p => p.IsNavigation).OrderBy(p => p.Name);
            if (navigationProperties.Count() > 0)
            {
                sb.AppendLine("## Navigation properties ##");
                sb.AppendLine("| Name | Type | Description |");
                sb.AppendLine("|----- |----- |----- |");
                foreach (var property in navigationProperties)
                {
                    Write(property);
                }
            }
        }

        private void Write(Property property)
        {
            sb.AppendFormat("| {0} | {1} | {2} |", property.Name, GetPropertyType(property), property.Description);
            sb.AppendLine();
        }

        private string GetPropertyType(Property property)
        {
            if (property.IsComplex)
            {
                return string.Format("[{0}](bk_{0})", property.Type);
            }
            else
            {
                return property.Type;
            }
        }
    }
}