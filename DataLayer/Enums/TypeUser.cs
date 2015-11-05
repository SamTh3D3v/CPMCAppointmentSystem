using System;

namespace DataLayer.Model
{
    [Flags]
    public enum TypeUser
    {
        None=0x0,
        Agent=0x1,
        Medecin =0x2,
        Admin=0x4,
    }
}