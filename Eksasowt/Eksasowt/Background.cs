using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eksasowt
{
    public class Background
    {
        private List<Texture2D> backgroundTextures;
        private int currentBackgroundIndex;
        private float transitionAlpha;
        private float transitionSpeed = 0.01f;
        private int screenWidth;
        private int screenHeight;

        public Background(List<Texture2D> backgroundTextures, int screenWidth, int screenHeight)
        {
            this.backgroundTextures = backgroundTextures;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            currentBackgroundIndex = 0;
            transitionAlpha = 0;
        }

        public void LoadNextBackground()
        {
            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundTextures.Count;
            transitionAlpha = 0;
        }

        public void Update()
        {
            if (transitionAlpha < 1)
            {
                transitionAlpha += transitionSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (backgroundTextures.Count > 0)
            {
                int nextBackgroundIndex = (currentBackgroundIndex + 1) % backgroundTextures.Count;
                Texture2D currentBackground = backgroundTextures[currentBackgroundIndex];
                Texture2D nextBackground = backgroundTextures[nextBackgroundIndex];

                Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

                // Dessinez les arrière-plans en plein écran avec la transitionAlpha
                spriteBatch.Draw(currentBackground, screenRectangle, Color.White * (1 - transitionAlpha));
                spriteBatch.Draw(nextBackground, screenRectangle, Color.White * transitionAlpha);
            }
        }

        public void StartTransition()
        {
            LoadNextBackground(); // Chargez le fond d'écran suivant
            transitionAlpha = 0; // Réinitialisez le fondu à zéro
        }
    }
}
