using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    internal class RootType
    {
        private OdcmProperty entity;

        public string Name { get { return entity.Name; } }
        public string TypeName { get { return entity.Type.Name; } }

        public RootType(OdcmProperty entity)
        {
            this.entity = entity;
        }
    }
}