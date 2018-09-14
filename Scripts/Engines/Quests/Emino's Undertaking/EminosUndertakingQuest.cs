using System;
using Server.Mobiles;

namespace Server.Engines.Quests.Ninja
{
	public class EminosUndertakingQuest : QuestSystem
	{
		private static Type[] m_TypeReferenceTable = new[]
			{
				typeof( Ninja.AcceptConversation ),
				typeof( Ninja.FindZoelConversation ),
				typeof( Ninja.RadarConversation ),
				typeof( Ninja.EnterCaveConversation ),
				typeof( Ninja.SneakPastGuardiansConversation ),
				typeof( Ninja.NeedToHideConversation ),
				typeof( Ninja.UseTeleporterConversation ),
				typeof( Ninja.GiveZoelNoteConversation ),
				typeof( Ninja.LostNoteConversation ),
				typeof( Ninja.GainInnInformationConversation ),
				typeof( Ninja.ReturnFromInnConversation ),
				typeof( Ninja.SearchForSwordConversation ),
				typeof( Ninja.HallwayWalkConversation ),
				typeof( Ninja.ReturnSwordConversation ),
				typeof( Ninja.SlayHenchmenConversation ),
				typeof( Ninja.ContinueSlayHenchmenConversation ),
				typeof( Ninja.GiveEminoSwordConversation ),
				typeof( Ninja.LostSwordConversation ),
				typeof( Ninja.EarnGiftsConversation ),
				typeof( Ninja.EarnLessGiftsConversation ),
				typeof( Ninja.FindEminoBeginObjective ),
				typeof( Ninja.FindZoelObjective ),
				typeof( Ninja.EnterCaveObjective ),
				typeof( Ninja.SneakPastGuardiansObjective ),
				typeof( Ninja.UseTeleporterObjective ),
				typeof( Ninja.GiveZoelNoteObjective ),
				typeof( Ninja.GainInnInformationObjective ),
				typeof( Ninja.ReturnFromInnObjective ),
				typeof( Ninja.SearchForSwordObjective ),
				typeof( Ninja.HallwayWalkObjective ),
				typeof( Ninja.ReturnSwordObjective ),
				typeof( Ninja.SlayHenchmenObjective ),
				typeof( Ninja.GiveEminoSwordObjective )
			};

		public override Type[] TypeReferenceTable => m_TypeReferenceTable;

		public override object Name => 1063173;

		public override object OfferMessage => 1063174;

		public override TimeSpan RestartDelay => TimeSpan.MaxValue;
		public override bool IsTutorial => true;

		public override int Picture => 0x15D5;

		public EminosUndertakingQuest( PlayerMobile from ) : base( from )
		{
		}

		// Serialization
		public EminosUndertakingQuest()
		{
		}

		public override void Accept()
		{
			base.Accept();

			AddConversation( new AcceptConversation() );
		}

		private bool m_SentRadarConversion;

		public override void Slice()
		{
			if ( !m_SentRadarConversion && ( From.Map != Map.Malas || From.X < 407 || From.X > 431 || From.Y < 801 || From.Y > 830 ) )
			{
				m_SentRadarConversion = true;
				AddConversation( new RadarConversation() );
			}

			base.Slice();
		}

		public override void ChildDeserialize( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();

			m_SentRadarConversion = reader.ReadBool();
		}

		public override void ChildSerialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( (int) 0 ); // version

			writer.Write( (bool) m_SentRadarConversion );
		}

		public static bool HasLostNoteForZoel( Mobile from )
		{
			if ( !(from is PlayerMobile pm) )
				return false;

			QuestSystem qs = pm.Quest;

			if ( qs is EminosUndertakingQuest )
			{
				if ( qs.IsObjectiveInProgress( typeof( GiveZoelNoteObjective ) ) )
				{
					return ( from.Backpack?.FindItemByType( typeof( NoteForZoel ) ) == null );
				}
			}

			return false;
		}

		public static bool HasLostEminosKatana( Mobile from )
		{
			if ( !(from is PlayerMobile pm) )
				return false;

			QuestSystem qs = pm.Quest;

			if ( qs is EminosUndertakingQuest )
			{
				if ( qs.IsObjectiveInProgress( typeof( GiveEminoSwordObjective ) ) )
				{
					return ( from.Backpack?.FindItemByType( typeof( EminosKatana ) ) == null );
				}
			}

			return false;
		}
	}
}
