using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class Schedule
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public DayOfWeek DayOfWeek { get; set; }

		[Required]
		public string StartDate { get; set; }

		[Required]
		public string EndDate { get; set; }

		public long RecruitmentOfficeId { get; set; }

		public virtual RecruitmentOffice RecruitmentOffice { get; set; }
	}
}
