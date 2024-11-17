//==============================================================[START OF FILE]==============================================================
//DBM ST10132589 ô¿ô
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace MunicipalServicesApp.Helpers
{
    //==============================================================[START OF CLASS]============================================================== 
    /// <summary>
    /// All of the methods to make the progress bar work in the report issues pages
    //HAD TO UPDATE FOR PART THREE TO INCLUDE PRIORITY FOR USE OF HEAPS
    /// </summary>
    public class ProgressBarHelper
    {
        private ProgressBar _progressBar;

        public ProgressBarHelper(ProgressBar progressBar)
        {
            _progressBar = progressBar;
        }


        /// <summary>
        /// Changed from 25 -> 20 to make the progress bar accurate for 5 variables
        /// </summary>
        public void IncreaseProgBar()
        {
            AnimateProgressBar(_progressBar.Value + 20);
        }

        public void DecreaseProgBar()
        {
            AnimateProgressBar(_progressBar.Value - 20);
        }
        /// <summary>
        /// Animates the progress bar to the value passed to it.
        /// </summary>
        public void AnimateProgressBar(double toValue)
        {
            DoubleAnimation animation = new DoubleAnimation
            {
                From = _progressBar.Value,
                To = toValue,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            _progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }

        /// <summary>
        /// Validates the input of the control passed to it.
        /// </summary>
        public void ValidateInput(Control control, Func<bool> getIsValid, Action<bool> setIsValid, string text = null)
        {
            bool isEmpty = string.IsNullOrEmpty(text ?? (control as TextBox)?.Text);
            control.BorderBrush = isEmpty ? Brushes.Red : (Brush)(new BrushConverter().ConvertFrom("#FFABADB3"));

            if (isEmpty && getIsValid())
            {
                DecreaseProgBar();
                setIsValid(false);
            }
            else if (!isEmpty && !getIsValid())
            {
                IncreaseProgBar();
                setIsValid(true);
            }
        }
    }
    //==============================================================[END OF CLASS]============================================================== 
}
//==============================================================[END OF FILE]==============================================================