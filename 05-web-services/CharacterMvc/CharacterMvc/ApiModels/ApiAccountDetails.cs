using System.Collections.Generic;

namespace CharacterMvc.ApiModels
{
    public class ApiAccountDetails
    {
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
