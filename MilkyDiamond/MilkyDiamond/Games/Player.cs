using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Games.Weapons;
using Charlotte.Tools;
using Charlotte.Game3Common;

namespace Charlotte.Games
{
	public class Player
	{
		public const int SPEED_LEVEL_MIN = 1;
		public const int SPEED_LEVEL_DEF = 3;
		public const int SPEED_LEVEL_MAX = 5;

		public double X;
		public double Y;
		public int SpeedLevel = SPEED_LEVEL_DEF;

		public SceneKeeper BornScene = new SceneKeeper(20);
		public SceneKeeper DeadScene = new SceneKeeper(20);
		public SceneKeeper MutekiScene = new SceneKeeper(60);

		private double Born_X;
		private double Born_Y;

		public void Draw()
		{
			if (this.DeadScene.IsFlaming())
			{
				DDScene scene = this.DeadScene.GetScene();

				DDDraw.SetAlpha(0.5);
				DDDraw.DrawBegin(Ground.I.Picture.Player, this.X, this.Y);
				DDDraw.DrawRotate(scene.Rate * 5.0);
				DDDraw.DrawZoom(1.0 + scene.Rate * 5.0);
				DDDraw.DrawEnd();
				DDDraw.Reset();

				return;
			}
			if (this.BornScene.IsFlaming())
			{
				if (this.BornScene.IsJustFired())
				{
					this.Born_X = -50.0;
					this.Born_Y = DDConsts.Screen_H / 2.0;
				}
				DDScene scene = this.BornScene.GetScene();

				DDUtils.Approach(ref this.Born_X, this.X, 0.9 - 0.3 * scene.Rate);
				DDUtils.Approach(ref this.Born_Y, this.Y, 0.9 - 0.3 * scene.Rate);

				DDDraw.SetAlpha(0.5);
				DDDraw.DrawCenter(Ground.I.Picture.Player, this.Born_X, this.Born_Y);
				DDDraw.Reset();

				return;
			}
			if (this.MutekiScene.IsFlaming())
			{
				DDDraw.SetAlpha(0.5);
				DDDraw.DrawCenter(Ground.I.Picture.Player, this.X, this.Y);
				DDDraw.Reset();

				return;
			}
			DDDraw.DrawCenter(Ground.I.Picture.Player, this.X, this.Y);
		}

		public void Shoot()
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
