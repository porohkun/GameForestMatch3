using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3.Core;
using GameForestMatch3.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Bonus = System.Collections.Generic.KeyValuePair<Microsoft.Xna.Framework.Point, GameForestMatch3.ChipColor>;

namespace GameForestMatch3
{
    public class GameField : GameObject<GameObject>
    {
        private Vector2 _position;
        private Vector2 _step { get; }
        public int Width { get; }
        public int Height { get; }
        public Cell[][] Cells { get; }
        public Dispencer[] Dispensers { get; }
        public int WaitingCells { get; private set; }
        public bool Ready => WaitingCells == 0;
        public int DestroyingChips { get; private set; }
        public int AppearingChips { get; private set; }

        public GameField(RenderCache renderCache, int width, int height, Rectf rect) : base(renderCache)
        {
            Width = width;
            Height = height;
            _position = rect.Position;
            _step = new Vector2(rect.Width / width, rect.Height / height);
            Cells = new Cell[Width][];
            Dispensers = new Dispencer[Width];
            WaitingCells = Width * Height;

            for (int x = 0; x < width; x++)
            {
                Cells[x] = new Cell[Height];
                Dispensers[x] = AddComponent(new Dispencer(renderCache, x, Cells[x], GetPosition(x, -1), _step));
                Dispensers[x].ChipArrived += GameField_ChipArrived;
                for (int y = 0; y < height; y++)
                {
                    AddComponent(new Sprite9SliceRenderer(renderCache, Resources.Get<Texture2D>("field"))
                    {
                        SortingLayer = SortingLayer.GetLayer("field"),
                        Position = GetPosition(x, y),
                        Size = _step,
                        CenterRect = new Rectf(32, 32, 64, 64),
                        Color = new Color(Color.White, 0.7f)
                    });

                    Cells[x][y] = new Cell(GetPosition(x, y));
                }
                //FillColumn(x);
                for (int y = 0; y < height; y++)
                    if ((x == 0 && y >= 4) || (x < 4 && y == 7))
                        Dispensers[x].Dispence(new Chip(this, RenderCache, ChipColor.Red));
                    else
                        Dispensers[x].Dispence(new Chip(this, RenderCache));
            }
        }

        private ButtonState prev = ButtonState.Released;
        protected override void OnUpdate(GameTime gameTime)
        {
            var state = Mouse.GetState().LeftButton;
            if (state == ButtonState.Pressed && prev == ButtonState.Released)
            {
                var pos = GetCellPos(Mouse.GetState().Position);
                if (CheckInBorder(pos))
                {
                    var chip = Cells[pos.X][pos.Y].CurrentChip;
                    var c = (int)chip.Bonus;
                    c++;
                    if (c == 4)
                        c = 0;
                    ShowNewChip(new Chip(this, RenderCache, chip.Color, (ChipBonus) c), pos);
                    AppearingChips--;
                    Remove(chip.Renderer);
                }
            }
            prev = state;
        }

        private void GameField_ChipArrived(int x, int y)
        {
            WaitingCells--;

            if (Ready)
                CheckCombos();
        }

        private void CheckCombos(Point? expect1 = null, Point? expect2 = null)
        {
            var exp1 = expect1 ?? new Point(-1, -1);
            var exp2 = expect2 ?? new Point(-1, -1);
            var horizontalLines = new List<HorizontalLine>();
            var verticalLines = new List<HorizontalLine>();
            for (int y = 0; y < Height; y++)
                horizontalLines.AddRange(GetHorizontalLines(y));
            for (int x = 0; x < Width; x++)
                verticalLines.AddRange(GetVerticalLines(x));

            if (horizontalLines.Count + verticalLines.Count == 0)
            {
                Coroutine.Start(this, ListenFirstClick());
                return;
            }

            var horForDel = new List<HorizontalLine>();
            var verForDel = new List<HorizontalLine>();

            var destroy = new List<Point>();
            var bombs = new List<Bonus>();
            var horizontal = new List<Bonus>();
            var vertical = new List<Bonus>();
            foreach (var hor in horizontalLines)
                foreach (var ver in verticalLines)
                    if (Cross(hor, ver))
                    {
                        var bomb = new Point(ver.Position.X, hor.Position.Y);
                        bombs.Add(new Bonus(bomb, Cells[bomb.X][bomb.Y].CurrentChip.Color));
                        horForDel.Add(hor);
                        verForDel.Add(ver);
                        for (int x = hor.Position.X; x < hor.Position.X + hor.Length; x++)
                            destroy.Add(new Point(x, hor.Position.Y));
                        for (int y = ver.Position.Y; y < ver.Position.Y + ver.Length; y++)
                            destroy.Add(new Point(ver.Position.X, y));
                    }
            foreach (var hor in horForDel)
                horizontalLines.Remove(hor);
            foreach (var ver in verForDel)
                verticalLines.Remove(ver);

            foreach (var hor in horizontalLines)
            {
                for (int x = hor.Position.X; x < hor.Position.X + hor.Length; x++)
                    destroy.Add(new Point(x, hor.Position.Y));
                if (hor.Length == 4)
                {
                    var hhh = new Point(hor.Position.X + 1, hor.Position.Y);
                    if (exp1.Y == hor.Position.Y && exp1.X >= hor.Position.X && exp1.X < hor.Position.X + hor.Length)
                        hhh = exp1;
                    if (exp2.Y == hor.Position.Y && exp2.X >= hor.Position.X && exp2.X < hor.Position.X + hor.Length)
                        hhh = exp2;
                    horizontal.Add(new Bonus(hhh, Cells[hhh.X][hhh.Y].CurrentChip.Color));
                }
                if (hor.Length >= 5)
                {
                    var bomb = new Point(hor.Position.X + 2, hor.Position.Y);
                    if (exp1.Y == hor.Position.Y && exp1.X >= hor.Position.X && exp1.X < hor.Position.X + hor.Length)
                        bomb = exp1;
                    if (exp2.Y == hor.Position.Y && exp2.X >= hor.Position.X && exp2.X < hor.Position.X + hor.Length)
                        bomb = exp2;
                    bombs.Add(new Bonus(bomb, Cells[bomb.X][bomb.Y].CurrentChip.Color));
                }
            }

            foreach (var ver in verticalLines)
            {
                for (int y = ver.Position.Y; y < ver.Position.Y + ver.Length; y++)
                    destroy.Add(new Point(ver.Position.X, y));
                if (ver.Length == 4)
                {
                    var vvv = new Point(ver.Position.X, ver.Position.Y + 2);
                    if (exp1.X == ver.Position.X && exp1.Y >= ver.Position.Y && exp1.Y < ver.Position.Y + ver.Length)
                        vvv = exp1;
                    if (exp2.X == ver.Position.X && exp2.Y >= ver.Position.Y && exp2.Y < ver.Position.Y + ver.Length)
                        vvv = exp2;
                    vertical.Add(new Bonus(vvv, Cells[vvv.X][vvv.Y].CurrentChip.Color));
                }
                if (ver.Length >= 5)
                {
                    var bomb = new Point(ver.Position.X, ver.Position.Y + 2);
                    if (exp1.X == ver.Position.X && exp1.Y >= ver.Position.Y && exp1.Y < ver.Position.Y + ver.Length)
                        bomb = exp1;
                    if (exp2.X == ver.Position.X && exp2.Y >= ver.Position.Y && exp2.Y < ver.Position.Y + ver.Length)
                        bomb = exp2;
                    bombs.Add(new Bonus(bomb, Cells[bomb.X][bomb.Y].CurrentChip.Color));
                }
            }

            foreach (var point in destroy.Distinct())
                DestroyChip(point);

            CreateBonuses(bombs, horizontal, vertical);
            return;

            destroy = new List<Point>(destroy.Distinct());

            var newDestroy = new List<Point>();
            foreach (var point in destroy)
            {
                if (Cells[point.X][point.Y].CurrentChip.Bonus == ChipBonus.Bomb)
                {

                }
            }

            var max = new int[Width];
            var news = new int[Width];
            for (int x = 0; x < Width; x++)
                max[x] = -1;
            foreach (var point in destroy)
            {
                max[point.X] = Math.Max(max[point.X], point.Y);
                news[point.X]++;
            }
            foreach (var point in bombs.Union(horizontal).Union(vertical))
                news[point.Key.X]--;
            for (int x = 0; x < Width; x++)
                for (int y = 0; y <= max[x]; y++)
                    Cells[x][y].Ready = false;
            var destroyRenderers = destroy.Select(p => Cells[p.X][p.Y].CurrentChip.Renderer).ToArray<Renderer>();
            foreach (var point in destroy)
                Cells[point.X][point.Y].CurrentChip = null;
            new DisappearEffect(destroyRenderers).Play(() =>
            {
                foreach (var rend in destroyRenderers)
                    Remove(rend);
            });
            List<Renderer> appears = CreateBonuses(bombs, horizontal, vertical);

            new AppearEffect(appears.ToArray()).Play(() =>
            {
                for (int x = 0; x < Width; x++)
                {
                    for (int y = max[x]; y >= 0; y--)
                        if (!Cells[x][y].Empty)
                        {
                            WaitingCells++;
                            Dispensers[x].FallChip(Cells[x][y].CurrentChip, y);
                        }
                    for (int i = 0; i < news[x]; i++)
                    {
                        WaitingCells++;
                        Dispensers[x].Dispence(new Chip(this, RenderCache));
                    }
                }
            });
        }

        private List<Renderer> CreateBonuses(List<Bonus> bombs, List<Bonus> horizontal, List<Bonus> vertical)
        {
            var appears = new List<Renderer>();
            foreach (var b in bombs)
            {
                var chip = new Chip(this, RenderCache, b.Value, ChipBonus.Bomb);
                appears.Add(chip.Renderer);
                ShowNewChip(chip, b.Key);
            }
            foreach (var b in horizontal)
            {
                var chip = new Chip(this, RenderCache, b.Value, ChipBonus.HorLine);
                appears.Add(chip.Renderer);
                ShowNewChip(chip, b.Key);
            }
            foreach (var b in vertical)
            {
                var chip = new Chip(this, RenderCache, b.Value, ChipBonus.VerLine);
                appears.Add(chip.Renderer);
                ShowNewChip(chip, b.Key);
            }

            if (appears.Count > 0)
                new AppearEffect(appears.ToArray()).Play(() =>
                {
                    OnAppear(appears.Count);
                });

            return appears;
        }

        private void ShowNewChip(Chip chip, Point pos)
        {
            chip.Renderer.Color = Color.White;
            chip.Renderer.Size = _step;
            chip.Renderer.Position = GetPosition(pos.X, pos.Y);
            Cells[pos.X][pos.Y].CurrentChip = chip;
            AppearingChips++;
        }

        private void DestroyChip(Point point)
        {
            var cell = Cells[point.X][point.Y];
            var chip = cell.CurrentChip;
            if (chip != null)
            {
                DestroyingChips++;
                switch (chip.Bonus)
                {
                    case ChipBonus.HorLine:
                    case ChipBonus.VerLine:
                    case ChipBonus.None:
                        {
                            cell.CurrentChip = null;
                            new DisappearEffect(chip.Renderer).Play(() =>
                            {
                                Remove(chip.Renderer);
                                OnDestroy();
                            });
                            break;
                        }
                    case ChipBonus.Bomb:
                        {
                            cell.CurrentChip = null;
                            Coroutine.Wait(0.25f, () =>
                            {
                                var explosion = AddComponent(new SpriteRenderer(RenderCache, "explosion")
                                {
                                    SortingLayer = SortingLayer.GetLayer("effects"),
                                    Position = GetPosition(point.X, point.Y),
                                    Size = _step / 2f,
                                });
                                new ExplodeEffect(explosion).Play(() => Remove(explosion));
                                new DisappearEffect(chip.Renderer).Play(() =>
                                {
                                    Remove(chip.Renderer);
                                    OnDestroy();
                                });
                                for (int x = point.X - 1; x <= point.X + 1; x++)
                                    for (int y = point.Y - 1; y <= point.Y + 1; y++)
                                        if (CheckInBorder(new Point(x, y)))
                                            DestroyChip(new Point(x, y));
                            });
                            break;
                        }
                }
            }
        }

        private void OnAppear(int count = 1)
        {
            AppearingChips -= count;
            if (DestroyingChips > 0)
                return;
            if (AppearingChips > 0)
                return;
            DispenseAndDropDown();
        }

        private void OnDestroy(int count = 1)
        {
            DestroyingChips -= count;
            if (DestroyingChips > 0)
                return;
            if (AppearingChips > 0)
                return;
            DispenseAndDropDown();
        }

        private void DispenseAndDropDown()
        {
            for (int x = 0; x < Width; x++)
            {
                var stable = true;
                for (int y = Height - 1; y >= 0; y--)
                {
                    var cell = Cells[x][y];
                    if (stable && cell.Ready) continue;
                    stable = false;
                    cell.Ready = false;
                    WaitingCells++;
                    if (cell.Empty)
                        Dispensers[x].Dispence(new Chip(this, RenderCache));
                    else
                        Dispensers[x].FallChip(cell.CurrentChip, y);
                }
            }
        }

        private IEnumerable<HorizontalLine> GetHorizontalLines(int y)
        {
            var color = Cells[0][y].CurrentChip.Color;
            var line = new HorizontalLine() { Position = new Point(0, y) };
            for (int x = 0; x < Width; x++)
            {
                if (color == Cells[x][y].CurrentChip.Color)
                    line.Length++;
                else
                {
                    if (line.Length >= 3)
                        yield return line;
                    line = new HorizontalLine() { Position = new Point(x, y), Length = 1 };
                    color = Cells[x][y].CurrentChip.Color;
                }
            }
            if (line.Length >= 3)
                yield return line;
        }

        private IEnumerable<HorizontalLine> GetVerticalLines(int x)
        {
            var color = Cells[x][0].CurrentChip.Color;
            var line = new HorizontalLine() { Position = new Point(x, 0) };
            for (int y = 0; y < Height; y++)
            {
                if (color == Cells[x][y].CurrentChip.Color)
                    line.Length++;
                else
                {
                    if (line.Length >= 3)
                        yield return line;
                    line = new HorizontalLine() { Position = new Point(x, y), Length = 1 };
                    color = Cells[x][y].CurrentChip.Color;
                }
            }
            if (line.Length >= 3)
                yield return line;
        }

        private IEnumerator<float> ListenFirstClick()
        {
            while (true)
            {
                while (Mouse.GetState().RightButton == ButtonState.Pressed)
                    yield return 0;
                while (Mouse.GetState().RightButton == ButtonState.Released)
                    yield return 0;
                var pos = GetCellPos(Mouse.GetState().Position);
                if (!CheckInBorder(pos))
                    continue;
                new SelectEffect(Cells[pos.X][pos.Y].CurrentChip.Renderer).Play(() =>
                {
                    Coroutine.Start(this, ListenSecondClick(pos));
                });
                break;
            }
        }

        private IEnumerator<float> ListenSecondClick(Point selectedCell)
        {
            while (true)
            {
                while (Mouse.GetState().RightButton == ButtonState.Pressed)
                    yield return 0;
                while (Mouse.GetState().RightButton == ButtonState.Released)
                    yield return 0;
                var pos = GetCellPos(Mouse.GetState().Position);
                if (!CheckInBorder(pos))
                    continue;

                var selected = Cells[selectedCell.X][selectedCell.Y].CurrentChip;
                var target = Cells[pos.X][pos.Y].CurrentChip;
                var direction = pos - selectedCell;
                if (Math.Abs(direction.X) + Math.Abs(direction.Y) != 1)
                {
                    new DeselectEffect(selected.Renderer).Play(() =>
                    {
                        Coroutine.Start(this, ListenFirstClick());
                    });
                }
                else
                {
                    new DeselectEffect(selected.Renderer).Play();
                    if (CheckCollapseChance(selectedCell, pos))
                    {
                        new SwapEffect(selected.Renderer, target.Renderer).Play(() =>
                        {
                            Cells[selectedCell.X][selectedCell.Y].CurrentChip = target;
                            Cells[pos.X][pos.Y].CurrentChip = selected;
                            CheckCombos(selectedCell, pos);
                        });
                    }
                    else
                    {
                        new NoSwapEffect(selected.Renderer, target.Renderer).Play(() =>
                        {
                            Coroutine.Start(this, ListenFirstClick());
                        });
                    }
                }
                break;
            }
        }

        private bool CheckCollapseChance(Point pos1, Point pos2)
        {
            var color1 = Cells[pos2.X][pos2.Y].CurrentChip.Color;
            var color2 = Cells[pos1.X][pos1.Y].CurrentChip.Color;
            return GetSameColorCrossCollapse(pos1, color1, pos2) || GetSameColorCrossCollapse(pos2, color2, pos1);
        }

        private bool GetSameColorCrossCollapse(Point pos, ChipColor color, Point wrongDir)
        {
            var l = GetSameColorCrossInOneDirection(pos, new Point(-1, 0), color, wrongDir);
            var r = GetSameColorCrossInOneDirection(pos, new Point(1, 0), color, wrongDir);
            var t = GetSameColorCrossInOneDirection(pos, new Point(0, -1), color, wrongDir);
            var b = GetSameColorCrossInOneDirection(pos, new Point(0, 1), color, wrongDir);

            var h = l + r;
            var v = t + b;
            return h >= 2 || v >= 2;
        }

        private int GetSameColorCrossInOneDirection(Point pos, Point dir, ChipColor color, Point wrongDir)
        {
            int res = 0;
            var test = pos + dir;
            if (test != wrongDir && CheckInBorder(test) && Cells[test.X][test.Y].CurrentChip.Color == color)
            {
                res++;
                test += dir;
                if (CheckInBorder(test) && Cells[test.X][test.Y].CurrentChip.Color == color)
                    res++;
            }
            return res;
        }

        private bool CheckInBorder(Point pos)
        {
            return pos.X >= 0 && pos.X < Width && pos.Y >= 0 && pos.Y < Height;
        }

        private Point GetCellPos(Point mousePos)
        {
            var pos = new Vector2(mousePos.X, mousePos.Y) - _position - _step / 2f;
            return new Point(Mathf.RoundToInt(pos.X / _step.X), Mathf.RoundToInt(pos.Y / _step.Y));
        }

        private bool Cross(HorizontalLine hor, HorizontalLine ver)
        {
            return hor.Position.X <= ver.Position.X && ver.Position.X < hor.Position.X + hor.Length &&
                   ver.Position.Y <= hor.Position.Y && hor.Position.Y < ver.Position.Y + ver.Length;
        }

        private Vector2 GetPosition(int x, int y)
        {
            return _position + _step.Scale(x + 0.5f, y + 0.5f);
        }
    }
}
