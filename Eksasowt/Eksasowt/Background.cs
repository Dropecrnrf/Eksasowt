using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 *Auteurs : Pedro Carneiro & Achraf Zamader
 *Classe : IFDAP4C
 *Date :
 *Fichier : Background.cs
 */
namespace Eksasowt
{
    public class Background
    {
        // Liste des textures d'arrière-plan
        private List<Texture2D> backgroundTextures;

        // Index de l'arrière-plan actuel
        private int currentBackgroundIndex;

        // Valeur de l'alpha pour la transition entre les arrière-plans
        private float transitionAlpha;

        // Vitesse de transition
        private float transitionSpeed = 0.01f;

        // Largeur de l'écran
        private int screenWidth;

        // Hauteur de l'écran
        private int screenHeight;

        // Constructeur
        public Background(List<Texture2D> backgroundTextures, int screenWidth, int screenHeight)
        {
            // Initialisations
            this.backgroundTextures = backgroundTextures;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            currentBackgroundIndex = 0;
            transitionAlpha = 0;
        }

        // Méthode pour charger l'arrière-plan suivant
        public void LoadNextBackground()
        {
            // Passe à l'arrière-plan suivant en bouclant si nécessaire
            currentBackgroundIndex = (currentBackgroundIndex + 1) % backgroundTextures.Count;

            // Réinitialise l'alpha de la transition à zéro
            transitionAlpha = 0;
        }

        public void Update()
        {
            // Augmente progressivement l'alpha de la transition jusqu'à 1
            if (transitionAlpha < 1)
            {
                transitionAlpha += transitionSpeed;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (backgroundTextures.Count > 0)
            {
                // Calcule l'index de l'arrière-plan suivant
                int nextBackgroundIndex = (currentBackgroundIndex + 1) % backgroundTextures.Count;

                // Récupère les textures de l'arrière-plan actuel et du suivant
                Texture2D currentBackground = backgroundTextures[currentBackgroundIndex];
                Texture2D nextBackground = backgroundTextures[nextBackgroundIndex];

                // Crée un rectangle de la taille de l'écran
                Rectangle screenRectangle = new Rectangle(0, 0, screenWidth, screenHeight);

                // Dessine les arrière-plans en plein écran avec l'alpha de transition
                spriteBatch.Draw(currentBackground, screenRectangle, Color.White * (1 - transitionAlpha));
                spriteBatch.Draw(nextBackground, screenRectangle, Color.White * transitionAlpha);
            }
        }

        // Méthode pour commencer la transition vers l'arrière-plan suivant
        public void StartTransition()
        {
            // Charge l'arrière-plan suivant
            LoadNextBackground();

            // Réinitialise l'alpha de transition à zéro
            transitionAlpha = 0;
        }
    }
}
