using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common;

namespace Charlotte.Games.Walls
{
	public class Wall0003 : IWall
	{
		private static IEnumerable<object> GetSequencer()
		{
			for (; ; )
			{
				{
					int slide = (int)((Game.I.Frame * 7L) % 180L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 180)
					{
						for (int dy = 0; dy < DDConsts.Screen_H; dy += 180)
						{
							DDDraw.DrawSimple(Ground.I.Picture.Wall0001, dx, dy);
						}
					}
				}

				{
					int slide = (int)((Game.I.Frame * 17L) % 90L);

					for (int dx = -slide; dx < DDConsts.Screen_W; dx += 90)
					{
						for (int dy = 0; dy < DDConsts.Screen_H; dy += 90)
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
