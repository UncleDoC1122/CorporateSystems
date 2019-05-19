using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class TroopType
	{
		[Key]
		public long Id { get; set; }

		[Required]
		public string Name { get; set; }

		public TroopKind TroopKind { get; set; }

		public long TroopKindId { get; set; }
	}
}
