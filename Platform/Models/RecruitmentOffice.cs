using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class RecruitmentOffice
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string ChiefFullName { get; set; }

		public virtual ICollection<Schedule> Schedule { get; set; }

		[Required]
		public string OfficeNo { get; set; }


	}
}
