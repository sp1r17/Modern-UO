using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Engines.Quests.Samurai
{
	public class HaochisKatana : QuestItem
	{
		public override int LabelNumber => 1063165; // Daimyo Haochi's Katana

		[Constructible]
		public HaochisKatana() : base( 0x13FF )
		{
			Weight = 1.0;
		}

		public HaochisKatana( Serial serial ) : base( serial )
		{
		}

		public override bool CanDrop( PlayerMobile player )
		{
			return !(player.Quest is HaochisTrialsQuest );

			//return !qs.IsObjectiveInProgress( typeof( FifthTrialReturnObjective ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
