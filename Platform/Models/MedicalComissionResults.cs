using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class MedicalComissionResults
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public Suitability Suitability { get; set; }

		[Required]
		public DateTime ComissionDate { get; set; }

		public Recruit Recruit { get; set; }

		public long RecruitId { get; set; }
	}

	public enum Suitability : byte
	{
		FullySuitable,
		AlternativeSuitable,
		RestrictedlySuitable,
		NotSuitable
	}
}
