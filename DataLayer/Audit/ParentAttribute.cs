using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    
    /// <summary>
    /// This class allows to define dependency between entities when auditing actions.
    /// A property marked with this attribute defines that the type of the property represents a parent entity of the type containing the property in auditing actions.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ParentAttribute : Attribute
    {
        public ParentAttribute()
            : base()
        {

        }

        public ParentAttribute(string relationName)
            : base()
        {
            _relationName = relationName;
        }

        private string _relationName;

        public string RelationName
        {
            get { return _relationName; }
            private set { _relationName = value; }
        }
    }
}
