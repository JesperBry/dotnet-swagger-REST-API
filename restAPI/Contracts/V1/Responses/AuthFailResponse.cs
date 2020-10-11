using System.Collections.Generic;

namespace restAPI.Contracts.V1.Responses
{
    public class AuthFailResponse
    {
        public IEnumerable<string> Errors { get; set; }
    }
}
