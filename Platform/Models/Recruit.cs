using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class Recruit
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string FullName { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string MaritalStatus { get; set; }

		public RecruitmentOffice RecruitmentOffice { get; set; }

		public TroopType TroopType { get; set; }

		public ICollection<MedicalComissionResults> MedicalComissionResults { get; set; }

		[Required]
		public string PathToPhoto { get; set; }

		[Required]
		public DateTime RecruitmentDate { get; set; }
	}
}
