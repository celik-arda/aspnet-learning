using form_task_arda.Data;
using form_task_arda.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Identity;

namespace form_task_arda.Models
{
    public class AppUser : IdentityUser
    {
        // kendi ekstradan ekleyeceğim özellik

        public string? FullName { get; set; }
    }
}
