using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Platform.Models
{
	public class TroopKind
	{
		[Key]
		public long Id { get; set; }
		
		[Required]
		public string Name { get; set; }

		public ICollection<TroopType> TroopTypes { get; set; }
	}
}
