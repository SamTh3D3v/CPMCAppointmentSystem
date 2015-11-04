using System;

namespace DataLayer.Model
{
    [Flags]
    public enum TypeUser
    {
        Agent=0x1,
        Medecin =0x2,
        Admin=0x4,
    }
}