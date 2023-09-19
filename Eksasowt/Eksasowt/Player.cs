using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
/*
 *Auteurs : Pedro Carneiro & Achraf Zamader
 *Classe : IFDAP4C
 *Date :
 *Fichier : Player.cs
 */
namespace Eksasowt
{
    public class Player
    {
        // Déclaration des champs pour les caractéristiques du joueur
        public Texture2D _texture; // Texture du joueur
        public Vector2 _position; // Position du joueur sur l'écran
        private int _speed = 200; // Vitesse de déplacement horizontale
        private float _jumpSpeed = 500; // Vitesse du saut normal
        private float _superJumpSpeed = 700; // Vitesse du super saut
        private float _gravity = 1000; // Force de gravité
        private bool _isJumping; // Indique si le joueur est en train de sauter
        private bool _isSuperJumping; // Indique si le joueur est en train de faire un super saut
        private float _superJumpTime = 0.5f; // Durée du super saut
        private float _jumpTimer; // Compteur de temps de saut
        private float _verticalVelocity = 0; // Vitesse verticale du joueur

        // Liste des textures pour l'animation de saut
        private List<Texture2D> _jumpFrames;
        private bool _isAnimatingJump; // Indique si l'animation de saut est en cours
        private int _currentJumpFrame; // Index de la frame d'animation de saut en cours
        private float _jumpFrameTimer;
        private float _jumpFrameInterval = 0.1f; // Intervalle entre chaque image d'animation de saut

        // Animations de marche à gauche et à droite
        private PlayerAnimation _walkLeftAnimation;
        private PlayerAnimation _walkRightAnimation;
        private bool _isWalkingLeft; // Indique si le joueur se déplace vers la gauche
        private bool _isWalkingRight; // Indique si le joueur se déplace vers la droite

        // Constructeur
        public Player(Texture2D texture, List<Texture2D> jumpFrames, int screenWidth, int screenHeight, List<Texture2D> walkLeftFrames, List<Texture2D> walkRightFrames)
        {
            // Initialisations
            _texture = texture;
            _jumpFrames = jumpFrames;
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
                    _isAnimatingJump = true; // Commencez l'animation de saut
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

            // Arrêtez l'animation de saut lorsque le saut est terminé
            if (!_isJumping)
            {
                _isAnimatingJump = false;
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

            // Mise à jour des animations de marche
            if (_isWalkingLeft)
            {
                _walkLeftAnimation.Update(gameTime);
            }
            else if (_isWalkingRight)
            {
                _walkRightAnimation.Update(gameTime);
            }

            // Mise à jour de l'animation de saut
            if (_isAnimatingJump)
            {
                _jumpFrameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (_jumpFrameTimer >= _jumpFrameInterval)
                {
                    _currentJumpFrame = (_currentJumpFrame + 1) % _jumpFrames.Count;
                    _jumpFrameTimer = 0f;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_isAnimatingJump)
            {
                // Dessinez l'animation de saut si elle est active
                spriteBatch.Draw(_jumpFrames[_currentJumpFrame], _position, Color.White);
            }
            else if (_isWalkingLeft)
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
