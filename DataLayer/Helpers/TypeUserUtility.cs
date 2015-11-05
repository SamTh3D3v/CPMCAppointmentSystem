using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public static class TypeUserUtility
    {
        public static bool IsAgent(TypeUser typeUser)
        {
            return TypeUser.Agent == (TypeUser.Agent & typeUser);
        }
        public static bool IsMedecin(TypeUser typeUser)
        {
            return TypeUser.Medecin == (TypeUser.Medecin & typeUser);
        }
        public static bool IsAdmin(TypeUser typeUser)
        {
            return TypeUser.Admin == (TypeUser.Admin & typeUser);
        }
        public static TypeUser WhichTypeUser(bool isAdmin, bool isMedecin, bool isAgent)
        {
            TypeUser typeUser = TypeUser.None;

            if (isAdmin)
                typeUser = TypeUser.Admin | typeUser;

            if (isMedecin)
                typeUser = TypeUser.Medecin | typeUser;

            if (isAgent)
                typeUser = TypeUser.Agent | typeUser;

            return typeUser;
         }

    }
}
