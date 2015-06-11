using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    internal class Entity
    {
        private OdcmProperty entity;

        public bool IsCollection { get { return entity.IsCollection; } }
        public string Name { get { return entity.Name; } }
        public string TypeName { get { return entity.Type.Name; } }

        public Entity(OdcmProperty entity)
        {
            this.entity = entity;
        }
    }
}