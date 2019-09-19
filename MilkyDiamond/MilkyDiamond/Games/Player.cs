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

		public void Draw()
		{
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
