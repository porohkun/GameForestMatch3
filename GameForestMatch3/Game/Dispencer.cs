using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameForestMatch3.Core;
using Microsoft.Xna.Framework;

namespace GameForestMatch3
{
    public class Dispencer : GameObject
    {
        private static Random _rnd = new Random();

        public int X { get; }
        public Cell[] Stack { get; }
        public Vector2 Position { get; }
        public Vector2 Size { get; }

        public event Action<int, int> ChipArrived;

        private Queue<Chip> _order = new Queue<Chip>();
        private Chip _currentChip;

        public Dispencer(RenderCache renderCache, int x, Cell[] stack, Vector2 position, Vector2 size) : base(renderCache)
        {
            X = x;
            Stack = stack;
            Position = position;
            Size = size;
        }

        private int index = 0;
        public void Dispence(Chip chip)
        {
            _order.Enqueue(chip);
            chip.Renderer.Index = ++index;
            chip.Renderer.Color = new Color(1f, 1f, 1f, 0f);
        }

        protected override void Update(GameTime gameTime)
        {
            if (_currentChip == null && _order.Count > 0)
            {
                _currentChip = _order.Dequeue();
                if (_currentChip != null)
                {
                    //_currentChip.Renderer.Size = Size;

                    FallChip(_currentChip, -1);
                }
            }
        }

        public void FallChip(Chip chip, int fallFrom)
        {
            if (!chip.Falling)
                Coroutine.Start(this, FallChipCoutine(chip, fallFrom));
        }

        private float _startSpeed = 700f;
        private IEnumerator<float> FallChipCoutine(Chip chip, int fallFrom)
        {
            chip.Falling = true;
            //fallFrom= GetCell(pos.Y)
            chip.Renderer.Position = GetPosition(fallFrom);
            chip.Renderer.Size = Size;
            //var speed = 100f + _rnd.Next(-10, 10);
            var  speed = _startSpeed+ _rnd.Next(0, 20);
            var acc = 200;

            var currentCell = fallFrom; //>= 0 ? fallFrom : 0;

            while (true)
            {
                var deltaT = (float)Coroutine.Time.ElapsedGameTime.TotalSeconds;
                speed += acc * deltaT;
                var moving = new Vector2(0f, speed * deltaT);
                var pos = chip.Renderer.Position + moving; //считем новую координату
                chip.Renderer.Position = pos; //перемещаем фишку по новым координатам
                var nextCell = currentCell + 1; //следующая ячейка

                if (nextCell >= 0 && nextCell < Stack.Length && !Stack[nextCell].Empty) //если следующая ячейка занята, возвращаем фишку на координату текущей ячейки
                {
                    if (pos.Y >= GetPosition(currentCell).Y)
                    {
                        chip.Renderer.Position = GetPosition(currentCell);
                        speed = _startSpeed;
                        if (!Stack[nextCell].CurrentChip.Falling) //если следующая фишка остановилась, то останавливаем и эту
                        {
                            chip.Renderer.Color = Color.White;
                                chip.Falling = false;
                            new AfterFallEffect(chip.Renderer).Play(() =>
                            {
                                Stack[currentCell].Ready = true;
                                Stack[currentCell].CurrentChip = chip;
                                ChipArrived?.Invoke(X, currentCell);
                                if (_currentChip == chip)
                                    _currentChip = null;
                            });
                            yield break;
                        }
                    }
                }
                else if (nextCell == Stack.Length) //если перелетели поле, то возвращаемся и останавливаемся
                {
                    if (pos.Y >= GetPosition(currentCell).Y)
                    {
                        chip.Renderer.Position = GetPosition(currentCell);
                        speed = _startSpeed;
                            chip.Falling = false;
                        new AfterFallEffect(chip.Renderer).Play(() =>
                        {
                            Stack[currentCell].Ready = true;
                            Stack[currentCell].CurrentChip = chip;
                            ChipArrived?.Invoke(X, currentCell);
                        });
                        yield break;
                    }
                }
                else
                {
                    if (_currentChip == chip) //меняем прозрачность
                    {
                        var a = (pos.Y - Position.Y) / Size.Y;
                        if (a >= 1f) a = 1f;
                        chip.Renderer.Color = new Color(1f, 1f, 1f, a);
                        if (pos.Y >= GetPosition(0).Y + Size.Y / 2f)
                            _currentChip = null;
                    }
                    nextCell = GetCell(pos.Y); //высчитываем ячейку от координат
                    if (nextCell > currentCell) //если высчитанная ячейка != текущей то переносим фишку в следующую ячейку
                    {
                        if (currentCell >= 0 && currentCell < Stack.Length && Stack[currentCell].CurrentChip == chip)
                            Stack[currentCell].CurrentChip = null;
                        Stack[nextCell].CurrentChip = chip;
                        currentCell = nextCell;
                    }
                }
                yield return 0f;
            }
        }

        private int GetCell(float y)
        {
            var pos = Position.Y - Size.Y / 2f;
            var cell = -1;
            while (pos < y)
            {
                pos += Size.Y;
                cell++;
            }
            return cell - 1;
        }

        private Vector2 GetPosition(int y)
        {
            return Position + new Vector2(0, Size.Y * (y + 1));
        }

        protected override void Draw(GameTime gameTime)
        {

        }
    }
}
