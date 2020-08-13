using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Tools;

namespace Charlotte.Games.Walls
{
	public class Wall0004 : IWall
	{
		private static IEnumerable<object> GetSequencer()
		{
			for (; ; )
			{
				{
					int slide = (int)((Game.I.Frame * 3L) % 108L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 108)
					{
						for (int dy = 0; dy < DDConsts.Screen_H; dy += 108)
						{
							DDDraw.DrawSimple(Ground.I.Picture.Wall0002, dx, dy);
						}
					}
				}

				{
					int slide = (int)((Game.I.Frame * 13L) % 90L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 90)
					{
						for (int dy = -45; dy < DDConsts.Screen_H; dy += 90)
						{
							DDDraw.DrawSimple(Ground.I.Picture.Wall0003, dx, dy);
						}
					}
				}

				yield return null;
			}
		}

		private Func<object> Sequencer = EnumerableTools.Supplier(GetSequencer());

		public void Draw()
		{
			this.Sequencer();
		}
	}
}
