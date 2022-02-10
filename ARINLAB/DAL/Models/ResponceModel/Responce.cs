using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models.ResponceModel
{
    public class Responce
    {
        public Responce()
        {
            IsSuccess = false;
            ErrorMessage = "";
            Data = null;
        }

        public Responce(bool isSuccess, string error, object data)
        {
            IsSuccess    = isSuccess;
            ErrorMessage = error;
            Data         = data; 
        }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public object Data { get; set; }
    }
}
