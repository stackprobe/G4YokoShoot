﻿using System;
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
	}
}
