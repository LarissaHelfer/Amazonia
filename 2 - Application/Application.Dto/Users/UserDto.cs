using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace API.Application.Dto
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }
        public List<RoleDto> Roles { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastLogin { get; set; }
        public string Foto { get; set; }
        public string Nome { get; set; }
    }
}
