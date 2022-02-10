using DAL.Models.ResponceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ARINLAB.Services
{
    public static class ResponceGenerator
    {
        public static Responce GetResponceModel(bool isSuccess, string error = "", object data = null)
        {
            return new Responce(isSuccess, error, data);
        }
    }
}
