using System;
using System.Collections.Generic;
using System.Text;

namespace TestBackendUser.Domain.Response
{
    public class UserResponse
    {
        public UserResponse(bool success ,dynamic data,List<string> errors)
        {
            Success = success;
            Data = data;
            Errors = errors;
        }
        public bool Success { get; set; }
        public dynamic Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
