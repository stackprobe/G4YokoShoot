using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Common.Options;

namespace Charlotte.Games
{
	public interface IEnemy
	{
		void Loaded(D2Point pt);
		bool EachFrame(); // ? 生存
		DDCrash GetCrash();
		bool Crashed(IWeapon weapon); // ? 生存
		IEnemies.Kind_e GetKind();
		void Draw();
	}
}
