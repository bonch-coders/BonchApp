﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using BonchApp.Droid;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using System.ComponentModel;
using Android.Graphics;

[assembly: ExportRenderer(typeof(Xamarin.Forms.Button), typeof(CustomButtonRenderer))]
namespace BonchApp.Droid
{
    public class CustomButtonRenderer : ButtonRenderer
    {
        private GradientDrawable _normal, _pressed;
        private Android.Widget.Button button;

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            button = this.Control; button.SetAllCaps(false);
            var label = (Android.Widget.Button)Control; // for example
            Typeface font = Typeface.CreateFromAsset(Forms.Context.Assets, "Fonts/RobotoLight.ttf");  // font name specified here
            label.Typeface = font;
            if (Control != null)
            {
                var button = e.NewElement;
               
                // Create a drawable for the button's normal state
                _normal = new Android.Graphics.Drawables.GradientDrawable();

                if (button.BackgroundColor.R == -1.0 && button.BackgroundColor.G == -1.0 && button.BackgroundColor.B == -1.0)
                    _normal.SetColor(Android.Graphics.Color.ParseColor("#95ffffff"));
                else 
                    _normal.SetColor(Android.Graphics.Color.ParseColor("#95ffffff"));
                _normal.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                _normal.SetCornerRadius(button.BorderRadius);

                // Create a drawable for the button's pressed state
                _pressed = new Android.Graphics.Drawables.GradientDrawable();
                var highlight = Context.ObtainStyledAttributes(new int[] { Android.Resource.Attribute.ColorActivatedHighlight }).GetColor(0, Android.Graphics.Color.Gray);
                _pressed.SetColor(Android.Graphics.Color.ParseColor("#30ffffff"));
                _pressed.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                _pressed.SetCornerRadius(button.BorderRadius);

                // Add the drawables to a state list and assign the state list to the button
                var sld = new StateListDrawable();
                sld.AddState(new int[] { Android.Resource.Attribute.StatePressed }, _pressed);
                sld.AddState(new int[] { }, _normal);
                Control.SetBackgroundDrawable(sld);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            var button = (Xamarin.Forms.Button)sender;

            if (_normal != null && _pressed != null)
            {
                if (e.PropertyName == "BorderRadius")
                {
                    _normal.SetCornerRadius(button.BorderRadius);
                    _pressed.SetCornerRadius(button.BorderRadius);
                }
                if (e.PropertyName == "BorderWidth" || e.PropertyName == "BorderColor")
                {
                    _normal.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                    _pressed.SetStroke((int)button.BorderWidth, button.BorderColor.ToAndroid());
                }
            }
        }
    }
}