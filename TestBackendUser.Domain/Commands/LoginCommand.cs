using System;
using System.Collections.Generic;
using System.Text;

namespace TestBackendUser.Domain.Commands
{
    public class LoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
