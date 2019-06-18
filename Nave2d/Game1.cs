using Esfera3D.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nave2d.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Nave2d
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D naveSprite, background;
        Rectangle Nave;
        Vector2 cenarioVelocidade, cenario;
        Point velocidade;
        List<Bola> tiros;
        List<Enemy> enemies;
        int locker;
        bool pewPewLocker;
        int pewpewdelay;
        int size = 100;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
        }

        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            naveSprite = Content.Load<Texture2D>("Images\\nave-2d");
            background = Content.Load<Texture2D>("Images\\cenario1");
            Nave = new Rectangle(new Point((graphics.PreferredBackBufferWidth - size) / 2, graphics.PreferredBackBufferHeight - size - 50), new Point(size));
            velocidade = new Point(2, 2);
            tiros = new List<Bola>();
            enemies = new List<Enemy>();
            locker = 0;
            pewPewLocker = false;
            pewpewdelay = 20;
            cenarioVelocidade = new Vector2((float)0.2, (float)0.2);
            cenario = new Vector2(0,0);
            enemies.Add(new Enemy(naveSprite, Color.Red, new Rectangle(100, 100, size, size), new Point(5, 0)));
        }


        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            var kbs = Keyboard.GetState();

            bool rightArrow = false, leftArrow = false, upArrow = false, downArrow = false, excluding = true;
            bool bla = true;

            rightArrow = Keyboard.GetState().IsKeyDown(Keys.Right);
            leftArrow = Keyboard.GetState().IsKeyDown(Keys.Left);
            upArrow = Keyboard.GetState().IsKeyDown(Keys.Up);
            downArrow = Keyboard.GetState().IsKeyDown(Keys.Down);

            if (rightArrow ^ leftArrow)
            {
                Nave.X += velocidade.X * (leftArrow ? -1 : 1);
                cenario.X += Convert.ToInt32(cenarioVelocidade.X * (leftArrow ? 1 : -1));
            }
            if (upArrow ^ downArrow)
                Nave.Y += velocidade.Y * (upArrow ? -1 : 1);

            locker++;
            if (locker >= pewpewdelay)
            {
                locker = 0;
                pewPewLocker = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && pewPewLocker == true)
            {
                tiros.Add(new Bola(new Rectangle(new Point(Nave.Location.X + Nave.Size.X/2, Nave.Location.Y), new Point(10, 10)), Content.Load<Texture2D>("Images\\esfera3d"), Color.White, new Point(0, -5)));
                pewPewLocker = false;
            }

            while (excluding)
            {
                excluding = false;
                foreach (var t in tiros)
                {
                    t.Posicao = new Rectangle(t.Posicao.Location + t.Velocidade, t.Posicao.Size);
                    if(t.Posicao.Y < 0)
                    {
                        tiros.Remove(t);
                        excluding = true;
                        break;
                    }
                    foreach(var e in enemies)
                    {
                        if (t.Posicao.Y <= e.posicao.Y + e.posicao.Height && t.Posicao.Y >= e.posicao.Y &&
                           t.Posicao.X <= e.posicao.X + e.posicao.Width && t.Posicao.X >= e.posicao.X)
                        {
                            tiros.Remove(t);
                            enemies.Remove(e);
                            excluding = true;
                            break;
                        }
                    }

                }
            }     
            
            foreach(var e in enemies)
            {
                if (e.posicao.X >= graphics.PreferredBackBufferWidth - e.posicao.Width || e.posicao.X <=0)
                {
                    e.speed = new Point(e.speed.X * -1, e.speed.Y);
                    e.posicao = new Rectangle(e.posicao.X, e.posicao.Y + 5, e.posicao.Width, e.posicao.Height);
                }
                e.posicao = new Rectangle(e.posicao.X + e.speed.X, e.posicao.Y + e.speed.Y, size, size);
                Debug.WriteLine(enemies[0].speed.ToString());
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            spriteBatch.Draw(background, cenario, Color.Transparent);
            spriteBatch.Draw(naveSprite, Nave, Color.White);
            foreach(var t in tiros)
            {
                spriteBatch.Draw(t.Aparencia, t.Posicao, t.Cor);
            }
            foreach(var e in enemies)
            {
                spriteBatch.Draw(e.aparencia, e.posicao, e.cor);
            }
            spriteBatch.End();
            base.Draw(gameTime);

            Debug.Print(tiros.Count.ToString());
        }
    }
}
