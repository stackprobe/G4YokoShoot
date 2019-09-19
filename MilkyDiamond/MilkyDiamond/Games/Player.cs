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
				IWeapon weapon = new Weapon0001();
				weapon.Loaded(new D2Point(this.X + 20.0, this.Y));
				Game.I.AddWeapon(weapon);
			}
		}
	}
}
