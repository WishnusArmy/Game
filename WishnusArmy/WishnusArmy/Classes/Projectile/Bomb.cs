﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ContentImporter.Sprites;
using static ContentImporter.Sounds;

    class Bomb : BaseProjectile
    {
        public Bomb(double damage, float speed) : base(damage, speed)
        {
        this.scale = new Vector2(0.5F, 0.5F);
        this.explosionRadius = 200 + (int)damage/2; 
        sprite = SPR_GRENADE;
        this.damage = damage;
        this.speed = speed;
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            //base.HandleInput(inputHelper);
            if (!shooting)
            {
                Enemy enemy = findTarget();
            if (enemy == null)
                return;

                mousePos = enemy.Position;
                shooting = true;
                distance = CalculateDistance(Position, targetPos);
            }
        targetPos = mousePos;

            double opposite = targetPos.Y - Position.Y;
            double adjacent = targetPos.X - Position.X;
            rotation = (float)Math.Atan2(opposite, adjacent);
            float x = (float)Math.Cos(rotation) * speed;
            float y = (float)Math.Sin(rotation) * speed;
            velocity = new Vector2(x, y);
        }
        public override void Update(object gameTime)
        {
        base.Update(gameTime);
        if (shooting && CalculateDistance(Position, targetPos) < 15)
        {
            Explode();
            PlaySound(SND_EXPLOSION);
        }
    }

        public virtual Enemy findTarget()
        {
            List<Enemy> enemies = ObjectLists.Enemies;
            enemies = enemies.OrderBy(o => o.CalculateDistance(o.GlobalPositionCenter, GlobalPosition)).ToList();
            if (enemies.Count > 0)
                return enemies[0];
            return null;
        }
    }
