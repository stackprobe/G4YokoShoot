using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Common;
using Charlotte.Game3Common;
using Charlotte.Tools;

namespace Charlotte.Games
{
	public class Game : IDisposable
	{
		public static Game I = null;

		public Game()
		{
			I = this;
		}

		public void Dispose()
		{
			I = null;
		}

		private Player Player = new Player();

		public int Frame = 0;

		public void Perform()
		{
			this.Player.X = DDConsts.Screen_W / 4;
			this.Player.Y = DDConsts.Screen_H / 2;

			for (; ; this.Frame++)
			{
				// プレイヤー行動
				{
					double xa = 0.0;
					double ya = 0.0;

					if (1 <= DDInput.DIR_4.GetInput()) // 左移動
					{
						xa = -1.0;
					}
					if (1 <= DDInput.DIR_6.GetInput()) // 左移動
					{
						xa = 1.0;
					}
					if (1 <= DDInput.DIR_8.GetInput()) // 上移動
					{
						ya = -1.0;
					}
					if (1 <= DDInput.DIR_2.GetInput()) // 下移動
					{
						ya = 1.0;
					}
					double speed = 6.0;

					if (1 <= DDInput.A.GetInput()) // 低速ボタン押下中
					{
						speed = 3.0;
					}
					this.Player.X += xa * speed;
					this.Player.Y += ya * speed;

					DDUtils.Range(ref this.Player.X, 0.0, DDConsts.Screen_W);
					DDUtils.Range(ref this.Player.Y, 0.0, DDConsts.Screen_H);

					if (1 <= DDInput.B.GetInput()) // 攻撃ボタン押下中
					{
						this.Player.Fire();
					}
				}

				//this.EnemyEachFrame(); // TODO
				this.WeaponEachFrame();

				// Crash
				{
					Crash playerCrash = CrashUtils.Point(new D2Point(this.Player.X, this.Player.Y));

					foreach (WeaponBox weapon in this.Weapons.Iterate())
						weapon.Crash = weapon.Value.GetCrash();

#if false // TODO
					foreach (EnemyBox enemy in this.Enemies.Iterate())
					{
						Crash enemyCrash = enemy.Value.GetCrash();

						foreach (WeaponBox weapon in this.Weapons.Iterate())
						{
							if (enemyCrash.IsCrashed(weapon.Crash))
							{
								if (enemy.Value.Crashed(weapon.Value) == false) // ? 消滅
									enemy.Dead = true;

								if (weapon.Value.Crashed(enemy.Value) == false) // ? 消滅
									weapon.Dead = true;
							}
						}
						this.Weapons.RemoveAll(weapon => weapon.Dead);

						if (this.Player.DeadFrame == 0 &&
							this.Player.DamageFrame == 0 &&
							this.Player.MutekiFrame == 0 && enemyCrash.IsCrashed(playerCrash))
						{
							if (enemy.Value.CrashedToPlayer() == false) // ? 消滅
								enemy.Dead = true;

							this.Player.Crashed(enemy.Value);
						}
					}
					this.Enemies.RemoveAll(enemy => enemy.Dead);
#endif
				}

				// ここから描画

				this.DrawWall();
				this.Player.Draw();
				//this.DrawEnemies(); // TODO
				this.DrawWeapons();

				DDEngine.EachFrame();
			}
		}

		private void DrawWall()
		{
			DDCurtain.DrawCurtain();
		}

		private class WeaponBox
		{
			public IWeapon Value;
			public Crash Crash;
			public bool Dead;
		}

		private DDList<WeaponBox> Weapons = new DDList<WeaponBox>();

		public void AddWeapon(IWeapon weapon)
		{
			this.Weapons.Add(new WeaponBox()
			{
				Value = weapon,
			});
		}

		private void WeaponEachFrame()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				if (weapon.Value.EachFrame() == false) // ? 消滅
				{
					weapon.Dead = true;
				}
			}
			this.Weapons.RemoveAll(weapon => weapon.Dead);
		}

		private void DrawWeapons()
		{
			foreach (WeaponBox weapon in this.Weapons.Iterate())
			{
				weapon.Value.Draw();
			}
		}
	}
}
