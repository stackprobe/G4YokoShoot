using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public static class IWeapons
	{
		public static IWeapon Load(IWeapon weapon, double x, double y)
		{
			return Load(weapon, new D2Point(x, y));
		}

		public static IWeapon Load(IWeapon weapon, D2Point pt)
		{
			weapon.Loaded(pt);
			return weapon;
		}
	}
}
