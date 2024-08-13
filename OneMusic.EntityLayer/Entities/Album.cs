using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneMusic.EntityLayer.Entities
{
	public class Album
	{
		public int AlbumID { get; set; }
		public string AlbumName { get; set; }
		public string CoverImage { get; set; }
		public decimal Price { get; set; }

		public int? AppUserID { get; set; }
		public AppUser AppUser { get; set; }
		public List<Song> Songs { get; set; }
        public int? CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
