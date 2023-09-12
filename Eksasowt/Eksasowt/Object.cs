using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Eksasowt
{
    public class Object
    {
        public Vector2 _position { get; set; }
        public Texture2D _texture { get; set; }
        public string _type { get; set; }

        public Object(Vector2 position, Texture2D texture, string type)
        {
            _position = position;
            _texture = texture;
            _type = type;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
        public bool CheckCollision(Vector2 playerPosition, Texture2D playerTexture)
        {
            // Créez des rectangles englobants pour l'objet et le joueur
            Rectangle objetRectangle = new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height);
            Rectangle playerRectangle = new Rectangle((int)playerPosition.X, (int)playerPosition.Y, playerTexture.Width, playerTexture.Height);

            // Créez un tableau de pixels pour l'objet et le joueur
            Color[] objetPixels = new Color[_texture.Width * _texture.Height];
            _texture.GetData(objetPixels);

            Color[] playerPixels = new Color[playerTexture.Width * playerTexture.Height];
            playerTexture.GetData(playerPixels);

            // Déterminez les coordonnées relatives de l'objet par rapport au joueur
            int relativeX = (int)(_position.X - playerPosition.X);
            int relativeY = (int)(_position.Y - playerPosition.Y);

            // Parcourez les pixels de l'objet et vérifiez s'ils se chevauchent avec les pixels du joueur
            for (int x = 0; x < _texture.Width; x++)
            {
                for (int y = 0; y < _texture.Height; y++)
                {
                    int playerIndex = (y + relativeY) * playerTexture.Width + (x + relativeX);
                    int objetIndex = y * _texture.Width + x;

                    // Vérifiez la collision des pixels
                    if (playerRectangle.Contains(x + relativeX, y + relativeY) &&
                        playerPixels[playerIndex].A > 0 &&
                        objetPixels[objetIndex].A > 0)
                    {
                        // Collision détectée
                        return true;
                    }
                }
            }

            // Pas de collision
            return false;
        }
    }
}