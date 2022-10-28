using System;
using System.Collections.Generic;
using System.Text;

namespace General.Core.Entities.Abstract
{
    interface IResponseService
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
