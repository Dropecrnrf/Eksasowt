using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
/*
 *Auteurs : Pedro Carneiro & Achraf Zamader
 *Classe : IFDAP4C
 *Date :
 *Fichier : PlayerAnimation.cs
 */
namespace Eksasowt
{
    public class PlayerAnimation
    {
        private List<Texture2D> _animationTextures; // Liste des textures pour l'animation
        private int _currentFrame; // Indice de la frame actuelle de l'animation
        private float _frameTimer; // Chronomètre pour mesurer le temps écoulé entre les frames
        private float _frameInterval; // Intervalle entre chaque frame de l'animation
        private int _frameCount; // Nombre total de frames dans l'animation

        //Constructeur
        public PlayerAnimation(List<Texture2D> animationTextures, int frameCount)
        {
            // Initialisations
            _animationTextures = animationTextures; 
            _currentFrame = 0;
            _frameTimer = 0f;
            _frameCount = frameCount;
            _frameInterval = 0.1f; 
        }

        public void Update(GameTime gameTime)
        {
            // Mettez à jour le chronomètre d'animation en ajoutant le temps écoulé depuis la dernière frame
            _frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Changez de frame si l'intervalle est écoulé
            if (_frameTimer >= _frameInterval)
            {
                _currentFrame = (_currentFrame + 1) % _frameCount; // Passez à la frame suivante en bouclant si nécessaire
                _frameTimer = 0f; // Réinitialisez le chronomètre
            }
        }

        public Texture2D GetCurrentFrame()
        {
            return _animationTextures[_currentFrame]; // Retourne la texture de la frame actuelle
        }
    }
}
