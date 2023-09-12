using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Eksasowt
{
    public class Player
    {
        private Texture2D _texture;
        private Vector2 _position;
        private int _speed = 200;
        private float _jumpSpeed = 500; // Vitesse du saut normal
        private float _superJumpSpeed = 700; // Vitesse du super saut
        private float _gravity = 1000; // Force de gravité
        private bool _isJumping;
        private bool _isSuperJumping;
        private float _jumpTime = 0.3f; // Durée du saut normal
        private float _superJumpTime = 0.5f; // Durée du super saut
        private float _jumpTimer;
        private float _verticalVelocity = 0; // Vitesse verticale du joueur

        private PlayerAnimation _walkLeftAnimation;
        private PlayerAnimation _walkRightAnimation;
        private bool _isWalkingLeft;
        private bool _isWalkingRight;

        public Player(Texture2D texture, int screenWidth, int screenHeight, List<Texture2D> walkLeftFrames, List<Texture2D> walkRightFrames)
        {
            _texture = texture;
            _position = new Vector2((screenWidth - _texture.Width) / 2, screenHeight - _texture.Height);

            _walkLeftAnimation = new PlayerAnimation(walkLeftFrames, 8); // 8 frames pour la marche à gauche
            _walkRightAnimation = new PlayerAnimation(walkRightFrames, 8); // 8 frames pour la marche à droite
        }

        public void Update(GameTime gameTime, int windowWidth, int windowHeight)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            // Gestion de la gravité
            _verticalVelocity += _gravity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position.Y += _verticalVelocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Empêcher le joueur de dépasser le bas de la fenêtre
            if (_position.Y >= windowHeight - _texture.Height)
            {
                _position.Y = windowHeight - _texture.Height;
                _verticalVelocity = 0;
            }

            // Gestion du saut
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (!_isJumping)
                {
                    // Commencez le saut normal
                    _isJumping = true;
                    _jumpTimer = 0f;
                    _verticalVelocity = -_jumpSpeed;
                }
                else if (_isSuperJumping && _jumpTimer < _superJumpTime)
                {
                    // Appliquez la vitesse du super saut pendant la durée du super saut
                    _verticalVelocity = -_superJumpSpeed;
                }
            }
            else
            {
                _isJumping = false;
            }

            // Mise à jour de la position horizontale
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                _isWalkingLeft = true;
                _isWalkingRight = false;
                _position.X = MathHelper.Clamp(_position.X - _speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, windowWidth - _texture.Width);
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                _isWalkingLeft = false;
                _isWalkingRight = true;
                _position.X = MathHelper.Clamp(_position.X + _speed * (float)gameTime.ElapsedGameTime.TotalSeconds, 0, windowWidth - _texture.Width);
            }
            else
            {
                _isWalkingLeft = false;
                _isWalkingRight = false;
            }

            if (_isWalkingLeft)
            {
                _walkLeftAnimation.Update(gameTime);
            }
            else if (_isWalkingRight)
            {
                _walkRightAnimation.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isWalkingLeft)
            {
                spriteBatch.Draw(_walkLeftAnimation.GetCurrentFrame(), _position, Color.White);
            }
            else if (_isWalkingRight)
            {
                spriteBatch.Draw(_walkRightAnimation.GetCurrentFrame(), _position, Color.White);
            }
            else
            {
                spriteBatch.Draw(_texture, _position, Color.White);
            }
        }
    }
}
