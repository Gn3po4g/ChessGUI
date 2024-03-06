namespace ChessGUI.Model {
    public class Board {
        public static int Row { get; } = 10;
        public static int Column { get; } = 9;
        public Chess[,] TheBoard { get; } = new Chess[Row, Column];

        private Chess? _focused = null, _start = null, _end = null;
        public Chess? Focused {
            get { return _focused; }
            set {
                if (_focused != null) { _focused.Focused = false; }
                _focused = value;
                if (_focused != null) { _focused.Focused = true; }
            }
        }
        public Chess? Start { 
            get { return _start; } 
            set {
                if (_start != null) { _start.Focused = false; }
                _start = value;
                if (_start != null) { _start.Focused = true; }
            } 
        }
        public Chess? End {
            get { return _end; }
            set {
                if (_end != null) { _end.Focused = false; }
                _end = value;
                if (_end != null) { _end.Focused = true; }
            }
        }

        public Board() {
            for (int i = 0; i < Row; i++) {
                for (int j = 0; j < Column; j++) {
                    TheBoard[i, j] = new Chess(i, j, Type.none);
                }
            }
        }

        public IEnumerable<Chess> ChessBoard {
            get { return TheBoard.Cast<Chess>(); }
        }

        public void ResetBoard() {
            for (int i = 0; i < Row; i++) {
                for (int j = 0; j < Column; j++) {
                    TheBoard[i, j].Type = DefaultBoard[i, j];
                }
            }
        }

        public void ClearBoard() {
            for (int i = 0; i < Row; i++) {
                for (int j = 0; j < Column; j++) {
                    TheBoard[i, j].Type = Type.none;
                }
            }
        }

        public void Click(int row, int col) {
            Chess clicked = TheBoard[row, col];
            if (Focused == null) {
                if (clicked.Type != Type.none) {
                    Focused = clicked;

                }
            } else {
                Move(Focused, clicked);
                Focused = null;
                Start = Focused;
                End = clicked;
            }
            //TheBoard[row, col].Focused ^= true;
        }

        public void Move(Chess from, Chess to) {
            to.Type = from.Type;
            from.Type = Type.none;
        }

        public Type[,] DefaultBoard { get; set; } = new Type[,]{
            {Type.br,Type.bn,Type.bb,Type.ba,Type.bk,Type.ba,Type.bb,Type.bn,Type.br },
            {Type.none ,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none},
            {Type.none ,Type.bc,Type.none,Type.none,Type.none,Type.none,Type.none,Type.bc,Type.none},
            {Type.bp ,Type.none,Type.bp,Type.none,Type.bp,Type.none,Type.bp,Type.none,Type.bp},
            {Type.none ,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none},
            {Type.none ,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none},
            {Type.rp ,Type.none,Type.rp,Type.none,Type.rp,Type.none,Type.rp,Type.none,Type.rp},
            {Type.none ,Type.rc,Type.none,Type.none,Type.none,Type.none,Type.none,Type.rc,Type.none},
            {Type.none ,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none,Type.none},
            {Type.rr,Type.rn,Type.rb,Type.ra,Type.rk,Type.ra,Type.rb,Type.rn,Type.rr },
        };
    }
}
