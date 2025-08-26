using form_task_arda.Data;
using form_task_arda.Models;

namespace form_task_arda.DTOs
{
    public class RegisterFormDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
