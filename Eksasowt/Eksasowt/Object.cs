using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
/*
 *Auteurs : Pedro Carneiro & Achraf Zamader
 *Classe : IFDAP4C
 *Date :
 *Fichier : Object.cs
 */
namespace Eksasowt
{
    public class Object
    {
        // Propriétés 
        public Vector2 _position { get; set; } 
        public Texture2D _texture { get; set; }
        // Calcule la largeur de l'objet
        public int Width => _texture.Width;
        // Calcule la hauteur de l'objet
        public int Height => _texture.Height; 

        // Constructeur
        public Object(Vector2 position, Texture2D texture)
        {
            // Initialisations
            _position = position;
            _texture = texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Dessine l'objet à sa position avec sa texture en utilisant la couleur blanche
            spriteBatch.Draw(_texture, _position, Color.White); 
        }
    }
}
