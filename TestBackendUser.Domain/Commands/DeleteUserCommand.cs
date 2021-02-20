using System;
using System.Collections.Generic;
using System.Text;

namespace TestBackendUser.Domain.Commands
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
    }
}
