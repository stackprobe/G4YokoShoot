using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte
{
	public class ResourcePicture
	{
		public DDPicture Player = DDPictureLoaders.Standard(@"Game\Player.png");
		public DDPicture Weapon0001 = DDPictureLoaders.Standard(@"Game\Weapon0001.png");

		public DDPicture Enemy0001 = DDPictureLoaders.Standard(@"Game\Enemy0001.png");
		public DDPicture Enemy0002 = DDPictureLoaders.Standard(@"Game\Enemy0002.png");
		public DDPicture Tama0001 = DDPictureLoaders.Standard(@"Game\Tama0001.png");
		public DDPicture Boss0001 = DDPictureLoaders.Standard(@"Game\Boss0001.png");
		public DDPicture Boss0002 = DDPictureLoaders.Standard(@"Game\Boss0002.png");
		public DDPicture Boss0003 = DDPictureLoaders.Standard(@"Game\Boss0003.png");

		public DDPicture Wall0001 = DDPictureLoaders.Standard(@"Wall\Wall0001.png");
		public DDPicture Wall0002 = DDPictureLoaders.Standard(@"Wall\Wall0002.png");
		public DDPicture Wall0003 = DDPictureLoaders.Standard(@"Wall\Wall0003.png");
	}
}
