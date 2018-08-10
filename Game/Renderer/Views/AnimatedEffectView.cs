﻿using ClassicUO.Game.GameObjects;
using Microsoft.Xna.Framework;

namespace ClassicUO.Game.Renderer.Views
{
    public class AnimatedEffectView : View
    {
        private Graphic _displayedGraphic = Graphic.Invalid;

        public AnimatedEffectView(in AnimatedItemEffect effect) : base(effect)
        {
        }

        public AnimatedItemEffect WorldObject => (AnimatedItemEffect) GameObject;


        public override bool Draw(in SpriteBatch3D spriteBatch, in Vector3 position)
        {
            return !PreDraw(position) && DrawInternal(spriteBatch, position);
        }

        public override bool DrawInternal(in SpriteBatch3D spriteBatch, in Vector3 position)
        {
            if (WorldObject.AnimationGraphic != _displayedGraphic)
            {
                _displayedGraphic = WorldObject.AnimationGraphic;
                Texture = TextureManager.GetOrCreateStaticTexture(WorldObject.AnimationGraphic);
                Bounds = new Rectangle(Texture.Width / 2 - 22, Texture.Height - 44 + WorldObject.Position.Z * 4, Texture.Width, Texture.Height);
            }

            HueVector = RenderExtentions.GetHueVector(WorldObject.Hue);

            return base.Draw(in spriteBatch, in position);
        }

        public override void Update(in double frameMS)
        {
            base.Update(frameMS);

            if (!WorldObject.IsDisposed)
                WorldObject.UpdateAnimation(frameMS);
        }
    }
}