using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Backend.Models
{
    #region: Request
    public class GetDetailRequest
    {
        public Guid Id { get; set; }
    }

    public class GetDetailByIntRequest
    {
        public int Id { get; set; }
    }
    #endregion

    #region: Response
    public class NonResponseData
    {

    }
    #endregion
}
