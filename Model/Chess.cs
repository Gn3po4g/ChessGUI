using System.ComponentModel;

namespace ChessGUI.Model {
    public enum Type {
        ba,
        bb,
        bc,
        bk,
        bn,
        bp,
        br,
        none,
        ra,
        rb,
        rc,
        rk,
        rn,
        rp,
        rr
    }
    public class Chess(int row, int col, Type type) : INotifyPropertyChanged {
        private int _row = row, _col = col;
        private Type _type = type;
        private bool _focused = false;
        public int Row { get { return _row; } set { _row = value; } }
        public int Col { get { return _col; } set { _col = value; } }
        public Type Type { get { return _type; } set { _type = value; OnTypeChanged(nameof(Type)); } }

        public bool Focused { get { return _focused; } set { _focused = value; OnTypeChanged(nameof(Focused)); } }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnTypeChanged(string name) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
