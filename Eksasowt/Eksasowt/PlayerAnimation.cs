using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eksasowt
{
    public class PlayerAnimation
    {
        private List<Texture2D> _animationTextures;
        private int _currentFrame;
        private float _frameTimer;
        private float _frameInterval;
        private int _frameCount;

        public PlayerAnimation(List<Texture2D> animationTextures, int frameCount)
        {
            _animationTextures = animationTextures;
            _currentFrame = 0;
            _frameTimer = 0f;
            _frameCount = frameCount;
            _frameInterval = 0.1f; // Intervalle entre chaque image d'animation (peut être ajusté selon vos besoins)
        }

        public void Update(GameTime gameTime)
        {
            // Mettez à jour le chronomètre d'animation
            _frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Changez de frame si l'intervalle est écoulé
            if (_frameTimer >= _frameInterval)
            {
                _currentFrame = (_currentFrame + 1) % _frameCount;
                _frameTimer = 0f;
            }
        }

        public Texture2D GetCurrentFrame()
        {
            return _animationTextures[_currentFrame];
        }
    }
}
