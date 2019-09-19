using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public class WallDark : IWall
	{
		public void Draw()
		{
			DDCurtain.DrawCurtain();
		}
	}
}
