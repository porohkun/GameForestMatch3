using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameForestMatch3.Core
{
    public sealed class Coroutine
    {
        private static List<Coroutine> _coroutines = new List<Coroutine>();

        public static void BindToGame(IGame game)
        {
            game.OnUpdate += Game_OnUpdate;
        }

        /// <summary>
        /// routine return values:
        /// 0: continue on next update
        /// n>0: continue after n seconds
        /// n<0: stop
        /// </summary>
        public static Coroutine Start(GameObject obj, IEnumerator<float> routine)
        {
            var coroutine = new Coroutine(obj, routine);
            _coroutines.Add(coroutine);
            return coroutine;
        }

        public static void Stop(Coroutine coroutine)
        {
            coroutine.Finished = true;
        }

        public static void StopAllFor(GameObject obj)
        {
            var coroutines = _coroutines.Where(c => c.Object == obj).ToArray();
            foreach (var coroutine in coroutines)
                Stop(coroutine);
        }

        private static void Game_OnUpdate(GameTime gameTime)
        {
            foreach (var coroutine in _coroutines)
            {
                if (!coroutine.Object.Enabled)
                {
                    Stop(coroutine);
                    continue;
                }
                coroutine._delay -= (float)gameTime.ElapsedGameTime.TotalSeconds;
                if (coroutine._delay > 0f) continue;
                if (coroutine._routine.MoveNext())
                {
                    coroutine._delay = coroutine._routine.Current;
                    if (coroutine._delay > 0f || Mathf.Approximately(coroutine._delay, 0f))
                        continue;
                }
                coroutine.Finished = true;
            }
            _coroutines.RemoveAll(c => c.Finished);
        }

        public GameObject Object { get; }
        public bool Finished { get; private set; }

        private IEnumerator<float> _routine;
        private float _delay;

        private Coroutine(GameObject obj, IEnumerator<float> routine)
        {
            Object = obj;
            _routine = routine;
        }
    }
}
