using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ParametreNotFoundException : Exception
    {
        public ParametreNotFoundException(string message)
            : base(message)
        {

        }

        public ParametreNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
    public class ParameterManager
    {
        public static T GetValue<T>(ParameterNames parameterName)
        {
            Parameter parameter = null;

            T result = default(T);
            
            using (var context = new CpmcContext())
            {
                parameter = context.Parameters.SingleOrDefault(p => p.Name == parameterName.ToString());

                if(parameter==null)
                    throw new ParametreNotFoundException(string.Format("Le paramètre {0} est introuvable.", parameterName.ToString()));                            

                result = (T)Convert.ChangeType(parameter.Value, typeof(T));
            }

            return result;
        }

        public static void SetValue(ParameterNames parameterName, object value)
        {
            using (var context = new CpmcContext())
            {
                var parameter = context.Parameters.SingleOrDefault(p => p.Name == parameterName.ToString());

                if (parameter == null)
                    throw new ParametreNotFoundException(string.Format("Le paramètre {0} est introuvable.", parameterName.ToString()));

                parameter.Value = value.ToString();

                context.SaveChanges();
            }
        }
    }
}
