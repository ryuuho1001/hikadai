using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleQuest
{
	public class Player : Character
	{

		public int Level
		{ get; private set; }

		public float Exp
		{ get; private set; }

		public float MaxHp
			{ get; private set;}
		public float AttackPoint
			{ get; private set;}
		public float DefencePoint
			{ get;private set;}

		public double Next
		{ get; private set; }

		public Player(string name, float maxHP, float attackPoint, float defencePoint,
			int level, float exp,double next)
			: base(name, maxHP, attackPoint, defencePoint,exp)
		{
			Level = level;
			this.Exp = exp;
			AttackPoint = attackPoint;
			DefencePoint = defencePoint;
			Next = next;

		}
			public void EXPCall(Enemy target)
			{
				Exp += target.GainExp;

			if (Exp >= Next)
			{
				LevelSystem();
			}
			}

		public void LevelSystem()
		{
			Logger.Log("レベルアップ\n");
			Level++;
			Logger.Log("Lv" + (Level - 1) + "->Lv" + Level);
			LevelUP();

			float Before = (float)Next;
			Exp = Exp - Before;

			Next = Math.Truncate(Before * 1, 3);

			if (Exp >= Next)
			{
				LevelSystem();
			}
		}


		public Player(PlayerSaveData data)
			:base(data.Name,data.MaxHP,data.AttackPoint,data.DefencePoint)
			{
			Level = data.Level;
			Exp = data.Exp;
			HP = data.HP;
			}

	}
}
