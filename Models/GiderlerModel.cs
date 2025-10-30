using form_task_arda.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;




namespace form_task_arda.Models
{
	[Table("Gider_Table")]
	public class GiderlerModel
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Gider_Id {  get; set; }
		[Required]
		public string Gider_Adi { get; set; }
		[Required]
		public string Gider_Kategorisi { get; set; }
		[Required]
		public int Gider_Maliyeti { get; set; }
		[Required]
		public string Gider_Tedarikcisi { get; set; } = "Belirtilmedi";
		public DateTime? Gider_Tarihi {  get; set; }

	}
}
