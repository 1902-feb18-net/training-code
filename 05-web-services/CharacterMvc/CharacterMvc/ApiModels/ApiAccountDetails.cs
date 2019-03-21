using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterMvc.ApiModels
{
    public class ApiAccountDetails
    {
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
