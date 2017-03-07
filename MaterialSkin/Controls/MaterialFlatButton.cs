using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialSkin.Animations;

namespace MaterialSkin.Controls
{
    public class MaterialFlatButton : Button, IMaterialControl
    {
        private bool autoUpper = true;
        [Category("Appearance")]
        public bool AutoUpper
        {
            get { return autoUpper; }
            set { autoUpper = value; }
        }

        private bool useCustomBackColor = false;
        [Category("Appearance")]
        public bool UseCustomBackColor
        {
            get { return useCustomBackColor; }
            set { useCustomBackColor = value; }
        }


        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MaterialSkinManager SkinManager { get { return MaterialSkinManager.Instance; } }
        [Browsable(false)]
        public MouseState MouseState { get; set; }
        public bool Primary { get; set; }
        
        private readonly AnimationManager animationManager;
        private readonly AnimationManager hoverAnimationManager;

        private Size _iconsize = new Size(25,25);
        public Size IconSize
        {
            get { return _iconsize; }
            set { if (!(value==null)) _iconsize = value; }
        }

        private SizeF textSize;

        private Image _icon;
        public Image Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        public MaterialFlatButton()
        {
            Primary = false;
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            animationManager = new AnimationManager(false)
            {
                Increment = 0.03,
                AnimationType = AnimationType.EaseOut
            };
            hoverAnimationManager = new AnimationManager
            {
                Increment = 0.07,
                AnimationType = AnimationType.Linear
            };

            hoverAnimationManager.OnAnimationProgress += sender => Invalidate();
            animationManager.OnAnimationProgress += sender => Invalidate();

            //UNDONE : Remove this function and use Design view Properties
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            //AutoSize = true;
            //Margin = new Padding(4, 6, 4, 6);
            //Padding = new Padding(0);
        }

        public override string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                if (autoUpper) value.ToUpper();
                textSize = CreateGraphics().MeasureString(value, SkinManager.ROBOTO_MEDIUM_10);
                if (AutoSize)
                    Size = GetPreferredSize();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (useCustomBackColor)
            {
                g.Clear(BackColor);
            }
            else g.Clear(Parent.BackColor);

            //Hover
            if (useCustomBackColor)
            {
                Color c = SkinManager.Theme == MaterialSkinManager.Themes.LIGHT ?
                      Color.FromArgb(30.PercentageToColorComponent(), SkinManager.GetFlatButtonHoverBackgroundColor())
                    : Color.FromArgb(10.PercentageToColorComponent(), SkinManager.GetFlatButtonHoverBackgroundColor());
                using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * c.A), c.RemoveAlpha())))
                    g.FillRectangle(b, ClientRectangle);
            }
            else
            {
                Color cc = SkinManager.Theme == MaterialSkinManager.Themes.LIGHT ?
                      Color.FromArgb(46.PercentageToColorComponent(), SkinManager.GetFlatButtonHoverBackgroundColor()) 
                    : Color.FromArgb(7.PercentageToColorComponent(), SkinManager.GetFlatButtonHoverBackgroundColor());
                using (Brush b = new SolidBrush(Color.FromArgb((int)(hoverAnimationManager.GetProgress() * cc.A), cc.RemoveAlpha())))
                    g.FillRectangle(b, ClientRectangle);
            }

            //Ripple
            if (animationManager.IsAnimating())
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                for (int i = 0; i < animationManager.GetAnimationCount(); i++)
                {
                    var animationValue = animationManager.GetProgress(i);
                    var animationSource = animationManager.GetSource(i);

                    using (Brush rippleBrush = new SolidBrush(Color.FromArgb((int)(101 - (animationValue * 100)), Color.Black)))
                    {
                        var rippleSize = (int)(animationValue * Width * 2);
                        g.FillEllipse(rippleBrush, new Rectangle(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize));
                    }
                }
                g.SmoothingMode = SmoothingMode.None;
            }
            
            //Icon
            Rectangle iconRect = new Rectangle(4, 5, 24, 24);

            if (String.IsNullOrEmpty(Text))
                // Center Icon
                iconRect.X += 2;

            if (!AutoSize)
            {
                iconRect.Width = _iconsize.Width;
                iconRect.Height = _iconsize.Height;
            }

            if (Icon != null)
                g.DrawImage(Icon, iconRect);

            //Text
            Rectangle textRect = ClientRectangle;

            if (Icon != null)
            {
                //
                // Resize and move Text container
                //

                // First 4: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                // Third 4: right padding
                if (AutoSize) textRect.Width -= 4 + 24 + 4 + 4;
                else textRect.Width -= 4 + 24 + 4 + 4;

                // First 4: left padding
                // 24: icon width
                // Second 4: space between Icon and Text
                if (AutoSize) textRect.X += 4 + 24 + 4;
                else textRect.X += 4 + _iconsize.Width + 4;
            }

            string temp = autoUpper ? Text.ToUpper() : Text;
            g.DrawString(
                temp,
                SkinManager.ROBOTO_MEDIUM_10,
                Enabled ? (Primary ? SkinManager.ColorScheme.PrimaryBrush : SkinManager.GetPrimaryTextBrush()) : SkinManager.GetFlatButtonDisabledTextBrush(),
                textRect,
                new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center }
                );
        }

        private Size GetPreferredSize()
        {
            return GetPreferredSize(new Size(0, 0));
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            // Provides extra space for proper padding for content
            int extra = 8;

            if (Icon != null)
                // 24 is for icon size
                // 4 is for the space between icon & text
                extra += 24 + 4;

            return new Size((int)Math.Ceiling(textSize.Width) + extra, 36);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode) return;

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.In);
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                hoverAnimationManager.StartNewAnimation(AnimationDirection.Out);
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;

                    animationManager.StartNewAnimation(AnimationDirection.In, args.Location);
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;

                Invalidate();
            };
        }
    }
}
