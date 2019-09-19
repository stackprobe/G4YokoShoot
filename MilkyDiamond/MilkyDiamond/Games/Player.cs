using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Games.Weapons;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class Player
	{
		public double X;
		public double Y;

		public int BornFrame;
		public int DeadFrame;

		private double Born_X;
		private double Born_Y;

		public void Draw()
		{
			if (1 <= this.BornFrame)
			{
				if (this.BornFrame == 1)
				{
					this.Born_X = -50.0;
					this.Born_Y = DDConsts.Screen_H / 2.0;
				}
				DDUtils.Approach(ref this.Born_X, this.X, 0.9);
				DDUtils.Approach(ref this.Born_Y, this.Y, 0.9);

				DDDraw.SetAlpha(0.5);
				DDDraw.DrawCenter(Ground.I.Picture.Player, this.Born_X, this.Born_Y);
				DDDraw.Reset();

				return;
			}
			if (1 <= this.DeadFrame)
			{
				DDDraw.SetAlpha(0.5);
				DDDraw.DrawZoom(1.0 + this.DeadFrame / 10.0);
				DDDraw.Reset();

				return;
			}
			DDDraw.DrawCenter(Ground.I.Picture.Player, this.X, this.Y);
		}

		public void Fire()
		{
			if (Game.I.Frame % 6 == 0)
			{
				Game.I.AddWeapon(IWeapons.Load(
					new Weapon0001(),
					this.X + 38.0,
					this.Y
					));
			}
		}
	}
}
