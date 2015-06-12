using System.Collections.Generic;
using Vipr.Core.CodeModel;

namespace MAX.Writer.REST
{
    internal class Entity
    {
        private OdcmProperty entity;
        private OdcmEntityClass entityClass;

        public bool IsCollection { get { return entity.IsCollection; } }
        public string Name { get { return entity.Name; } }
        public List<Property> Properties { get; private set; }
        public string TypeName { get { return entity.Type.Name; } }

        public Entity(OdcmProperty entity)
        {
            this.entity = entity;
            this.entityClass = (OdcmEntityClass)entity.Type;

            Properties = new List<Property>();
            foreach (var property in entityClass.Properties)
            {
                Properties.Add(new Property(property));
            }
        }
    }
}