using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace FarmerPortraits
{
    public class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.Display.RenderedActiveMenu += OnRenderedActiveMenu;
        }

        private void OnRenderedActiveMenu(object sender, RenderedActiveMenuEventArgs e)
        {
            if (Game1.activeClickableMenu is DialogueBox dialogueBox)
            {
                SpriteBatch b = e.SpriteBatch;
                DrawCustomPortrait(b, dialogueBox);
            }
        }

        private void DrawCustomPortrait(SpriteBatch b, DialogueBox box)
        {
            // Ajustamos las coordenadas si es necesario
            int x = box.xPositionOnScreen + 20;
            int y = box.yPositionOnScreen + 24;
            
            // Ahora busca 'portrait.png' en la raíz del mod
            Texture2D portrait = Helper.ModContent.Load<Texture2D>("portrait.png");
            
            if (portrait != null)
            {
                b.Draw(portrait, new Rectangle(x, y, 256, 256), Color.White);
            }
        }
    }
}
