using Domain.Models;

namespace Getri_FinalProject_MVC_API.ViewModel
{
	public class ProductCreateViewModel
	{
		public int ProductId { get; set; }

		public string? ProductName { get; set; }

		public string? ProductDescription { get; set; }

		public int? ProductPrice { get; set; }

		public int? CategoryId { get; set; }

		public string CategoryName { get; set; }
	}
}
