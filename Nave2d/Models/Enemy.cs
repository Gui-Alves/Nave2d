using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nave2d.Models
{
    public class Enemy
    {
        public Texture2D aparencia { get; set; }
        public Color cor { get; set; }
        public Rectangle posicao { get; set; }
        public Point speed { get; set; }

        public Enemy(Texture2D aparencia, Color cor, Rectangle posicao, Point speed)
        {
            this.aparencia = aparencia;
            this.cor = cor;
            this.posicao = posicao;
            this.speed = speed;
        }
          

    }
}
