using form_task_arda.Data;
using form_task_arda.Models;
using System.ComponentModel.DataAnnotations;

namespace form_task_arda.DTOs
{
    
    // Buradaki RegEx sayesinde "@" işaretine izin vermez, ayrıca boş string olamaz, çizgiler ve nokta kullanılabilir //
    public class RegisterFormDto
    {
        [Required(ErrorMessage = "adınızı boş bırakamazsınız")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Bir Kullanıcı adı girmelisiniz")]
        [StringLength(12, MinimumLength = 4, ErrorMessage = "Kullanıcı adınız 4-12 hane arasında olmalı")]
        [RegularExpression(@"^[a-zA-Z0-9_.-]+$")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email zorunludur")]
        [EmailAddress(ErrorMessage = "Lütfen geçerli bir email girin")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
