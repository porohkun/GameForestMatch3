using System.Collections.Generic;

namespace GameForestMatch3
{
    public class EffectsManager 
    {
        //#region Части эффектов
        
        //private Dictionary<string, List<BaseEffectAction>> _actions = new Dictionary<string, List<BaseEffectAction>>();
        
        //public T GetAction<T>(string prefabName = null) where T : BaseEffectAction
        //{
        //    var actionName = prefabName == null ? typeof(T).Name : prefabName;
        //    if (!_actions.ContainsKey(actionName))
        //        _actions.Add(actionName, new List<BaseEffectAction>());
        //    foreach (var item in _actions[actionName])
        //        if (item.Free)
        //        {
        //            item.Lock();
        //            return item as T;
        //        }
        //    return GetNewAction<T>(prefabName);
        //}
        
        //private T GetNewAction<T>(string prefabName = null) where T : BaseEffectAction
        //{
        //    var actionName = prefabName == null ? typeof(T).Name : prefabName;
        //    var action = Instantiate(Resources.Load<T>("EffectActions/" + actionName));
        //    action.transform.SetParent(transform);
        //    action.name = actionName;
        //    action.Initialize(this);
        //    _actions[actionName].Add(action);
        //    return action;
        //}

        //#endregion

        //#region Эффекты
        
        //private Dictionary<string, BaseEffect> _effects = new Dictionary<string, BaseEffect>();
        
        //public T GetEffect<T>(string prefabName = null) where T : BaseEffect
        //{
        //    var effectName = prefabName == null ? typeof(T).Name : prefabName;
        //    if (!_effects.ContainsKey(effectName))
        //        _effects.Add(effectName, Resources.Load<T>("Effects/" + effectName));
        //    var effect = ScriptableObject.Instantiate<T>(_effects[effectName] as T);
        //    effect.Initialize(this);
        //    return effect;
        //}

        //#endregion

    }
}
