using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Esfera3D.Classes
{
    public class Bola
    {
        public Rectangle Posicao { get; set; }
        public Texture2D Aparencia { get; set; }
        public Color Cor { get; set; }
        public Point Velocidade { get; set; }

        public Bola()
        {
        }

        public void movimentar(int x, int y)
        {

        }
        public Bola(Rectangle posicao, Texture2D aparencia, Color cor, Point velocidade)
        {
            Posicao = posicao;
            Aparencia = aparencia;
            Cor = cor;
            this.Velocidade = velocidade;
        }
    }
}
