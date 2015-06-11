using System;
using System.Collections.Generic;
using System.Text;
using Vipr.Core;

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

            foreach(var restNamespace in docSet.Namespaces)
            {
                Write(restNamespace);
            }

            FileList.Add(new TextFile("C:\\Users\\chbigham\\Downloads\\TestOutput", sb.ToString()));

            return FileList;
        }

        private void Write(Namespace restNamespace)
        {
            
        }
    }
}