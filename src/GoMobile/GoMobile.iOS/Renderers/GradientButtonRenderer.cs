﻿using CoreAnimation;
using CoreGraphics;
using GoMobile.Controls;
using GoMobile.iOS.Renderers;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(GradientButton), typeof(GradientButtonRenderer))]
namespace GoMobile.iOS.Renderers
{
    public class GradientButtonRenderer : ButtonRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == "StartGradientColor" || e.PropertyName == "EndGradientColor")
            {
                var model = Element as GradientButton;
                if (model != null)
                {
                    SetButtonBackground(Control, model.StartGradientColor, model.EndGradientColor);
                }
            }
        }

        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            var model = Element as GradientButton;
            if (model != null)
            {
                SetButtonBackground(Control, model.StartGradientColor, model.EndGradientColor);
            }
        }

        private void SetButtonBackground(UIButton button, Color startColor, Color endColor)
        {
            var startGradient = startColor.ToCGColor();
            var endGradient = endColor.ToCGColor();

            var gradientLayer = new CAGradientLayer
            {
                Frame = button.Bounds,
                Colors = new[] { endGradient, startGradient },
                BorderWidth = 0
            };

            button.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
