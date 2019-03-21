using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterRestService.Models
{
    public class Register
    {
        [EmailAddress]
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public bool IsAdmin { get; set; }
    }
}
