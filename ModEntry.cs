using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Netcode;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using StardewValley.BellsAndWhistles;
using StardewValley.ItemTypeDefinitions;
using StardewValley.Menus;
using StardewValley.Objects;
using StardewValley.Tools;
using xTile.Dimensions;

namespace FarmerPortraits;

public class ModEntry : Mod
{
	[HarmonyPatch(typeof(DialogueBox), new Type[] { typeof(Dialogue) })]
	[HarmonyPatch(/*Could not decode attribute arguments.*/)]
	public class DialogueBox_Patch
	{
		public static void Postfix(DialogueBox __instance)
		{
			if (Config.EnableMod && __instance.transitionInitialized && !__instance.transitioning && (Config.ShowWithQuestions || !__instance.isQuestion) && (Config.ShowWithNPCPortrait || !__instance.isPortraitBox()) && (Config.ShowWithEvents || !Game1.eventUp) && (Config.ShowMisc || __instance.isQuestion || __instance.isPortraitBox()))
			{
				AdjustWindow(ref __instance);
			}
		}
	}

	[HarmonyPatch(typeof(DialogueBox), "setUpIcons")]
	public class DialogueBox_setUpIcons_Patch
	{
		public static void Prefix(DialogueBox __instance)
		{
			if (Config.EnableMod && __instance.transitionInitialized && !__instance.transitioning && (Config.ShowWithQuestions || !__instance.isQuestion) && (Config.ShowWithNPCPortrait || !__instance.isPortraitBox()) && (Config.ShowWithEvents || !Game1.eventUp) && (Config.ShowMisc || __instance.isQuestion || __instance.isPortraitBox()))
			{
				AdjustWindow(ref __instance);
			}
		}
	}

	[HarmonyPatch(typeof(DialogueBox), "drawBox")]
	public class DialogueBox_drawBox_Patch
	{
		public static void Postfix(DialogueBox __instance, SpriteBatch b)
		{
			if (Config.EnableMod && __instance.transitionInitialized && !__instance.transitioning && (Config.ShowWithQuestions || !__instance.isQuestion) && (Config.ShowWithNPCPortrait || !__instance.isPortraitBox()) && (Config.ShowWithEvents || !Game1.eventUp) && (Config.ShowMisc || __instance.isQuestion || __instance.isPortraitBox()))
			{
				int num = 384;
				int num2 = 448;
				drawBox(b, __instance.x - num2 - 32, __instance.y + ((IClickableMenu)__instance).height - num, num2, num, __instance.isPortraitBox() ? __instance.characterDialogue.getPortraitIndex() : (-1));
			}
		}

		private static void drawBox(SpriteBatch b, int xPos, int yPos, int boxWidth, int boxHeight, int which)
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0109: Unknown result type (might be due to invalid IL or missing references)
			//IL_011c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0126: Unknown result type (might be due to invalid IL or missing references)
			//IL_0130: Unknown result type (might be due to invalid IL or missing references)
			//IL_0154: Unknown result type (might be due to invalid IL or missing references)
			//IL_0165: Unknown result type (might be due to invalid IL or missing references)
			//IL_016f: Unknown result type (might be due to invalid IL or missing references)
			//IL_018b: Unknown result type (might be due to invalid IL or missing references)
			//IL_019e: Unknown result type (might be due to invalid IL or missing references)
			//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01db: Unknown result type (might be due to invalid IL or missing references)
			//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0202: Unknown result type (might be due to invalid IL or missing references)
			//IL_0279: Unknown result type (might be due to invalid IL or missing references)
			//IL_028c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0296: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0250: Unknown result type (might be due to invalid IL or missing references)
			//IL_025f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0324: Unknown result type (might be due to invalid IL or missing references)
			//IL_0330: Unknown result type (might be due to invalid IL or missing references)
			//IL_033a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0381: Unknown result type (might be due to invalid IL or missing references)
			//IL_039f: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
			//IL_040d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0412: Unknown result type (might be due to invalid IL or missing references)
			//IL_041c: Unknown result type (might be due to invalid IL or missing references)
			b.Draw(Game1.mouseCursors, new Rectangle(xPos, yPos - 20, boxWidth, 24), (Rectangle?)new Rectangle(275, 313, 1, 6), Color.White);
			b.Draw(Game1.mouseCursors, new Rectangle(xPos + 12, yPos + boxHeight, boxWidth - 20, 32), (Rectangle?)new Rectangle(275, 328, 1, 8), Color.White);
			b.Draw(Game1.mouseCursors, new Rectangle(xPos - 32, yPos + 24, 32, boxHeight - 28), (Rectangle?)new Rectangle(264, 325, 8, 1), Color.White);
			b.Draw(Game1.mouseCursors, new Vector2((float)(xPos - 44), (float)(yPos - 28)), (Rectangle?)new Rectangle(261, 311, 14, 13), Color.White, 0f, Vector2.Zero, 4f, (SpriteEffects)0, 0.87f);
			b.Draw(Game1.mouseCursors, new Vector2((float)(xPos - 44), (float)(yPos + boxHeight - 4)), (Rectangle?)new Rectangle(261, 327, 14, 11), Color.White, 0f, Vector2.Zero, 4f, (SpriteEffects)0, 0.87f);
			b.Draw(Game1.mouseCursors, new Rectangle(xPos + boxWidth, yPos, 28, boxHeight), (Rectangle?)new Rectangle(293, 324, 7, 1), Color.White);
			b.Draw(Game1.mouseCursors, new Vector2((float)(xPos + boxWidth - 8), (float)(yPos - 28)), (Rectangle?)new Rectangle(291, 311, 12, 11), Color.White, 0f, Vector2.Zero, 4f, (SpriteEffects)0, 0.87f);
			b.Draw(Game1.mouseCursors, new Vector2((float)(xPos + boxWidth - 8), (float)(yPos + boxHeight - 8)), (Rectangle?)new Rectangle(291, 326, 12, 12), Color.White, 0f, Vector2.Zero, 4f, (SpriteEffects)0, 0.87f);
			if (Config.UseCustomBackground)
			{
				Texture2D cachedTexture = GetCachedTexture("background", which);
				if (cachedTexture != null)
				{
					b.Draw(cachedTexture, new Rectangle(xPos - 4, yPos, boxWidth + 12, boxHeight + 4), (Rectangle?)null, Color.White);
					goto IL_02b6;
				}
			}
			b.Draw(Game1.mouseCursors, new Vector2((float)(xPos - 4), (float)yPos), (Rectangle?)new Rectangle(583, 411, 115, 97), Color.White, 0f, Vector2.Zero, 4f, (SpriteEffects)0, 0.88f);
			goto IL_02b6;
			IL_02b6:
			int num = xPos + 76;
			int num2 = yPos + boxHeight / 2 - 148 - 36;
			int num3 = ((!Config.FacingFront) ? 6 : 0);
			if (Config.UseCustomPortrait)
			{
				Texture2D cachedTexture2 = GetCachedTexture("portrait", which);
				if (cachedTexture2 != null)
				{
					Rectangle val = default(Rectangle);
					((Rectangle)(ref val))._002Ector(num + 20, num2 + 24, 256, 256);
					b.Draw(cachedTexture2, val, (Rectangle?)null, Color.White, 0f, Vector2.Zero, (SpriteEffects)0, 0.88f);
					goto IL_042e;
				}
			}
			FarmerRenderer.isDrawingForUI = true;
			drawFarmer(b, num3, new Rectangle(num3 % 6 * 16, ((NetFieldBase<bool, NetBool>)(object)Game1.player.bathingClothes).Value ? 576 : (num3 / 6 * 32), 16, 16), new Vector2((float)(xPos + boxWidth / 2 - 128), (float)(yPos + boxHeight / 2 - 208)), Color.White);
			if (Game1.timeOfDay >= 1900)
			{
				drawFarmer(b, num3, new Rectangle(num3 % 6 * 16, ((NetFieldBase<bool, NetBool>)(object)Game1.player.bathingClothes).Value ? 576 : (num3 / 6 * 32), 16, 16), new Vector2((float)(xPos + boxWidth / 2 - 128), (float)(yPos + boxHeight / 2 - 192)), Color.DarkBlue * 0.3f);
			}
			FarmerRenderer.isDrawingForUI = false;
			goto IL_042e;
			IL_042e:
			SpriteText.drawStringHorizontallyCenteredAt(b, ((Character)Game1.player).Name, xPos + boxWidth / 2, num2 + 296 + 16, 999999, -1, 999999, 1f, 0.88f, false, (Color?)null, 99999);
		}

		private static void drawFarmer(SpriteBatch b, int currentFrame, Rectangle sourceRect, Vector2 position, Color overrideColor)
		{
			//IL_0079: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0397: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_040f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0201: Unknown result type (might be due to invalid IL or missing references)
			//IL_0207: Unknown result type (might be due to invalid IL or missing references)
			//IL_020c: Unknown result type (might be due to invalid IL or missing references)
			//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_049b: Unknown result type (might be due to invalid IL or missing references)
			//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
			//IL_0225: Unknown result type (might be due to invalid IL or missing references)
			//IL_022f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0236: Unknown result type (might be due to invalid IL or missing references)
			//IL_024f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0250: Unknown result type (might be due to invalid IL or missing references)
			//IL_0252: Unknown result type (might be due to invalid IL or missing references)
			//IL_0536: Unknown result type (might be due to invalid IL or missing references)
			//IL_0538: Unknown result type (might be due to invalid IL or missing references)
			//IL_081a: Unknown result type (might be due to invalid IL or missing references)
			//IL_081c: Unknown result type (might be due to invalid IL or missing references)
			//IL_055f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0561: Unknown result type (might be due to invalid IL or missing references)
			//IL_0277: Unknown result type (might be due to invalid IL or missing references)
			//IL_027d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0282: Unknown result type (might be due to invalid IL or missing references)
			//IL_058f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0590: Unknown result type (might be due to invalid IL or missing references)
			//IL_0592: Unknown result type (might be due to invalid IL or missing references)
			//IL_0759: Unknown result type (might be due to invalid IL or missing references)
			//IL_075a: Unknown result type (might be due to invalid IL or missing references)
			//IL_075c: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
			//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
			//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
			//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
			//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
			//IL_088f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0890: Unknown result type (might be due to invalid IL or missing references)
			//IL_0892: Unknown result type (might be due to invalid IL or missing references)
			//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
			//IL_08c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_08d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_0710: Unknown result type (might be due to invalid IL or missing references)
			//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a4e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a50: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09b5: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_09de: Unknown result type (might be due to invalid IL or missing references)
			//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
			//IL_09ec: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_08e0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0724: Unknown result type (might be due to invalid IL or missing references)
			//IL_071a: Unknown result type (might be due to invalid IL or missing references)
			//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
			//IL_05ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a06: Unknown result type (might be due to invalid IL or missing references)
			//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
			//IL_090e: Unknown result type (might be due to invalid IL or missing references)
			//IL_090f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0911: Unknown result type (might be due to invalid IL or missing references)
			//IL_093a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0940: Unknown result type (might be due to invalid IL or missing references)
			//IL_0945: Unknown result type (might be due to invalid IL or missing references)
			//IL_094a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0953: Unknown result type (might be due to invalid IL or missing references)
			//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
			//IL_07b7: Unknown result type (might be due to invalid IL or missing references)
			//IL_07bc: Unknown result type (might be due to invalid IL or missing references)
			//IL_07c5: Unknown result type (might be due to invalid IL or missing references)
			//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0618: Unknown result type (might be due to invalid IL or missing references)
			//IL_0619: Unknown result type (might be due to invalid IL or missing references)
			//IL_061b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
			//IL_0964: Unknown result type (might be due to invalid IL or missing references)
			//IL_0969: Unknown result type (might be due to invalid IL or missing references)
			//IL_095f: Unknown result type (might be due to invalid IL or missing references)
			//IL_07db: Unknown result type (might be due to invalid IL or missing references)
			//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aa6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ab9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0973: Unknown result type (might be due to invalid IL or missing references)
			//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0652: Unknown result type (might be due to invalid IL or missing references)
			//IL_0657: Unknown result type (might be due to invalid IL or missing references)
			//IL_065c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0665: Unknown result type (might be due to invalid IL or missing references)
			//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ac5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0676: Unknown result type (might be due to invalid IL or missing references)
			//IL_067b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0671: Unknown result type (might be due to invalid IL or missing references)
			//IL_0ad9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0685: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c2b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c3f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c40: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c48: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c52: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c58: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b24: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b8b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
			//IL_0b96: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bb9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0baf: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bbe: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc6: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc7: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bce: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bd5: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c00: Unknown result type (might be due to invalid IL or missing references)
			//IL_0bf3: Unknown result type (might be due to invalid IL or missing references)
			//IL_0c0a: Unknown result type (might be due to invalid IL or missing references)
			AnimationFrame val = default(AnimationFrame);
			((AnimationFrame)(ref val))._002Ector(((NetFieldBase<bool, NetBool>)(object)Game1.player.bathingClothes).Value ? 108 : currentFrame, 0, false, false, (endOfAnimationBehavior)null, false);
			Farmer player = Game1.player;
			float num = 0.8f;
			float num2 = 4f;
			int num3 = FarmerRenderer.featureXOffsetPerFrame[currentFrame];
			int num4 = FarmerRenderer.featureYOffsetPerFrame[currentFrame];
			AccessTools.Method(typeof(FarmerRenderer), "executeRecolorActions", (Type[])null, (Type[])null).Invoke(Game1.player.FarmerRenderer, new object[1] { player });
			((Vector2)(ref position))._002Ector((float)Math.Floor(position.X), (float)Math.Floor(position.Y));
			Vector2 val2 = default(Vector2);
			((Vector2)(ref val2))._002Ector((float)(val.positionOffset * 4), (float)(val.positionOffset * 4));
			Texture2D val3 = AccessTools.FieldRefAccess<FarmerRenderer, Texture2D>(Game1.player.FarmerRenderer, "baseTexture");
			b.Draw(val3, position + val2, (Rectangle?)sourceRect, overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8f);
			((Rectangle)(ref sourceRect)).Offset(288, 0);
			if (player.currentEyes != 0 && (Game1.timeOfDay < 2600 || (((NetFieldBase<bool, NetBool>)(object)player.isInBed).Value && ((NetFieldBase<int, NetInt>)(object)player.timeWentToBed).Value != 0)) && ((!player.FarmerSprite.PauseForSingleAnimation && !player.UsingTool) || (player.UsingTool && player.CurrentTool is FishingRod)) && (!player.UsingTool || !(player.CurrentTool is FishingRod) || ((FishingRod)/*isinst with value type is only supported in some contexts*/).isFishing))
			{
				int num5 = 5 - FarmerRenderer.featureXOffsetPerFrame[currentFrame];
				if (!Config.FacingFront)
				{
					num5 += 3;
				}
				num5 *= 4;
				b.Draw(val3, position + val2 + new Vector2((float)num5, (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + ((player.IsMale && !Config.FacingFront) ? 36 : 40))) * num2, (Rectangle?)new Rectangle(5, 16, Config.FacingFront ? 6 : 2, 2), overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8000001f);
				b.Draw(val3, position + val2 + new Vector2((float)num5, (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + ((!Config.FacingFront) ? 40 : 44))) * num2, (Rectangle?)new Rectangle(264, 2 + (player.currentEyes - 1) * 2, Config.FacingFront ? 6 : 2, 2), overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.80000013f);
			}
			int num6 = player.getHair(false);
			HairStyleMetadata hairStyleMetadata = Farmer.GetHairStyleMetadata(((NetFieldBase<int, NetInt>)(object)player.hair).Value);
			if (player != null && ((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value != null && ((NetFieldBase<int, NetInt>)(object)((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value.hairDrawType).Value == 1 && hairStyleMetadata != null && hairStyleMetadata.coveredIndex != -1)
			{
				num6 = hairStyleMetadata.coveredIndex;
				hairStyleMetadata = Farmer.GetHairStyleMetadata(num6);
			}
			AccessTools.Method(typeof(FarmerRenderer), "executeRecolorActions", (Type[])null, (Type[])null).Invoke(Game1.player.FarmerRenderer, new object[1] { player });
			int num7 = 4;
			int num8 = 4;
			Texture2D val4 = default(Texture2D);
			int num9 = default(int);
			player.GetDisplayShirt(ref val4, ref num9);
			Color val5 = (((NetFieldBase<bool, NetBool>)(object)player.prismaticHair).Value ? Utility.GetPrismaticColor(0, 1f) : ((NetFieldBase<Color, NetColor>)(object)player.hairstyleColor).Value);
			Rectangle val6 = default(Rectangle);
			((Rectangle)(ref val6))._002Ector(num9 * 8 % 128, num9 * 8 / 128 * 32, 8, 8 - num8);
			Texture2D val7 = FarmerRenderer.hairStylesTexture;
			Rectangle value = default(Rectangle);
			((Rectangle)(ref value))._002Ector(num6 * 16 % FarmerRenderer.hairStylesTexture.Width, num6 * 16 / FarmerRenderer.hairStylesTexture.Width * 96, 16, 32);
			Texture2D hatsTexture = FarmerRenderer.hatsTexture;
			bool flag = false;
			Rectangle value2 = default(Rectangle);
			if (((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value != null)
			{
				ParsedItemData dataOrErrorItem = ItemRegistry.GetDataOrErrorItem(((Item)((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value).QualifiedItemId);
				int spriteIndex = dataOrErrorItem.SpriteIndex;
				hatsTexture = dataOrErrorItem.GetTexture();
				((Rectangle)(ref value2))._002Ector(20 * spriteIndex % hatsTexture.Width, 20 * spriteIndex / hatsTexture.Width * 20 * 4, 20, 20 - num7);
				if (dataOrErrorItem.IsErrorItem)
				{
					value2 = dataOrErrorItem.GetSourceRect(0, (int?)null);
					flag = true;
				}
			}
			Rectangle value3 = ((((NetFieldBase<int, NetInt>)(object)player.accessory).Value >= 0) ? new Rectangle(((NetFieldBase<int, NetInt>)(object)player.accessory).Value * 16 % FarmerRenderer.accessoriesTexture.Width, ((NetFieldBase<int, NetInt>)(object)player.accessory).Value * 16 / FarmerRenderer.accessoriesTexture.Width * 32, 16, 16) : default(Rectangle));
			if (hairStyleMetadata != null)
			{
				val7 = hairStyleMetadata.texture;
				((Rectangle)(ref value))._002Ector(hairStyleMetadata.tileX * 16, hairStyleMetadata.tileY * 16, 16, 32);
			}
			Rectangle value4 = val6;
			float num10 = 1E-07f;
			float num11 = 2.2E-05f;
			int num12 = 0;
			if (Config.FacingFront)
			{
				value4 = val6;
				((Rectangle)(ref value4)).Offset(128, 0);
				if (!((NetFieldBase<bool, NetBool>)(object)player.bathingClothes).Value)
				{
					b.Draw(FarmerRenderer.shirtsTexture, position + val2 + new Vector2((float)(16 + FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(56 + FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4) + (float)num12 * 4f - (float)(player.IsMale ? 0 : 0)) * num2, (Rectangle?)val6, ((Color)(ref overrideColor)).Equals(Color.White) ? Color.White : overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8000002f);
					b.Draw(FarmerRenderer.shirtsTexture, position + val2 + new Vector2((float)(16 + FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(56 + FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4) + (float)num12 * 4f - (float)(player.IsMale ? 0 : 0)), (Rectangle?)value4, ((Color)(ref overrideColor)).Equals(Color.White) ? Utility.MakeCompletelyOpaque(player.GetShirtColor()) : overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8000002f + num10);
				}
				if (((NetFieldBase<int, NetInt>)(object)player.accessory).Value >= 0)
				{
					b.Draw(FarmerRenderer.accessoriesTexture, position + val2 + new Vector2((float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(8 + FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + num12 - 4)), (Rectangle?)value3, (((Color)(ref overrideColor)).Equals(Color.White) && ((NetFieldBase<int, NetInt>)(object)player.accessory).Value < 6) ? ((NetFieldBase<Color, NetColor>)(object)player.hairstyleColor).Value : overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8f + ((((NetFieldBase<int, NetInt>)(object)player.accessory).Value < 8) ? 1.9E-05f : 2.9E-05f));
				}
				b.Draw(val7, position + val2 + new Vector2((float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + ((player.IsMale && ((NetFieldBase<int, NetInt>)(object)player.hair).Value >= 16) ? (-4) : ((!player.IsMale && ((NetFieldBase<int, NetInt>)(object)player.hair).Value < 16) ? 4 : 0)))) * num2, (Rectangle?)value, ((Color)(ref overrideColor)).Equals(Color.White) ? ((NetFieldBase<Color, NetColor>)(object)player.hairstyleColor).Value : overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8f + num11);
			}
			else
			{
				((Rectangle)(ref val6)).Offset(0, 8);
				((Rectangle)(ref value)).Offset(0, 32);
				value4 = val6;
				((Rectangle)(ref value4)).Offset(128, 0);
				if (((NetFieldBase<int, NetInt>)(object)player.accessory).Value >= 0)
				{
					((Rectangle)(ref value3)).Offset(0, 16);
				}
				if (((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value != null)
				{
					((Rectangle)(ref value2)).Offset(0, 20);
				}
				if (!((NetFieldBase<bool, NetBool>)(object)player.bathingClothes).Value)
				{
					b.Draw(FarmerRenderer.shirtsTexture, position + val2 + new Vector2(16f + (float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), 56f + (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4) + (float)num12) * num2, (Rectangle?)val6, ((Color)(ref overrideColor)).Equals(Color.White) ? Color.White : overrideColor, 0f, Vector2.Zero, 4f * num2, (SpriteEffects)0, num + 1.8E-07f);
					b.Draw(FarmerRenderer.shirtsTexture, position + val2 + new Vector2(16f + (float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), 56f + (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4) + (float)num12) * num2, (Rectangle?)value4, ((Color)(ref overrideColor)).Equals(Color.White) ? Utility.MakeCompletelyOpaque(player.GetShirtColor()) : overrideColor, 0f, Vector2.Zero, 4f * num2, (SpriteEffects)0, num + 1.8E-07f + num10);
				}
				if (((NetFieldBase<int, NetInt>)(object)player.accessory).Value >= 0)
				{
					b.Draw(FarmerRenderer.accessoriesTexture, position + val2 + new Vector2((float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(4 + FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + num12)) * num2, (Rectangle?)value3, (((Color)(ref overrideColor)).Equals(Color.White) && ((NetFieldBase<int, NetInt>)(object)player.accessory).Value < 6) ? ((NetFieldBase<Color, NetColor>)(object)player.hairstyleColor).Value : overrideColor, 0f, Vector2.Zero, 4f * num2, (SpriteEffects)0, num + ((((NetFieldBase<int, NetInt>)(object)player.accessory).Value < 8) ? 1.9E-05f : 2.9E-05f));
				}
				b.Draw(val7, position + val2 + new Vector2((float)(FarmerRenderer.featureXOffsetPerFrame[currentFrame] * 4), (float)(FarmerRenderer.featureYOffsetPerFrame[currentFrame] * 4 + ((player.IsMale && ((NetFieldBase<int, NetInt>)(object)player.hair).Value >= 16) ? (-4) : ((!player.IsMale && ((NetFieldBase<int, NetInt>)(object)player.hair).Value < 16) ? 4 : 0)))) * num2, (Rectangle?)value, ((Color)(ref overrideColor)).Equals(Color.White) ? ((NetFieldBase<Color, NetColor>)(object)player.hairstyleColor).Value : overrideColor, 0f, Vector2.Zero, 16f, (SpriteEffects)0, num + num11);
			}
			if (((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value != null && !((NetFieldBase<bool, NetBool>)(object)player.bathingClothes).Value)
			{
				float num13 = 3.9E-05f;
				bool flip = player.FarmerSprite.CurrentAnimationFrame.flip;
				int num14 = ((!((NetFieldBase<bool, NetBool>)(object)((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value.ignoreHairstyleOffset).Value) ? FarmerRenderer.hairstyleHatOffset[((NetFieldBase<int, NetInt>)(object)player.hair).Value % 16] : 0);
				Vector2 val8 = new Vector2(-8f + (float)(((!flip) ? 1 : (-1)) * num3), -12f + (float)(num4 * 4) + (float)num14 + 4f + (float)num12) * num2;
				Color val9 = (((NetFieldBase<bool, NetBool>)(object)((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value.isPrismatic).Value ? Utility.GetPrismaticColor(0, 1f) : overrideColor);
				b.Draw(FarmerRenderer.hatsTexture, position + val2 + val8, (Rectangle?)value2, ((NetFieldBase<bool, NetBool>)(object)((NetFieldBase<Hat, NetRef<Hat>>)(object)player.hat).Value.isPrismatic).Value ? Utility.GetPrismaticColor(0, 1f) : Color.White, 0f, Vector2.Zero, 16f, (SpriteEffects)0, 0.8f + num13);
			}
			((Rectangle)(ref sourceRect)).Offset(-288 + val.armOffset * 16, 0);
			b.Draw(val3, position + val2 + player.armOffset, (Rectangle?)sourceRect, overrideColor, 0f, Vector2.Zero, 4f * num2, (SpriteEffects)0, FarmerRenderer.GetLayerDepth(num, (FarmerSpriteLayers)14, false));
		}
	}

	public static IMonitor SMonitor;

	public static IModHelper SHelper;

	public static ModConfig Config;

	public static ModEntry context;

	private static PerScreen<Dictionary<string, Texture2D>> portraitTextures = new PerScreen<Dictionary<string, Texture2D>>((Func<Dictionary<string, Texture2D>>)(() => new Dictionary<string, Texture2D>()));

	private static void AdjustWindow(ref DialogueBox __instance)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		__instance.x = Math.Max(520, (int)Utility.getTopLeftPositionForCenteringOnScreen(((IClickableMenu)__instance).width, ((IClickableMenu)__instance).height, 0, 0).X + 260);
		((IClickableMenu)__instance).width = Math.Min(((Rectangle)(ref Game1.uiViewport)).Width - __instance.x - 48, 1200);
		__instance.friendshipJewel = new Rectangle(__instance.x + ((IClickableMenu)__instance).width - 64, __instance.y + 256, 44, 44);
	}

	private static void ReloadTextures()
	{
		portraitTextures.Value.Clear();
		string[] array = new string[2] { "background", "portrait" };
		foreach (string text in array)
		{
			Texture2D val = null;
			int num = 0;
			while (true)
			{
				val = GetAssetTexture(text + num);
				if (val != null)
				{
					portraitTextures.Value[text + num] = val;
				}
				else if (num > 5)
				{
					break;
				}
				num++;
			}
			val = GetAssetTexture(text);
			if (val != null)
			{
				portraitTextures.Value[text] = val;
			}
		}
	}

	public static Texture2D GetCachedTexture(string v, int which)
	{
		if (which > -1 && portraitTextures.Value.TryGetValue(v + which, out var value))
		{
			return value;
		}
		if (portraitTextures.Value.TryGetValue(v, out value))
		{
			return value;
		}
		return null;
	}

	public static Texture2D GetAssetTexture(string v)
	{
		if (SHelper.ModContent.DoesAssetExist<Texture2D>(v + "_" + ((Character)Game1.player).Name + ".png"))
		{
			return SHelper.ModContent.Load<Texture2D>(v + "_" + ((Character)Game1.player).Name + ".png");
		}
		if (SHelper.ModContent.DoesAssetExist<Texture2D>(v + ".png"))
		{
			return SHelper.ModContent.Load<Texture2D>(v + ".png");
		}
		if (SHelper.GameContent.DoesAssetExist<Texture2D>(SHelper.GameContent.ParseAssetName("aedenthorn.FarmerPortraits/" + v + "_" + ((Character)Game1.player).Name)))
		{
			return SHelper.GameContent.Load<Texture2D>("aedenthorn.FarmerPortraits/" + v + "_" + ((Character)Game1.player).Name);
		}
		if (SHelper.GameContent.DoesAssetExist<Texture2D>(SHelper.GameContent.ParseAssetName("aedenthorn.FarmerPortraits/" + v)))
		{
			return SHelper.GameContent.Load<Texture2D>("aedenthorn.FarmerPortraits/" + v);
		}
		return null;
	}

	private void SetGlobalPortrait(string arg1, string[] arg2)
	{
		string path = string.Join(' ', arg2);
		SetTexture("portrait.png", path);
	}

	private void SetThisPortrait(string arg1, string[] arg2)
	{
		string path = string.Join(' ', arg2);
		SetTexture("portrait_" + ((Character)Game1.player).Name + ".png", path);
	}

	private void SetGlobalBackground(string arg1, string[] arg2)
	{
		string path = string.Join(' ', arg2);
		SetTexture("background.png", path);
	}

	private void SetThisBackground(string arg1, string[] arg2)
	{
		string path = string.Join(' ', arg2);
		SetTexture("background_" + ((Character)Game1.player).Name + ".png", path);
	}

	private void SetTexture(string output, string path)
	{
		if (!path.EndsWith(".png"))
		{
			SMonitor.Log("File " + path + " doesn't have the .png extension.", (LogLevel)0);
			return;
		}
		if (!File.Exists(path))
		{
			SMonitor.Log("File " + path + " doesn't exist or can't be accessed", (LogLevel)0);
			return;
		}
		string text = Path.Combine(SHelper.DirectoryPath, output);
		if (File.Exists(text))
		{
			int num = 0;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler;
			while (true)
			{
				defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendFormatted(text);
				defaultInterpolatedStringHandler.AppendLiteral(".bkp");
				defaultInterpolatedStringHandler.AppendFormatted((num == 0) ? "" : ((object)num));
				if (!File.Exists(defaultInterpolatedStringHandler.ToStringAndClear()))
				{
					break;
				}
				num++;
			}
			defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
			defaultInterpolatedStringHandler.AppendFormatted(text);
			defaultInterpolatedStringHandler.AppendLiteral(".bkp");
			defaultInterpolatedStringHandler.AppendFormatted((num == 0) ? "" : ((object)num));
			File.Move(text, defaultInterpolatedStringHandler.ToStringAndClear());
		}
		File.Copy(path, text);
		SMonitor.Log("Copied " + path + " to " + text, (LogLevel)0);
		ReloadTextures();
	}

	public override void Entry(IModHelper helper)
	{
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		Config = ((Mod)this).Helper.ReadConfig<ModConfig>();
		context = this;
		SMonitor = ((Mod)this).Monitor;
		SHelper = helper;
		helper.Events.GameLoop.GameLaunched += GameLoop_GameLaunched;
		helper.Events.Input.ButtonPressed += Input_ButtonPressed;
		helper.Events.Display.MenuChanged += Display_MenuChanged;
		Harmony val = new Harmony(((Mod)this).ModManifest.UniqueID);
		val.PatchAll();
		helper.ConsoleCommands.Add("fppme", "Set the portrait for this farmer.", (Action<string, string[]>)SetThisPortrait);
		helper.ConsoleCommands.Add("fppg", "Set the portrait for all farmers.", (Action<string, string[]>)SetGlobalPortrait);
		helper.ConsoleCommands.Add("fpbme", "Set the background for this farmer.", (Action<string, string[]>)SetThisBackground);
		helper.ConsoleCommands.Add("fpbg", "Set the background for all farmers.", (Action<string, string[]>)SetGlobalBackground);
	}

	private void Input_ButtonPressed(object sender, ButtonPressedEventArgs e)
	{
		if (Config.EnableMod)
		{
			bool flag = false;
		}
	}

	private void Display_MenuChanged(object sender, MenuChangedEventArgs e)
	{
		if (Config.EnableMod && Game1.activeClickableMenu != null)
		{
			ReloadTextures();
		}
	}

	private void GameLoop_GameLaunched(object sender, GameLaunchedEventArgs e)
	{
		IGenericModConfigMenuApi api = ((Mod)this).Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");
		if (api != null)
		{
			api.Register(((Mod)this).ModManifest, delegate
			{
				Config = new ModConfig();
			}, delegate
			{
				((Mod)this).Helper.WriteConfig<ModConfig>(Config);
			});
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.EnableMod, delegate(bool value)
			{
				Config.EnableMod = value;
			}, () => "Mod Enabled");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.ShowWithNPCPortrait, delegate(bool value)
			{
				Config.ShowWithNPCPortrait = value;
			}, () => "Show With NPCs");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.ShowWithQuestions, delegate(bool value)
			{
				Config.ShowWithQuestions = value;
			}, () => "Show With Questions");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.ShowMisc, delegate(bool value)
			{
				Config.ShowMisc = value;
			}, () => "Show Otherwise", () => "Show for dialogue boxes that are neither questions nor have NPC portraits");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.ShowWithEvents, delegate(bool value)
			{
				Config.ShowWithEvents = value;
			}, () => "Show During Events");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.FacingFront, delegate(bool value)
			{
				Config.FacingFront = value;
			}, () => "Facing Front", () => "If not set, the portrait will face right (only meaningful if there is no custom portrait)");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.UseCustomPortrait, delegate(bool value)
			{
				Config.UseCustomPortrait = value;
			}, () => "Use Custom Portrait", () => "If a custom portrait png is loaded, use it for the portrait");
			api.AddBoolOption(((Mod)this).ModManifest, () => Config.UseCustomBackground, delegate(bool value)
			{
				Config.UseCustomBackground = value;
			}, () => "Use Custom Background", () => "If a custom background png is loaded, use it for the background");
		}
	}
}
