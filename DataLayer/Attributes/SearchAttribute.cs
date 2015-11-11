using System;

namespace DataLayer.Attributes
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = true)]
    public class SearchAttribute:Attribute
    {
        public String Name { get; set; }
        public SearchAttribute()
        {
            
        }
        public SearchAttribute(string name)
        {
            Name = name;
        }
    }
}
