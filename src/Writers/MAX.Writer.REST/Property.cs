using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    public class Property
    {
        private OdcmProperty property;

        public string Description { get { return property.Description; } }
        public bool IsComplex { get { return !property.Type.FullName.Contains("Edm"); } }
        public bool IsDirect { get { return !property.IsCollection; } }
        public bool IsNavigation { get { return property.IsCollection; } }
        public string Name { get { return property.Name; } }
        public string Type { get { return property.Type.Name; } }

        public Property(OdcmProperty property)
        {
            this.property = property;
        }
    }
}