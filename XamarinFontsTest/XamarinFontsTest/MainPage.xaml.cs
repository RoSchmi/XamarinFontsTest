// Copyright RoSchmi 2018, License Apache 2.0

// This Xamarin.Forms App gives some examples how to work with Fonts in SkiaSharp
// Clicking the Button shows the name of the next font in the list,
// displayed in the fonts style

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using SkiaSharp.Views;
using SkiaSharp.Views.Forms;
using XamarinFontsTest;
using SkiaSharp;


namespace XamarinFontsTest
{
    public partial class MainPage : ContentPage
    {
        int fontsCounter = -1;
        string wantedFont = "Helvetica";

        List<string> iOSTypeList = new List<string>() { 
                                                        "Helvetica",
                                                        "Courier",
                                                        "Lobster-Regular",
                                                        "Papyrus",
                                                        "San Francisco",
                                                        "TimesNewRomanPSMT",
                                                        "Verdana",
                                                        "Thonburi",
        };

        List<string> typeList = SKFontManager.Default.FontFamilies.ToList();


        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            typeList = Device.RuntimePlatform == Device.iOS ? iOSTypeList : SKFontManager.Default.FontFamilies.ToList();
            fontsCounter = (fontsCounter == (typeList.Count - 1)) ? 0 : (fontsCounter + 1);
            wantedFont = typeList[fontsCounter];             
            canvasView.InvalidateSurface();           
        }

        private void Button_Last_Clicked(object sender, EventArgs e)
        {
            typeList = Device.RuntimePlatform == Device.iOS ? iOSTypeList : SKFontManager.Default.FontFamilies.ToList();
            fontsCounter = fontsCounter == 0 ? typeList.Count - 1 : (fontsCounter - 1);
            wantedFont = typeList[fontsCounter];
            canvasView.InvalidateSurface();
        }

        private void canvasView_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            SkiaSharp.SKRect rect = new SKRect(300, 400, 100, 100);

            SKPaint sKPaint = new SKPaint()
            { Color = SKColors.Chartreuse };           
            canvas.DrawRect(rect, sKPaint);
            canvas.DrawCircle(100, 100, 60, sKPaint);
            
            canvas.Save();

            SKPaint textPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                StrokeWidth = 2,
                FakeBoldText = true,
                TextSize = 80,
                IsAntialias = true,
                LcdRenderText = true,
                Color = SKColors.Black
            };

            textPaint.IsAntialias = true;
            textPaint.LcdRenderText = true;

            typeList = Device.RuntimePlatform == Device.iOS ? iOSTypeList : SKFontManager.Default.FontFamilies.ToList();

            SkiaSharp.SKTypeface standardFont = SKFontManager.Default.MatchCharacter('a');           

            string typeFaceName = typeList.Find(X => X == wantedFont);

            SkiaSharp.SKTypeface selectedFont = typeFaceName != null ? SKTypeface.FromFamilyName(typeFaceName, SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright) : standardFont;

            textPaint.Typeface = selectedFont;

            string text = standardFont.FamilyName;
            text = typeFaceName != null ? typeFaceName : standardFont.FamilyName;

            float textWidth = textPaint.MeasureText(text);   // only for demonstration

            canvas.DrawText(text, 400, (float)(300), textPaint);

            textPaint.IsAntialias = false;

            canvas.DrawText(text, 400, (float)(400), textPaint);

            canvas.Restore();         
        }     
    }
}
