using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGUI.ViewModel;

internal class Cannon(int row, int col, ChessColor color) : Chess(row, col, color)
{
    public override ImageSource ImageSource { get; set; } = new BitmapImage(
        new Uri($"pack://application:,,,/Images/{color.ToString().ToLower()}_cannon.png")
    );

    public override bool CanEat(Chess other)
    {
        return false;
    }
}